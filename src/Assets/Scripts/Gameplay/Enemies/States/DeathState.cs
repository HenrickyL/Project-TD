using Perikan.Infra.Animation;
using Perikan.Infra.Gameplay;
using System.Collections;
using UnityEngine;

namespace Perikan.Gameplay.EnemyState { 
    public class DeathState : AEnemyState
    {
        private MonoBehaviour _coroutineHost;

        public DeathState(MonoBehaviour coroutineHost) : base("Death") {
            _coroutineHost = coroutineHost;
        }

        public override void Enter(GameAsset entity)
        {
            base.Enter(entity);
            _coroutineHost.StartCoroutine(HandleDeath());
        }

        private IEnumerator HandleDeath() {
            enemy.SetDeath();
            animationController.ChangeAnimator(AnimationStateEnum.Death);
            yield return new WaitForSeconds(animationController.GetAnimationLength(AnimationTypeEnum.Death) + 0.15f);
            Entity.Recycle();
        }
    }
}