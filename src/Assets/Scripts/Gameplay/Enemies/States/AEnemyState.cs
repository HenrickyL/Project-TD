public abstract class AEnemyState : BaseState<Enemy>
{
    protected AnimationStateController animationController;

    public AEnemyState(string name): base(name) { 
    }

    public override void Enter(Enemy entity) { 
        base.Enter(entity);
        animationController = Entity.AnimationController;
    }
}
