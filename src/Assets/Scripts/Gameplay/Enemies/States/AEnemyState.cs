public abstract class AEnemyState : IEntityState
{
    protected private Enemy enemy;
    protected AnimationStateController animationController;

    public virtual void Enter(GameEntity entity) { 
        enemy  = entity as Enemy;
        animationController = enemy.AnimationController;
    }

    public virtual void Exit() { }

    public virtual void UpdateState() { }
}
