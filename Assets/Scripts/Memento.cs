public class Memento
{
    private IMementable _state;

    public IMementable State { get => _state; }

    public Memento(IMementable state) => _state = state;
}
