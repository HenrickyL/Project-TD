using UnityEngine;

public class GameState : IGameState
{
    public void Enter()
    {
        Debug.Log("Entering Game State");
        Time.timeScale = 1;  // Retoma o tempo normal do jogo
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
