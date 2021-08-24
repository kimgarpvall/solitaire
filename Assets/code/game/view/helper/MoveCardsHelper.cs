using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveCardsHelper
{
    // Types
    public delegate float GetDistanceBetweenCards();
    public delegate float GetPointerCardOffsetWhenMovingCard();

    private enum MoveState
    {
        WaitingToMove,
        Moving
    }

    // Dependencies
    private readonly List<CardView> mCards;
    private readonly GetDistanceBetweenCards mGetDistanceBetweenCardsDelegate;
    private readonly GetPointerCardOffsetWhenMovingCard mGetPointerCardDistanceWhenMovingCard;
    private readonly IReadTableState mReadTableState;
    private readonly EventBus mEventBus;
    private readonly IInputController mInputController;

    // Logic
    private MoveState mMoveState = MoveState.WaitingToMove;
    private List<Card> mCardsToMove = new List<Card>();
    private List<CardView> mCardViewsToMove = new List<CardView>();

    public MoveCardsHelper(
        List<CardView> cards,
        GetDistanceBetweenCards getDistanceBetweenCardsDelegate,
        GetPointerCardOffsetWhenMovingCard getPointerOffsetDistanceWhenMovingCard)
    {
        mCards = cards;
        mGetDistanceBetweenCardsDelegate = getDistanceBetweenCardsDelegate;
        mGetPointerCardDistanceWhenMovingCard = getPointerOffsetDistanceWhenMovingCard;

        mReadTableState = Dependencies.Get().Get<IReadTableState>();
        mEventBus = Dependencies.Get().Get<EventBus>();
        mInputController = Dependencies.Get().Get<IInputController>();
    }

    public void UpdateMove()
    {
        if (mInputController.GetMainInput() == null)
            return;

        if (mMoveState == MoveState.WaitingToMove)
        {
            if (TryStartingMove())
                mMoveState = MoveState.Moving;
        }
        else if (mMoveState == MoveState.Moving)
        {
            if (!TryKeepMoving())
            {
                EndMove();
                mMoveState = MoveState.WaitingToMove;
            }
        }
    }

    // Move cards
    private bool TryStartingMove()
    {
        var input = mInputController.GetMainInput().Value;
        if (input.mInputState == InputState.Down)
        {
            var cardGameObject = UIUtils.GetGameObject(input.mPosition, UIUtils.Masks.Cards);
            if (cardGameObject != null)
            {
                var cardView = cardGameObject.GetComponentInParent<CardView>();
                mCardsToMove = ModelUtils.GetCardsToMove(mReadTableState.GetState(), mReadTableState.GetCard(cardView.GetId()));

                // Sort cards by index
                mCardsToMove.OrderBy(p => { return p.mIndex; });

                // Sort card views by same index
                mCardViewsToMove.Clear();
                foreach (var c in mCardsToMove)
                {
                    foreach (var cc in mCards)
                    {
                        if (c.mCardId == cc.GetId())
                        {
                            mCardViewsToMove.Add(cc);
                        }
                    }
                }

                return true;
            }
        }

        return false;
    }

    private bool TryKeepMoving()
    {
        //UpdateHierarchy();

        var input = mInputController.GetMainInput().Value;
        if (input.mInputState == InputState.Active)
        {
            // Move the stack of cards
            for (int i = 0; i < mCardViewsToMove.Count; i++)
            {
                var pos = mCardViewsToMove[i].transform.position;
                pos.x = input.mPosition.x;
                pos.y = input.mPosition.y;

                // Relative pos
                pos.y -= i * mGetDistanceBetweenCardsDelegate.Invoke();
                pos.y -= mGetPointerCardDistanceWhenMovingCard.Invoke();

                // Apply
                mCardViewsToMove[i].transform.position = pos;
            }

            return true;
        }

        return false;
    }

    private void EndMove()
    {
        var placementPosition = GetNearestPlaceablePosition(mInputController.GetMainInput().Value.mPosition);

        // Reset
        var cardsToMove = new List<Card>(mCardsToMove);
        mCardViewsToMove.Clear();
        mCardsToMove.Clear();

        // Place
        if (placementPosition != null)
        {
            // Place cards
            mEventBus.PostMessage(new TryPlaceCardsEvent {
                mPositionToPlace = placementPosition.Value,
                mCardsToPlace = cardsToMove
            });
        }
        else
        {
            // Reset
            mEventBus.PostMessage(new SetupDeckViewEvent());
        }
    }

    // Utils
    private TablePosition? GetNearestPlaceablePosition(Vector2 touchPos)
    {
        Dictionary<UIUtils.Masks, TablePosition> maskTablePositionMappings = new Dictionary<UIUtils.Masks, TablePosition> {
            { UIUtils.Masks.Foundation0, TablePosition.Foundation0 },
            { UIUtils.Masks.Foundation1, TablePosition.Foundation1 },
            { UIUtils.Masks.Foundation2, TablePosition.Foundation2 },
            { UIUtils.Masks.Foundation3, TablePosition.Foundation3 },

            { UIUtils.Masks.Tableu0, TablePosition.Tableu0 },
            { UIUtils.Masks.Tableu1, TablePosition.Tableu1 },
            { UIUtils.Masks.Tableu2, TablePosition.Tableu2 },
            { UIUtils.Masks.Tableu3, TablePosition.Tableu3 },
            { UIUtils.Masks.Tableu4, TablePosition.Tableu4 },
            { UIUtils.Masks.Tableu5, TablePosition.Tableu5 },
            { UIUtils.Masks.Tableu6, TablePosition.Tableu6 }
        };

        // Ignore the moving cards
        ToggleMovingCards(false);

        var UIPosition = new Vector3(touchPos.x, touchPos.y, 0f);
        var cardGameObject = UIUtils.GetGameObject(UIPosition, UIUtils.Masks.Cards);
        if (cardGameObject != null)
        {
            var cardView = cardGameObject.GetComponentInParent<CardView>();
            var card = mReadTableState.GetCard(cardView.GetId());
            if (card.mIsShowing &&
                card.mPosition != TablePosition.Stock &&
                card.mPosition != TablePosition.StockOpen)
            {
                ToggleMovingCards(true);
                return card.mPosition;
            }
        }

        foreach (var mapping in maskTablePositionMappings)
        {
            if (UIUtils.GetGameObject(UIPosition, mapping.Key) != null)
            {
                ToggleMovingCards(true);
                return mapping.Value;
            }
        }

        ToggleMovingCards(true);
        return null;
    }
    private void ToggleMovingCards(bool activate)
    {
        foreach (var m in mCardViewsToMove)
            m.gameObject.SetActive(activate);
    }

    public void UpdateHierarchy()
    {
        // Tableu cards by y position
        {
            var cardViews = mCards
                .Where(p =>
                {
                    return ModelUtils.TableuPositions.Contains(mReadTableState.GetCard(p.GetId()).mPosition);
                })
                .OrderBy(p=>
                {
                    return p.transform.position.x;
                })
                .OrderBy(p =>
                {
                    return p.transform.position.y;
                })
                .Reverse()
                .ToList();

            ApplySiblingIndexes(cardViews, 100);
        }
        // Open stock
        {
            var cardViews = mCards
                .Where(p => {
                    return mReadTableState.GetCard(p.GetId()).mPosition == TablePosition.StockOpen;
                })
                .OrderBy(p => {
                    return mReadTableState.GetCard(p.GetId()).mIndex;
                })
                .ToList();

            ApplySiblingIndexes(cardViews, 200);
        }
        // Closed stock
        {
            var cardViews = mCards
                .Where(p => {
                    return mReadTableState.GetCard(p.GetId()).mPosition == TablePosition.Stock;
                })
                .OrderBy(p => {
                    return mReadTableState.GetCard(p.GetId()).mIndex;
                })
                .ToList();

            ApplySiblingIndexes(cardViews, 300);
        }
        // Foundation
        {
            var cardViews = mCards
                .Where(p => {
                    return ModelUtils.FoundationPositions.Contains(mReadTableState.GetCard(p.GetId()).mPosition);
                })
                .OrderBy(p => {
                    return mReadTableState.GetCard(p.GetId()).mIndex;
                })
                .ToList();

            ApplySiblingIndexes(cardViews, 400);
        }


        // Moving cards - always on top
        int movingCardsIndex = 500;
        foreach (var c in mCardViewsToMove)
        {
            c.transform.SetSiblingIndex(movingCardsIndex);
            movingCardsIndex--;
        }
    }
    private int ApplySiblingIndexes(List<CardView> cardViews, int siblingIndex)
    {
        for (int i = 0; i < cardViews.Count; i++)
        {
            cardViews[i].transform.SetSiblingIndex(siblingIndex);
            siblingIndex--;
        }

        return siblingIndex;
    }
}
