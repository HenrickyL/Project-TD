using Perikan.Gameplay.Entity;
using Perikan.Gameplay.Entity.Tower;
using Perikan.Gameplay.Factory;
using Perikan.Gameplay.Generator;
using Perikan.Gameplay.Map;
using Perikan.Infra.Collections;
using Perikan.Infra.Gameplay;
using UnityEditor;
using UnityEngine;

namespace Perikan.Gameplay.Controller { 
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

        GameAssetCollection enemies = new GameAssetCollection();
        GameAssetCollection nonEnemies = new GameAssetCollection();

        TowerType selectedTowerType;


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
            SelectTowerType();
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    HandleTower();
                }
                else if (Input.GetKey(KeyCode.LeftControl)) {
                    HandleSpawnTouch();
                }
                else
                {
                    HandleWall();
                }
            }

            if (Input.GetMouseButtonDown(1)) {
                HandleDestination();
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
            Physics.SyncTransforms();
            _board.GameUpdate();
            nonEnemies.GameUpdate();
        }


        public static void AddProjectile(Projectile projectile)
        {
            Instance.nonEnemies.Add(projectile);
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
        private void HandleDestination()
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

        private void HandleWall() {
            GameTile tile = _board.GetTile(TouchRay);
            if (tile != null) {
                _board.ToggleWall(tile);
            }
        }

        private void HandleTower()
        {
            GameTile tile = _board.GetTile(TouchRay);
            if (tile != null)
            {
                _board.ToggleTower(tile, selectedTowerType);
            }
        }

        private void SelectTowerType() {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedTowerType = TowerType.Laser;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedTowerType = TowerType.Mortar;
            }
        }

        private void SpawnEnemy() {
            if (_board.SpawnPointCount > 0 && _board.HasDestinations) {
                GameTile spawnPoint =
                    //_board.GetSpawnPoint(Random.Range(0, _board.SpawnPointCount));
                    _board.GetRandomSpawnPoint();
                if (spawnPoint != null) { 
                    Enemy enemy = enemyFactory.Get();
                    enemy.SpawnOn(spawnPoint);
                    enemies.Add(enemy);
                }
            }
        }
    }
}