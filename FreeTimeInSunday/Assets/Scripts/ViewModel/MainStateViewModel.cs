public class MainStateViewModel
{
    public enum State
    {
      PlayerMove,
      DecideEventIcon,
      DecideEvent,
      Event,
      Result
    }

    private  State _state;
    public State state => _state;

    public MainStateViewModel()
    {
      _state = State.PlayerMove;
    }

    public void SetState(State state)
      => _state = state;
}
