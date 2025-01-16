public sealed class StateMachine<T> where T : GameAsset
{
    private IEntityState<T> _currentState;
    private T _context;

    public IEntityState<T> CurrentState { get { return _currentState; } }
    public T Context { get { return _context; } }

    public string CurrentStateName => _currentState.Name();

    public void UpdateState()
    {
        _currentState?.UpdateState();
    }

    public void ChangeState(T context, BaseState<T> newState)
    {
        _context = context;
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(_context);
    }
}
