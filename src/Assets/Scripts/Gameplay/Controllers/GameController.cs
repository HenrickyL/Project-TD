using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);
    [SerializeField]
    GameBoard board = default;


    private void Awake()
    {
        AwakeSingleton();
    }
    
    private void AwakeSingleton() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Inicializa o jogo e gera o mapa
    public void InitializeGame()
    {
        StartCoroutine(MapGenerator.GenerateMap(board, boardSize));

        SetupGameElements();  // Configura elementos adicionais, como defesas e inimigos

        Debug.Log("GameController Initalized");
    }

    // Configura outros elementos do jogo
    private void SetupGameElements()
    {
        // Configura elementos do jogo, como defesas, inimigos, etc.
        Debug.Log("Configuring game elements.");
    }

    // Loop principal do jogo
    public void UpdateGame()
    {
        // Atualizações do loop principal do jogo
        // Exemplo: Atualizar a lógica dos inimigos e defesas
    }
}
