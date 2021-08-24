using System.Collections.Generic;
using UnityEngine;

public class TryPlacingCardsCommand : Command
{
    private readonly TablePosition mTryPlacePosition;
    private readonly List<Card> mCardsToPlace;
    private readonly IWriteTableState mWriteTableState;
    private readonly IReadTableState mReadTableState;

    private FlipCardsHelper mFlipCardsHelper;

    // Undo/remove
    private List<Card> mBeforeState;
    private List<Card> mAfterState;

    public TryPlacingCardsCommand(TablePosition tryPlacePosition, List<Card> cardsToPlace)
    {
        mTryPlacePosition = tryPlacePosition;
        mCardsToPlace = new List<Card>(cardsToPlace);
        mWriteTableState = Dependencies.Get().Get<IWriteTableState>();
        mReadTableState = Dependencies.Get().Get<IReadTableState>();

        mFlipCardsHelper = new FlipCardsHelper();
    }

    public override CommandUndoBehaviour Execute()
    {
        mBeforeState = mReadTableState.GetState();

        if(PlaceCards(mCardsToPlace[0].mCardId))
        {
            mFlipCardsHelper.TryFlippingCards();
            mAfterState = mReadTableState.GetState();
            return CommandUndoBehaviour.AllowUndo;
        }

        return CommandUndoBehaviour.DoNotAllowUndo;
    }

    public override void Redo()
    {
        mWriteTableState.UpdateState(mAfterState);
    }
    public override void Undo()
    {
        mWriteTableState.UpdateState(mBeforeState);
    }

    private bool PlaceCards(int firstCardId)
    {
        var placementIndex = TryGetPlacementIndex(mTryPlacePosition, firstCardId);
        if (placementIndex != null)
        {
            // Update cards
            for (int i = 0; i < mCardsToPlace.Count; i++)
            {
                var card = mCardsToPlace[i];
                var cardCopy = Card.Copy(card);

                cardCopy.mPosition = mTryPlacePosition;
                // Index, underneath the card that's already there
                cardCopy.mIndex = placementIndex.Value + 1 + i;

                mWriteTableState.UpdateState(cardCopy);
            }

            return true;
        }

        return false;
    }

    // Index of this position to place the cards at
    private int? TryGetPlacementIndex(TablePosition position, int cardToPlaceId)
    {
        var cardsAtPosition = ModelUtils.GetCardsAtPosition(mReadTableState.GetState(), position);
        var cardToPlace = ModelUtils.GetReadableCard(cardToPlaceId);

        // Only the King can be placed on an empty position
        // The King is placed on index 0 -> return -1 for the calculation, "index + 1", to make sense
        if (cardsAtPosition.Count == 0)
        {
            // Is Tableu AND king?
            if(ModelUtils.TableuPositions.Contains(position))
            {
                return cardToPlace.mValue == 12 ? (int?)-1 : null;
            }
            // Is Foundation AND Ace?
            else if(ModelUtils.FoundationPositions.Contains(position))
            {
                return cardToPlace.mValue == 0 ? (int?)-1 : null;
            }
        }

        var lastCardId = cardsAtPosition[cardsAtPosition.Count - 1].mCardId;
        var lastCard = ModelUtils.GetReadableCard(lastCardId);
        bool canPlace = IsValidSuiteMatch(cardToPlace, lastCard, position);
        return canPlace ? (int?)mReadTableState.GetCard(lastCardId).mIndex : null;
    }

    private bool IsValidSuiteMatch(ReadableCard cardToPlace, ReadableCard lastCard, TablePosition position)
    {
        if(ModelUtils.FoundationPositions.Contains(position))
        {
            return cardToPlace.mSuite == lastCard.mSuite && lastCard.mValue == cardToPlace.mValue - 1;
        }
        else if(ModelUtils.TableuPositions.Contains(position))
        {
            if (cardToPlace.mSuite == Suite.Spades)
                return (lastCard.mSuite == Suite.Hearts || lastCard.mSuite == Suite.Diamonds) && IsValidTableuMatch(cardToPlace.mValue, lastCard.mValue);
            else if (cardToPlace.mSuite == Suite.Hearts)
                return (lastCard.mSuite == Suite.Spades || lastCard.mSuite == Suite.Clubs) && IsValidTableuMatch(cardToPlace.mValue, lastCard.mValue);
            else if (cardToPlace.mSuite == Suite.Clubs)
                return (lastCard.mSuite == Suite.Hearts || lastCard.mSuite == Suite.Diamonds) && IsValidTableuMatch(cardToPlace.mValue, lastCard.mValue);
            else if (cardToPlace.mSuite == Suite.Diamonds)
                return (lastCard.mSuite == Suite.Spades || lastCard.mSuite == Suite.Clubs) && IsValidTableuMatch(cardToPlace.mValue, lastCard.mValue);
        }

        Debug.LogAssertion("Can't figure out if this is a valid suite match");
        return false;
    }

    private bool IsValidTableuMatch(int cardToPlaceValue, int lastCardValue)
    {
        return cardToPlaceValue == lastCardValue - 1;
    }

    
}
