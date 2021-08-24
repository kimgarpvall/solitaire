using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TableView : MonoBehaviour, IInitialiser, IUpdater
{
    public GameObject pDistanceCalculationFirstCard;
    public GameObject pDistanceCalculationSecondCard;

    public GameObject pPointerOffsetDistanceCalculationFirstCard;
    public GameObject pPointerOffsetDistanceCalculationSecondCard;

    public Transform pCardsParent;

    public Transform pFoundation0;
    public Transform pFoundation1;
    public Transform pFoundation2;
    public Transform pFoundation3;

    public Transform pStock;
    public Transform pStockOpen;

    public Transform pTableu0;
    public Transform pTableu1;
    public Transform pTableu2;
    public Transform pTableu3;
    public Transform pTableu4;
    public Transform pTableu5;
    public Transform pTableu6;

    private List<CardView> mCards;

    private IReadTableState mReadTableState;
    private EventBus mEventBus;
    private EventListenerHandlesHelper mEventListener;

    private MoveCardsHelper mMoveCardsHelper;
    private UpdateStockHelper mUpdateStockHelper;

    // IInitialiser
    public void OnInit()
    {
        // Get dependencies
        mReadTableState = Dependencies.Get().Get<IReadTableState>();
        mEventBus = Dependencies.Get().Get<EventBus>();

        // Create card views
        mCards = new List<CardView>();
        for (int i = 0; i < 52; i++)
            mCards.Add(CreateCard(i));

        mMoveCardsHelper = new MoveCardsHelper(mCards, GetDistanceBetweenCards, GetPointerCardOffsetWhenMovingCard);
        mUpdateStockHelper = new UpdateStockHelper();

        // Event listeners
        mEventListener = new EventListenerHandlesHelper(mEventBus);
        mEventListener.AddHandle(mEventBus.AddListener(new SetupDeckViewEvent(), (Event e)=> {
            SetupDeck();
        }));
    }

    private void SetupDeck()
    {
        // Position the cards
        var cards = mReadTableState.GetState();
        foreach(var card in cards)
        {
            var cardView = GetCardView(card.mCardId);

            // Update card state
            cardView.SetIsShowing(card.mIsShowing);

            // Update card position
            SetCardPosition(cardView.transform, card.mPosition, card.mIndex);
        }

        mMoveCardsHelper.UpdateHierarchy();
    }

    // IUpdater
    private enum MoveCardsState
    {
        WaitingToMove,
        Moving,
        Dropped
    }
    public void OnUpdate(float dt)
    {
        mMoveCardsHelper.UpdateMove();
        mUpdateStockHelper.TryTakingCardFromStock();
        mMoveCardsHelper.UpdateHierarchy();
    }

    // Util
    private CardView GetCardView(int cardId)
    {
        var cardView = mCards.First(p => p.GetId() == cardId);
        return cardView;
    }
    private CardView CreateCard(int cardId)
    {
        var card = (GameObject)Instantiate(Resources.Load("Card"));
        card.transform.SetParent(pCardsParent.transform, false);
        var cardView = card.GetComponent<CardView>();
        cardView.SetId(cardId);
        cardView.SetIsShowing(false);

        return cardView;
    }
    private void SetCardPosition(Transform card, TablePosition position, int index)
    {
        var cardPosition = GetPosition(position, index);
        card.position = new Vector3(cardPosition.x, cardPosition.y, 0f);
    }

    private Vector2 GetPosition(TablePosition position, int index)
    {
        var tablePosition = GetTablePosition(position);

        // Apply tableu y offset
        if (ModelUtils.TableuPositions.Contains(position))
            tablePosition.y += -(index * GetDistanceBetweenCards());

        return tablePosition;
    }

    private Vector2 GetTablePosition(TablePosition position)
    {
        Vector2 startPos = Vector2.zero;
        switch (position)
        {
            case TablePosition.Foundation0: startPos = pFoundation0.transform.position; break;
            case TablePosition.Foundation1: startPos = pFoundation1.transform.position; break;
            case TablePosition.Foundation2: startPos = pFoundation2.transform.position; break;
            case TablePosition.Foundation3: startPos = pFoundation3.transform.position; break;

            case TablePosition.Stock: startPos = pStock.transform.position; break;
            case TablePosition.StockOpen: startPos = pStockOpen.transform.position; break;

            case TablePosition.Tableu0: startPos = pTableu0.transform.position; break;
            case TablePosition.Tableu1: startPos = pTableu1.transform.position; break;
            case TablePosition.Tableu2: startPos = pTableu2.transform.position; break;
            case TablePosition.Tableu3: startPos = pTableu3.transform.position; break;
            case TablePosition.Tableu4: startPos = pTableu4.transform.position; break;
            case TablePosition.Tableu5: startPos = pTableu5.transform.position; break;
            case TablePosition.Tableu6: startPos = pTableu6.transform.position; break;
        }

        return startPos;
    }

    private float GetDistanceBetweenCards()
    {
        var firstY = pDistanceCalculationFirstCard.transform.position.y;
        var secondY = pDistanceCalculationSecondCard.transform.position.y;
        return Mathf.Abs(firstY - secondY);
    }

    private float GetPointerCardOffsetWhenMovingCard()
    {
        var firstY = pPointerOffsetDistanceCalculationFirstCard.transform.position.y;
        var secondY = pPointerOffsetDistanceCalculationSecondCard.transform.position.y;
        return Mathf.Abs(firstY - secondY);
    }
}
