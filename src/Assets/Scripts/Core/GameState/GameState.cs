using UnityEngine;

public class GameState : IGameState
{
    private GameController gameController;

    public void Enter()
    {
        // Inicializa o controlador do jogo
        gameController = GameController.Instance;
        gameController.InitializeGame();  // Gera o mapa e configura o jogo

        this.SetupEnter();
    }

    public void Exit()
    {
        Debug.Log("Exiting Game State");
    }

    public void UpdateGame()
    {
        KeyOpenResumeMenu();

        gameController.UpdateGame();
    }

    /*--------------------------------*/
    private void KeyOpenResumeMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.ChangeToMenuState();
        }
    }
    private void SetupEnter() {
        Time.timeScale = 1;  // Retoma o tempo normal do jogo
        UIManager.Instance.Hide();
        Debug.Log("Entering Game State");
    }
}
