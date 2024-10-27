using UnityEngine;

public class GameManager : MonoBehaviour //Singleton
{
    private IGameState currentState;
    public static GameManager Instance { get; private set; }

    private GameState gameState;
    private MenuState menuState;

    private bool OnInitialize { get; set; } = false;



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

        if (!OnInitialize) { 
            if (gameState == null)
                gameState = new GameState();
            if (menuState == null)
                menuState = new MenuState();
            UIManager.Instance.Initialize();
            OnInitialize = true;
        }
        UIManager.Instance.OnEnable = true;
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
        if (UIManager.Instance.OnEnable) { 
            currentState?.UpdateGame();
        }
    }

    private void ChangeState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
