using System;
using System.Collections;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    Transform _ground = default;
    [SerializeField]
    GameTile _tilePrefab = default;

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

    public IEnumerator Initialize(Vector2Int size)
    {
        this._size = size;
        _ground.localScale = new Vector3(size.x, size.y, 1f);

        _tiles = new GameTile[size.x * size.y];

        for (int i = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, i++)
            {
                CreateTile(x, y, i);
            }
        }

        yield return TileSearch.FindPathsEnumerator(_tiles);

        //Perikan.IA.Node<GameTile> node = Perikan.IA.SearchMethods.BreadthFirstSearch<GameTile>(_tiles[0], _tiles.Last());
        //Debug.Log(">>>>>>>>>>>>>>>> Path");
        //StartCoroutine(TileSearch.FindPath(node));
    }

    private void CreateTile(int x, int y, int i) {
        GameTile tile = _tiles[i] = Instantiate(_tilePrefab);
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
} 
