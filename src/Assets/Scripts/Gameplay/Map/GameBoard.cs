using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    Transform _ground = default;
    [SerializeField]
    GameTile _tilePrefab = default;

    private GameTileContentFactory _contentFactory;

    private GameTile[] _tiles;

    Vector2Int _size;

    Vector2 _offset = default;

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

        //yield return TileSearch.FindPathsEnumerator(_tiles);
        //yield return ToggleDestination(_tiles[_tiles.Length / 2]);

        //Perikan.IA.Node<GameTile> node = Perikan.IA.SearchMethods.BreadthFirstSearch<GameTile>(_tiles[0], _tiles.Last());
        //Debug.Log(">>>>>>>>>>>>>>>> Path");
        //StartCoroutine(TileSearch.FindPath(node));
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
            tile.Content = _contentFactory.Get(GameTileContentType.Empty);
           TileSearch.FindPath(_tiles);
        }
        else
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Destination);
            tile.Content.transform.Translate(new Vector3(0, 0.01f));
            if (!(TileSearch.FindAllDestinations(_tiles).Count == 0))
            {
                TileSearch.FindPath(_tiles);
            }
        }
    }
} 
