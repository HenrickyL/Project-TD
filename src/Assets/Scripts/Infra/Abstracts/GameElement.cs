using Perikan.Infra.Factory;
using UnityEngine;
using Perikan.Infra.GameStateManagement;

namespace Perikan.Infra.Gameplay
{
    [SelectionBase]
    public abstract class GameElement : GameAsset
    {
        private StateMachine _stateMachine = null;
        protected StateMachine StateMachine
        {
            get
            {
                if (_stateMachine == null)
                    CreateStateMachine();
                return _stateMachine;
            }
        }

        
        public float Scale { get; protected set; }

        /* --------------------------------------------------- */

        private void CreateStateMachine()
        {
            _stateMachine = new StateMachine();
        }

        /* --------------------------------------------------- */

        public override void GameUpdate()
        {
            base.GameUpdate();
            _stateMachine?.UpdateState();
        }

        public void ChangeState(BaseState state)
        {
            StateMachine.ChangeState(this, state);
        }
    }
}