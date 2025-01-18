using Perikan.Gameplay.Entity;
using Perikan.Infra.GameStateManagement;

namespace Perikan.Gameplay.TowerState {

    public class ATowerState : BaseState
    {
        protected Tower tower => (Tower)Entity;
        public ATowerState(string name) : base(name)
        {}
    }
}