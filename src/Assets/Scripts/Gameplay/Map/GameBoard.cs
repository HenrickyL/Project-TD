using System;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    Transform _ground = default;
    [SerializeField]
    GameTile _tilePrefab = default;
    [SerializeField]
    Texture2D _gridTexture = default;

    private GameTileContentFactory _contentFactory;

    private GameTile[] _tiles;

    Vector2Int _size;

    Vector2 _offset = default;


    private bool _showPaths = false;
    private bool _showGrid = true;

    public bool ShowGrid
    {
        get => _showGrid;
        set
        {
            _showGrid = value;
            Material m = _ground.GetComponent<MeshRenderer>().material;
            if (_showGrid)
            {
                m.mainTexture = _gridTexture;
                //m.SetTexture("_MainTex", _gridTexture);
                m.SetTextureScale("_BaseMap", _size);
            }
            else
            {
                m.mainTexture = null;
            }
        }
    }

    public bool ShowPaths
    {
        get => _showPaths;
        set
        {
            _showPaths = value;
            ApplyShowPath();
        }
    }

    private List<GameTile> _spawnPoints = new();
    public int SpawnPointCount => _spawnPoints.Count;


    private void ApplyShowPath() {
        if (_showPaths)
        {
            foreach (GameTile tile in _tiles)
            {
                tile.ShowPath();
            }
        }
        else
        {
            foreach (GameTile tile in _tiles)
            {
                tile.HidePath();
            }
        }
    }

    public Vector2 Offset { get {
            _offset = new Vector2(
                (_size.x - 1) * 0.5f, (_size.y - 1) * 0.5f
            );
            return _offset;
        }
    }

    public void Initialize(Vector2Int size, GameTileContentFactory contentFactory)
    {
        //Clear();
        this._size = size;
        this._contentFactory = contentFactory;
        _ground.localScale = new Vector3(size.x, size.y, 1f);

        _tiles = new GameTile[size.x * size.y];

        for (int i = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, i++)
            {
                GameTile tile = CreateTile(x, y, i);
                tile.Content = _contentFactory.Get(GameTileContentType.Empty);
            }
        }
        //yield return T ileSearch.FindPathsEnumerator(_tiles);
        //yield return ToggleDestination(_tiles[_tiles.Length / 2]);

        //Perikan.IA.Node<GameTile> node = Perikan.IA.SearchMethods.BreadthFirstSearch<GameTile>(_tiles[0], _tiles.Last());
        //Debug.Log(">>>>>>>>>>>>>>>> Path");
        //StartCoroutine(TileSearch.FindPath(node));
        ApplyShowPath();
    }

    private GameTile CreateTile(int x, int y, int i) {
        GameTile tile = _tiles[i] = Instantiate(_tilePrefab);
        tile.IsAlternative = (x & 1) == 0;
        if ((y & 1) == 0)
        {
            tile.IsAlternative = !tile.IsAlternative;
        }

        tile.SetPosition(new Vector2Int(x, y));
        tile.transform.SetParent(transform, false);
        tile.transform.localPosition = new Vector3(
            x - Offset.x,
            0f,
            y - Offset.y
        );

        if (x > 0)
        {
            tile.MakeLeftNeighbors(_tiles[i - 1]);
        }
        if (y > 0)
        {
            tile.MakeBelowNeighbors(_tiles[i - _size.x]);
        }
        return tile;
    }

    private void OnValidate()
    {
        if (_size.x < 2)
        {
            _size.x = 2;
        }
        if (_size.y < 2)
        {
            _size.y = 2;
        }
    }

    private GameTile GetTile(int x, int y) {
        if (x < 0 || x > _size.x || y < 0 || y >= _size.y)
            throw new ArgumentOutOfRangeException("Invalid Coordinates");

        int index = x + y * _size.x;
        return _tiles[index];
    }

    public void SetEnable(bool value) { gameObject.SetActive(value); }

    public GameTile GetTile(Ray ray) {
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            int x = (int)(hit.point.x + _size.x * 0.5f);
            int y = (int)(hit.point.z + _size.y * 0.5f);
            if (x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            {
                return _tiles[x + y * _size.x];
            }
        }
        return null;
    }

    public void Clear() {
        if(_tiles.Length > 0)
        {
            foreach (GameTile tile in _tiles) {
                if(tile != null) Destroy(tile);
            }
        }
    }


    public void ToggleDestination(GameTile tile) {
        if (tile.Content.Type == GameTileContentType.Destination)
        {
            if (TileSearch.ExistDetination(_tiles))
            {
                tile.Content = _contentFactory.Get(GameTileContentType.Empty);
                FindPath();
            }
        }
        else if(tile.Content.Type == GameTileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Destination);
            tile.Content.transform.Translate(new Vector3(0, 0.01f));
            FindPath();
        }
    }

    public void ToggleWall(GameTile tile) {
        if (tile.Content.Type == GameTileContentType.Wall)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Empty);
            FindPath();
        }
        else if(tile.Content.Type == GameTileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Wall);
            if (TileSearch.ExistDetination(_tiles)) {
                FindPath();
            }
            //if (!TileSearch.FindPath(_tiles)) { 
            //    tile.Content = _contentFactory.Get(GameTileContentType.Empty);
            //}
        }
    }


    public void ToggleSpawnPoint(GameTile tile) {
        if (tile.Content.Type == GameTileContentType.SpawnPoint)
        {
            if (_spawnPoints.Count > 1) {
                _spawnPoints.Remove(tile);
                tile.Content = _contentFactory.Get(GameTileContentType.Empty);
            }
        }
        else if(tile.Content.Type == GameTileContentType.Empty){
            tile.Content = _contentFactory.Get(GameTileContentType.SpawnPoint);
            tile.Content.transform.Translate(new Vector3(0, 0.01f));
            _spawnPoints.Add(tile);
        }
    }

    public GameTile GetSpawnPoint(int index)
    {
        return _spawnPoints[index];
    }

    private bool FindPath() {
        return TileSearch.FindPath(_tiles, ShowPaths);
    }
} 
