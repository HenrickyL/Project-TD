using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);
    
    [SerializeField]
    GameBoard _board = default;

    [SerializeField]
    Material _arrowEnable = default;

    [SerializeField]
    Material _arrowDisable = default;
    public static Material ArrowMaterial { get { return Instance._arrowEnable; } }
    public static Material ArrowDisableMaterial { get { return Instance._arrowDisable; } }

    [SerializeField]
    GameTileContentFactory tileContentFactory = default;

    [SerializeField]
    EnemyFactory enemyFactory = default;

    [SerializeField, Range(0.1f, 10f)]
    float spawnSpeed = 1f;

    float _spawnProgress = 0;

    Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

    EnemyCollection enemies = new EnemyCollection();


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
        DontDestroyOnLoad(_board);
    }

    // Inicializa o jogo e gera o mapa
    public void InitializeGame()
    {
        StartCoroutine(MapGenerator.GenerateMap(_board, tileContentFactory, boardSize));
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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                HandleAlternativeTouch();
            }
            else if (Input.GetKey(KeyCode.LeftControl)) {
                HandleSpawnTouch();
            }
            else
            {
                HandleTouch();
            }
        }

        if (Input.GetKeyDown(KeyCode.G)){
             _board.ShowGrid = !_board.ShowGrid;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            _board.ShowPaths = !_board.ShowPaths;
        }

        UpdateSpawn();
        UpdateEnemies();
    }


    /* ----------------------------------------------------- */

    private void UpdateSpawn() {
        _spawnProgress += spawnSpeed * Time.deltaTime;
        while (_spawnProgress >= 1f)
        {
            _spawnProgress -= 1f;
            SpawnEnemy();
        }
    }

    private void UpdateEnemies() { 
        enemies.GameUpdate();
    }
    private void HandleAlternativeTouch()
    {
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null){
            _board.ToggleDestination(tile);
        }
    }

    private void HandleSpawnTouch() { 
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null)
        {
            _board.ToggleSpawnPoint(tile);
        }
    }

    private void HandleTouch() {
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null) {
            _board.ToggleWall(tile);
        }
    }

    private void SpawnEnemy() {
        if (_board.SpawnPointCount > 0 && _board.HasDestinations) { 
            GameTile spawnPoint =
                _board.GetSpawnPoint(Random.Range(0, _board.SpawnPointCount));
            Enemy enemy = enemyFactory.Get();
            enemy.SpawnOn(spawnPoint);
            enemies.Add(enemy);
        }
    }
}
