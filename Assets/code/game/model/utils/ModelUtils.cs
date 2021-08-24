using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModelUtils
{
    public static List<TablePosition> TableuPositions = new List<TablePosition> {
        TablePosition.Tableu0,
        TablePosition.Tableu1,
        TablePosition.Tableu2,
        TablePosition.Tableu3,
        TablePosition.Tableu4,
        TablePosition.Tableu5,
        TablePosition.Tableu6,
    };
    public static List<TablePosition> FoundationPositions = new List<TablePosition> {
        TablePosition.Foundation0,
        TablePosition.Foundation1,
        TablePosition.Foundation2,
        TablePosition.Foundation3
    };

    public static List<Card> GetCardsToMove(List<Card> allCards, Card activeCard)
    {
        // Can't move a card from the stock
        if(activeCard.mPosition == TablePosition.Stock)
        {
            return new List<Card>();
        }

        // Foundation and open stock is just one card
        if( activeCard.mPosition == TablePosition.Foundation0 ||
            activeCard.mPosition == TablePosition.Foundation1 ||
            activeCard.mPosition == TablePosition.Foundation2 ||
            activeCard.mPosition == TablePosition.Foundation3 ||
            activeCard.mPosition == TablePosition.StockOpen)
        {
            return new List<Card> { activeCard };
        }

        return allCards
            .Where(p=>{ return p.mIsShowing && p.mPosition == activeCard.mPosition && p.mIndex >= activeCard.mIndex ; })
            .OrderBy(p=> { return p.mIndex; })
            .ToList();
    }

    public static int GetNumberOfCardsAtPosition(List<Card> allCards, TablePosition position)
    {
        return allCards.Count(p => p.mPosition == position);
    }

    public static List<Card> GetCardsAtPosition(List<Card> allCards, TablePosition position)
    {
        return allCards
            .Where(p=> p.mPosition == position)
            .OrderBy(p=> { return p.mIndex; })
            .ToList();
    }

    public static ReadableCard GetReadableCard(int cardId)
    {
        Suite suite = Suite.Spades;
        int value = 0;
        if(cardId <= 12)
        {
            suite = Suite.Spades;
            value = cardId;
        }
        else if(cardId >= 13 && cardId <= 25)
        {
            suite = Suite.Hearts;
            value = cardId - 13;
        }
        else if(cardId >= 26 && cardId <= 38)
        {
            suite = Suite.Clubs;
            value = cardId - 26;
        }
        else if(cardId >= 39)
        {
            suite = Suite.Diamonds;
            value = cardId - 39;
        }

        return new ReadableCard {
            mSuite = suite,
            mValue = value
        };
    }
}
