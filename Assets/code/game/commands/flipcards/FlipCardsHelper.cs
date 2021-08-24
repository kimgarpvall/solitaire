using System.Collections.Generic;
using System.Linq;

public class FlipCardsHelper
{
    private readonly IReadTableState mReadTableState;
    private readonly IWriteTableState mWriteTableState;

    private List<Card> mResetState;

    public FlipCardsHelper()
    {
        mReadTableState = Dependencies.Get().Get<IReadTableState>();
        mWriteTableState = Dependencies.Get().Get<IWriteTableState>();
        mResetState = new List<Card>(mReadTableState.GetState());
    }

    public void TryFlippingCards()
    {
        // Flip cards
        List<Card> flipCards = new List<Card>();
        foreach (var position in ModelUtils.TableuPositions)
        {
            var cards = ModelUtils.GetCardsAtPosition(mReadTableState.GetState(), position);
            cards = cards
                .OrderBy(p => { return p.mIndex; })
                .ToList();

            if (cards.Count > 0)
            {
                var cardToFlip = cards[cards.Count - 1];
                if (!cardToFlip.mIsShowing)
                {
                    cardToFlip.mIsShowing = true;
                    flipCards.Add(cardToFlip);
                }
            }
        }

        if(flipCards.Count > 0)
        {
            mWriteTableState.UpdateState(flipCards);
        }
    }

    public void UndoCardFlip()
    {
        mWriteTableState.UpdateState(mResetState);
    }
}
