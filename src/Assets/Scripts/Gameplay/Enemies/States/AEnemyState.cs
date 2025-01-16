public abstract class AEnemyState : BaseState
{
    protected AnimationStateController animationController;
    protected Enemy enemy => (Enemy)Entity;

    public AEnemyState(string name): base(name) { 
    }

    public override void Enter(GameAsset entity) { 
        base.Enter(entity);
        animationController = enemy.AnimationController;
    }
}
