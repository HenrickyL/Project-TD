using Perikan.Infra.Animation;
using Perikan.Infra.Gameplay;
using System;
using System.Collections;
using UnityEngine;

namespace Perikan.Gameplay.EnemyState { 
    public class HitState : AEnemyState
    {
        private MonoBehaviour _coroutineHost;
        private float _damage = 0f;

        Action<float> _callback;

        public HitState(MonoBehaviour coroutineHost, float damage, Action<float> hitCallback) : base("Hit")
        {
            _coroutineHost = coroutineHost;
            _damage = damage;
            _callback = hitCallback;
        }

        public void SetDamage(float damage) { 
            _damage = damage;
        }


        public override void Enter(GameElement entity)
        {
            base.Enter(entity);
            _callback(_damage);
            _coroutineHost.StartCoroutine(HandleHit());
            //TODO: check another way so you don't have to create new states.
        }

  
        public override void Exit()
        {
            base.Exit();
            animationController.ChangeAnimator(AnimationStateEnum.Hit, 0);
        }


        private IEnumerator HandleHit()
        { 
            animationController.ChangeAnimator(AnimationStateEnum.Hit, 1);
            yield return new WaitForSeconds(animationController.GetAnimationLength(AnimationTypeEnum.Hit) + 0.15f);

            if (enemy.IsAlive)
            {
                Entity.ChangeState(new MovimentState());
            }
            else
            {
                Entity.ChangeState(new DeathState(Entity));
            }
        }
    
    }
}