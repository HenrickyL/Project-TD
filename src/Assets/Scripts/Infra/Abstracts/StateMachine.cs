using Perikan.Infra.Gameplay;

namespace Perikan.Infra.GameStateManagement
{
    public sealed class StateMachine
    {
        private BaseState _currentState;
        private GameElement _context;

        public BaseState CurrentState { get { return _currentState; } }
        public GameElement Context { get { return _context; } }

        public string CurrentStateName => _currentState.Name();

        public void UpdateState()
        {
            _currentState?.UpdateState();
        }

        public void ChangeState(GameElement context, BaseState newState)
        {
            _context = context;
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter(_context);
        }
    }
}