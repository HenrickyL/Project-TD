using UnityEngine;

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
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize() {
        
        if (gameState == null)
            gameState = new GameState();
        if (menuState == null)
            menuState = new MenuState();
        UIManager.Instance.Initialize();
        ChangeState(gameState);
    }

    /*-----------------------------*/

    public void ChangeToGameState() {
        ChangeState(gameState);
    }

    public void ChangeResume() { 
        ChangeState(gameState);
        UIManager.Instance.Hide();
    }

    public void ChangeToMenuState()
    {
        ChangeState(menuState);
    }

    /*-----------------------------*/
    
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
