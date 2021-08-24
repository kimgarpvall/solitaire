using System.Collections.Generic;

public class StartGameEvent : Event { }
public class SetupDeckViewEvent : Event { }
public class ModelStateUpdatedEvent : Event { }
public class FlipCardsEvent : Event { }
public class TakeCardFromStockEvent : Event { }
public class TryPlaceCardsEvent : Event
{
    public TablePosition mPositionToPlace;
    public List<Card> mCardsToPlace;
}