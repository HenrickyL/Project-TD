using Perikan.Infra.GameStateManagement;

namespace Perikan.Gameplay.Entity.Tower.Mortar.States{

    public class ATowerState : BaseState
    {
        protected MortarTower tower => (MortarTower)Entity;
        public ATowerState(string name) : base(name)
        {}
    }
}