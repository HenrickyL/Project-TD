using UnityEngine;

public class MenuState : IGameState
{
    public void Enter()
    {
        Time.timeScale = 0;
        UIManager.Instance.Show();
        Debug.Log("Entering Menu State");
    }

    public void Exit()
    {
        UIManager.Instance.Hide();
        Debug.Log("Exiting Menu State");
    }

    public void UpdateGame()
    {
        KeySwapMenu();

        //// Se o jogador escolher "Main Menu", muda a cena de volta para o menu principal
        //if (UIManager.Instance.IsMainMenuSelected())
        //{
        //    // Carrega a cena do menu principal
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        //}

        //// Se o jogador escolher "Resume", retorna ao jogo
        //if (UIManager.Instance.IsResumeSelected())
        //{
        //    GameManager.Instance.ChangeState(new GameState());
        //}
    }

    /*--------------------------------*/
    private void KeySwapMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.ChangeToGameState();
        }
    }
}
