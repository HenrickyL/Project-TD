using Perikan.Infra.Animation;
using Perikan.Infra.GameStateManagement;
using UnityEngine;

namespace Perikan.Infra.Gameplay
{
    [SelectionBase]
    public abstract class GameElement : GameAsset
    {
        [SerializeField]
        AnimationStateController _animController;
        public AnimationStateController AnimationController
        {
            get => _animController;
            protected set { _animController = value; }
        }

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