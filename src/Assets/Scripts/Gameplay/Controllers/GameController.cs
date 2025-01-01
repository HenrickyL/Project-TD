using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);
    
    [SerializeField]
    GameBoard board = default;

    [SerializeField]
    Material _arrowEnable = default;

    [SerializeField]
    Material _arrowDisable = default;
    public static Material ArrowMaterial { get { return Instance._arrowEnable; } }
    public static Material ArrowDisableMaterial { get { return Instance._arrowDisable; } }

    [SerializeField]
    GameTileContentFactory tileContentFactory = default;

    Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);


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

    private void SaveBoard() {
        DontDestroyOnLoad(board);
    }

    // Inicializa o jogo e gera o mapa
    public void InitializeGame()
    {

        StartCoroutine(MapGenerator.GenerateMap(board, tileContentFactory, boardSize));
        //MapGenerator.Generate(board, boardSize);
        SaveBoard();

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
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }
    }

    /* ----------------------------------------------------- */
    private void HandleTouch()
    {
        GameTile tile = board.GetTile(TouchRay);
        if (tile != null && tile.Content.Type != GameTileContentType.Destination)
        {
            //tile.Content = tileContentFactory.Get(GameTileContentType.Destination);
            //StartCoroutine(
            board.ToggleDestination(tile);
                //);
        }
    }

}
