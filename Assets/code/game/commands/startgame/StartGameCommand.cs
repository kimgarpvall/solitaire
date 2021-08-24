using System.Collections.Generic;

public class StartGameCommand : Command
{
    private readonly CommandQueue mCommandQueue;
    private readonly IWriteTableState mWriteTableState;
    
    public StartGameCommand(CommandQueue commandQueue)
    {
        mCommandQueue = commandQueue;
        mWriteTableState = Dependencies.Get().Get<IWriteTableState>();
    }

    public override CommandUndoBehaviour Execute()
    {
        mCommandQueue.Reset();
        mWriteTableState.Initialise(new List<Card>(GetShuffledDeck()));

        return CommandUndoBehaviour.DoNotAllowUndo;
    }

    public override void Undo()
    {
    }
    public override void Redo()
    {
    }

    private List<Card> GetShuffledDeck()
    {
        var deck = new List<Card>();

        // Generate 52 cards
        for (int i = 0; i < 52; i++)
        {
            deck.Add(new Card { mCardId = i, mIndex = i, mPosition = TablePosition.Stock, mIsShowing = false });
        }

        // Shuffle the deck
        RandomUtils.Shuffle(deck, Dependencies.Get().Get<RandomNumberGenerator>());

        // Set the cards up in the correct order and facing
        int cardIndex = 0;
        for (int i = 0; i < ModelUtils.TableuPositions.Count; i++)
        {
            cardIndex = SetupTableu(deck, ModelUtils.TableuPositions[i], cardIndex, i + 1);
        }

        // The stock cards have been shuffled already, update their indexes to match their order
        var stockIndex = 0;
        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i].mPosition == TablePosition.Stock)
            {
                var c = deck[i];
                c.mIndex = stockIndex;
                deck[i] = c;
                stockIndex++;
            }
        }

        return deck;
    }

    int SetupTableu(List<Card> cards, TablePosition position, int cardIndex, int tableuSize)
    {
        for(int i = 0; i < tableuSize; i++)
        {
            var card = cards[cardIndex];
            card.mPosition = position;
            card.mIndex = i;

            card.mIsShowing = i == tableuSize - 1;

            cards[cardIndex] = card;

            cardIndex++;
        }

        return cardIndex;
    }
}
