using System.Linq;
using System.Collections.Generic;

public class TakeCardFromStockCommand : Command
{
    private readonly IReadTableState mReadTableState;
    private readonly IWriteTableState mWriteTableState;

    private List<Card> mBeforeState;
    private List<Card> mAfterState;

    public TakeCardFromStockCommand()
    {
        mReadTableState = Dependencies.Get().Get<IReadTableState>();
        mWriteTableState = Dependencies.Get().Get<IWriteTableState>();
    }

    public override CommandUndoBehaviour Execute()
    {
        mBeforeState = mReadTableState.GetState();

        // Get the highest index on the stock - card id
        // Get the card at the top of stock stack
        var sortedStockCards = mReadTableState.GetState()
            .Where(p => { return p.mPosition == TablePosition.Stock; })
            .OrderBy(p => { return p.mIndex; })
            .ToList();

        if (sortedStockCards.Count == 0)
        {
            // Move all the cards back to the stock
            var cardsToMoveToStock = mReadTableState.GetState()
                .Where(p => { return p.mPosition == TablePosition.StockOpen; })
                .OrderBy(p => { return -p.mIndex; })
                .ToList();
            for (int i = 0; i < cardsToMoveToStock.Count; i++)
            {
                var c = cardsToMoveToStock[i];
                c.mIndex = i;
                c.mPosition = TablePosition.Stock;
                c.mIsShowing = false;
                cardsToMoveToStock[i] = c;
            }

            mWriteTableState.UpdateState(cardsToMoveToStock);
        }
        else
        {
            // Move the card at the top of the stock to the open stock position
            var card = sortedStockCards[sortedStockCards.Count - 1];
            card.mIsShowing = true;
            card.mPosition = TablePosition.StockOpen;

            Card? lastCardInOpenStock = mReadTableState.GetState()
                .Where(p => { return p.mPosition == TablePosition.StockOpen; })
                .OrderBy(p => { return p.mIndex; })
                .LastOrDefault();

            card.mIndex = lastCardInOpenStock != null ? lastCardInOpenStock.Value.mIndex + 1 : 0;
            mWriteTableState.UpdateState(card);
        }

        mAfterState = mReadTableState.GetState();
        return CommandUndoBehaviour.AllowUndo;
    }

    public override void Undo()
    {
        UnityEngine.Debug.Log("TakeCardFromStockCommand Undo");
        mWriteTableState.UpdateState(mBeforeState);
    }
    public override void Redo()
    {
        UnityEngine.Debug.Log("TakeCardFromStockCommand Redo");
        mWriteTableState.UpdateState(mAfterState);
    }
}
