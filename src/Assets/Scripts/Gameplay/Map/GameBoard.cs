using System.Drawing;
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

    public void Initialize(Vector2Int size)
    {
        this._size = size;
        _ground.localScale = new Vector3(size.x, size.y, 1f);

        _tiles = new GameTile[size.x * size.y];

        for (int i=0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, i++)
            {
                CreateTile(x, y, i);
            }
        }
    }

    private void CreateTile(int x, int y, int i) {
        GameTile tile = _tiles[i] = Instantiate(_tilePrefab);
        tile.transform.SetParent(transform, false);
        tile.transform.localPosition = new Vector3(
            x - Offset.x, 
            0f,
            y- Offset.y
        );

        if (x > 0)
        {
            tile.MakeLeftNeighbors(_tiles[i - 1]);
        }
        if (y > 0)
        {
            tile.MakeAboveNeighbors( _tiles[i - _size.x]);
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
} 
