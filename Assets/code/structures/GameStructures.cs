using System.Collections.Generic;

public interface IInitialiser
{
    void OnInit();
}
public interface IUpdater
{
    void OnUpdate(float dt);
}

public enum TablePosition
{
	Foundation0,
	Foundation1,
	Foundation2,
	Foundation3,

	Tableu0,
	Tableu1,
	Tableu2,
	Tableu3,
	Tableu4,
	Tableu5,
	Tableu6,

	Stock,
	StockOpen
}

public enum Suite
{
	Spades,
	Hearts,
	Clubs,
	Diamonds
}

public struct ReadableCard
{
	public Suite mSuite;
	public int mValue;
}

public struct Card
{
	public int mCardId;

	public TablePosition mPosition;
	public int mIndex;
	public bool mIsShowing;

	public static Card Copy(Card other)
    {
		return new Card
		{
			mCardId = other.mCardId,
			mPosition = other.mPosition,
			mIndex = other.mIndex,
			mIsShowing = other.mIsShowing
		};
	}
}
public interface IReadTableState
{
	List<Card> GetState();
	Card GetCard(int cardId);
}
public interface IWriteTableState
{
	void Initialise(List<Card> cards);
	void UpdateState(List<Card> cards);
	void UpdateState(Card card);
}