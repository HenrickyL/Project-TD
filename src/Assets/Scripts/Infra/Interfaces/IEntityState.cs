using Perikan.Infra.Gameplay;

namespace Perikan.Infra.GameStateManagement
{
    public interface IEntityState
    { 
        void Enter(GameElement context);
        void UpdateState();
        void Exit();
        string Name();
    }

    public abstract class BaseState : IEntityState
    {
        private GameElement _entity = default;
        private string _name = default;

        protected GameElement Entity => _entity;

        public BaseState(string name) { 
            _name = name;
        }
        public virtual void Enter(GameElement context) {
            _entity = context;
        }
        public virtual void UpdateState() { }
        public virtual void Exit() { }
        public string Name() {
            return _name;
        }
    }
}