using Perikan.Infra.GameStateManagement;
using Perikan.Infra.Animation;
using Perikan.Infra.Gameplay;
using Perikan.Gameplay.Entity;

namespace Perikan.Gameplay.EnemyState {
    public abstract class AEnemyState : BaseState
    {
        protected AnimationStateController animationController;
        protected Enemy enemy => (Enemy)Entity;

        public AEnemyState(string name): base(name) { 
        }

        public override void Enter(GameElement entity) { 
            base.Enter(entity);
            animationController = enemy.AnimationController;
        }
    }
}