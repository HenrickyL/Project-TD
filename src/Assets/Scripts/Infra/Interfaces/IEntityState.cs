using System.Collections;

public interface IEntityState { 
    void Enter(GameEntity entity);
    void UpdateState();
    void Exit();
}
