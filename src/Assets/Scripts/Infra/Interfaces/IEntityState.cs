public interface IEntityState<T> where T: GameAsset
{ 
    void Enter(T context);
    void UpdateState();
    void Exit();
    string Name();
}


public abstract class BaseState<T> : IEntityState<T> where T : GameAsset
{
    private T _entity = default;
    private string _name = default;

    protected T Entity => _entity;

    public BaseState(string name) { 
        _name = name;
    }
    public virtual void Enter(T context) {
        _entity = context;
    }
    public virtual void UpdateState() { }
    public virtual void Exit() { }
    public string Name() {
        return _name;
    }
}