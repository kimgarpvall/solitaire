using System.Linq;
public class TryWinningGameCommand : Command
{
    private readonly IReadTableState mReadTableState;
    public TryWinningGameCommand()
    {
        mReadTableState = Dependencies.Get().Get<IReadTableState>();
    }

    public override CommandUndoBehaviour Execute()
    {
        var cards = mReadTableState.GetState();
        var foundNonFoundationCard = cards.Any(p =>
        {
            return !ModelUtils.FoundationPositions.Contains(p.mPosition);
        });

        if(!foundNonFoundationCard)
        {
            Dependencies.Get().Get<EventBus>().PostMessage(new StartGameEvent());
        }

        return CommandUndoBehaviour.DoNotAllowUndo;
    }

    public override void Undo()
    {
        
    }
    public override void Redo()
    {
    }
}
