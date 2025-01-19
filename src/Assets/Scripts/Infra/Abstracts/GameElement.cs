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

        private AbstractGameAssetFactory _originFactory;
        public AbstractGameAssetFactory OriginFactory
        {
            get => _originFactory;
            set
            {
                Debug.Assert(_originFactory == null, "Redefined origin factory!");
                _originFactory = value;
            }
        }

        public float Scale { get; protected set; }

        /* --------------------------------------------------- */

        private void CreateStateMachine()
        {
            _stateMachine = new StateMachine();
        }

        /* --------------------------------------------------- */

        public virtual void GameUpdate()
        {
            _stateMachine?.UpdateState();
        }

        public override void Recycle()
        {
            OriginFactory.Reclaim(this);
        }

        public void ChangeState(BaseState state)
        {
            StateMachine.ChangeState(this, state);
        }
    }
}