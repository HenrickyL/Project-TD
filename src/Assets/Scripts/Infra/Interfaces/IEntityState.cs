using System.Collections;

public interface IEntityState { 
    void Enter(GameEntity entity);
    IEnumerator UpdateState();
    void Exit();
}
