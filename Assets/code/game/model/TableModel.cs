using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TableModel : IInitialiser, IReadTableState, IWriteTableState
{
    private List<Card> mState = new List<Card>();
    public void OnInit()
    {
    }

    // IReadTableState  
    public List<Card> GetState()
    {
        return new List<Card>(mState);
    }
    public Card GetCard(int cardId)
    {
        return Card.Copy(mState.First(p => { return p.mCardId == cardId; }));
    }

    // IWriteTableState
    public void Initialise(List<Card> cards)
    {
        mState.Clear();
        foreach(var c in cards)
        {
            mState.Add(Card.Copy(c));
        }
    }
    public void UpdateState(List<Card> cards)
    {
        foreach(var card in cards)
        {
            UpdateState(card);
        }
    }
    public void UpdateState(Card card)
    {
        var copy = Card.Copy(card);
        for(int i = 0; i < mState.Count; i++)
        {
            if(mState[i].mCardId == copy.mCardId)
            {
                mState[i] = copy;
                return;
            }
        }

        Debug.LogAssertion("Could not update card because it doesn't exist in mState");
    }
}
