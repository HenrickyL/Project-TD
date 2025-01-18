using Perikan.Infra.Gameplay;

namespace Perikan.Infra.GameStateManagement
{
    public interface IEntityState
    { 
        void Enter(GameAsset context);
        void UpdateState();
        void Exit();
        string Name();
    }

    public abstract class BaseState : IEntityState
    {
        private GameAsset _entity = default;
        private string _name = default;

        protected GameAsset Entity => _entity;

        public BaseState(string name) { 
            _name = name;
        }
        public virtual void Enter(GameAsset context) {
            _entity = context;
        }
        public virtual void UpdateState() { }
        public virtual void Exit() { }
        public string Name() {
            return _name;
        }
    }
}