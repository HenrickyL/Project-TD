using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //Singleton
{
    private IGameState currentState;
    public static GameManager Instance { get; private set; }

    private GameState gameState;
    private MenuState menuState;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Mantém o GameManager ao mudar de cena
            DontDestroyOnLoad(gameObject);
        }
        else { 
            Destroy(gameObject);
        }
        Debug.Log("--->Awake GameManager");
    }

    /*-----------------------------*/

    public void ChangeToGameState() {
        ChangeState(gameState);
    }
    public void ChangeToMenuState()
    {
        ChangeState(menuState);
    }
    public void GoToMainMenu() {
        ChangeToGameState();
        UIManager.Instance.Hide();
        SceneManager.LoadScene(ScenesEnum.MainMenuScene.ToString());
    }

    /*-----------------------------*/
    
    private void Start()
    {
        if (gameState == null)
            gameState = new GameState();
        if (menuState == null)
            menuState = new MenuState();
        ChangeState(gameState);
    }
    private void Update()
    {
        currentState?.UpdateGame();
    }

    private void ChangeState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
