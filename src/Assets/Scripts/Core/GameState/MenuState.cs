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
    }

    /*--------------------------------*/
    private void KeySwapMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.ChangeResume();
        }
    }
}
