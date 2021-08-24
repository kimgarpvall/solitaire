public class UpdateStockHelper
{
    private readonly IInputController mInputController;
    private readonly EventBus mEventBus;

    public UpdateStockHelper()
    {
        mInputController = Dependencies.Get().Get<IInputController>();
        mEventBus = Dependencies.Get().Get<EventBus>();
    }

    public void TryTakingCardFromStock()
    {
        if (mInputController.GetMainInput() == null)
            return;

        var input = mInputController.GetMainInput().Value;
        if (input.mInputState == InputState.Down && UIUtils.GetGameObject(input.mPosition, UIUtils.Masks.Stock) != null)
            mEventBus.PostMessage(new TakeCardFromStockEvent());
    }
}
