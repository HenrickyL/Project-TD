using System.Collections;
using UnityEngine;

public class DeathState : AEnemyState
{
    private MonoBehaviour _coroutineHost;

    public DeathState(MonoBehaviour coroutineHost) : base("Death") {
        _coroutineHost = coroutineHost;
    }

    public override void Enter(GameEntity entity)
    {
        base.Enter(entity);
        _coroutineHost.StartCoroutine(HandleDeath());
    }


    private IEnumerator HandleDeath() {
        enemy.IsAlive = false;
        animationController.ChangeAnimator(AnimationStateEnum.Death);
        yield return new WaitForSeconds(animationController.GetAnimationLength(AnimationTypeEnum.Death) + 0.15f);
        enemy.Recycle();
    }
}
