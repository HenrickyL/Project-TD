using System.Collections;

public abstract class AEnemyState : IEntityState
{
    protected private Enemy enemy;
    protected AnimationStateController animatorController;

    public virtual void Enter(GameEntity entity) { 
        enemy  = entity as Enemy;
        animatorController = enemy.AnimationController;
    }

    public virtual void Exit() { }

    public abstract IEnumerator UpdateState();
}
