public interface IEntityState { 
    void Enter(GameEntity entity);
    void UpdateState();
    void Exit();
    string Name();
}
