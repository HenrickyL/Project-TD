using UnityEngine;

public class GameState : IGameState
{
    public void Enter()
    {
        Time.timeScale = 1;  // Retoma o tempo normal do jogo
        UIManager.Instance.Hide();
        Debug.Log("Entering Game State");
    }

    public void Exit()
    {
        Debug.Log("Exiting Game State");
    }

    public void UpdateGame()
    {
        KeyOpenResumeMenu();
    }

    /*--------------------------------*/
    private void KeyOpenResumeMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.ChangeToMenuState();
        }
    }
}
