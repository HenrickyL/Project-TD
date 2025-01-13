public abstract class AEnemyState : IEntityState
{
    string _name = "default";
    protected private Enemy enemy;
    protected AnimationStateController animationController;

    public AEnemyState(string name) { 
        _name = name;
    }

    public virtual void Enter(GameEntity entity) { 
        enemy  = entity as Enemy;
        animationController = enemy.AnimationController;
    }

    public virtual void Exit() { }

    public virtual void UpdateState() { }

    public string Name()
    {
        return _name;
    }
}
