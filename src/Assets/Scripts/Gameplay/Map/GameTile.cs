using Perikan.AI;
using System;
using UnityEngine;
public class GameTile : MonoBehaviour, IState<GameTile>, IEquatable<GameTile>
{
    //[SerializeField]
    //TileArrow _arrow;
    [SerializeField]
    private GameTile[] _neighbors;

    GameTileContent _content;

    public bool onWall => Content.Type == GameTileContentType.Wall;

    public GameTileContent Content
    {
        get => _content;
        set
        {
            Debug.Assert(value != null, "Null assigned to content!");
            if (_content != null)
            {
                _content.Recycle();
            }
            _content = value;
            // Definir o pai da instância
            _content.transform.SetParent(this.transform, false);
        }
    }

    public TileArrow Arrow { get; set; }

    public GameTile[] Neighbors {
        get {
            return IsAlternative ?
                new GameTile[] { North, South, East, West } :
                new GameTile[] { West, East, South, North };
        }
    }

    public GameTile North { get { return _neighbors[(int)Direction.North]; } set { _neighbors[(int)Direction.North] = value; } }
    public GameTile East { get { return _neighbors[(int)Direction.East]; } set { _neighbors[(int)Direction.East] = value; } }
    public GameTile South { get { return _neighbors[(int)Direction.South]; } set { _neighbors[(int)Direction.South] = value; } }
    public GameTile West { get { return _neighbors[(int)Direction.West]; } set { _neighbors[(int)Direction.West] = value; } }
    [SerializeField]
    GameTile _nextOnPath;
    [SerializeField]
    int _distance;

    public int Distance { get { return _distance; } }
    public bool HasPath => _distance != int.MaxValue;

    private Vector2Int _position;
    public bool IsAlternative { get; set; }


    /* -------------------------------------------------------------- */

    private void Awake()
    {
        _neighbors = new GameTile[(int)Direction.West + 1];
    }
    public GameTile GrowPathTo(GameTile neighbor)
    {
        Debug.Assert(HasPath, "No path!");
        //if (neighbor == null || neighbor.HasPath)
        //{
        //    return null;
        //}
        neighbor._distance = _distance + 1;
        neighbor._nextOnPath = this;
        return !neighbor.onWall ? neighbor : null;
    }

    /* -------------------------------------------------------------- */


    public void MakeBelowNeighbors(GameTile below)
    {
        Debug.Assert(
            this.North == null && below.South == null, "Redefined neighbors!"
        );
        this.South = below;
        below.North = this;
    }

    public void MakeLeftNeighbors(
        GameTile left
    )
    {
        Debug.Assert(
            this.West == null && left.East == null, "Redefined neighbors!"
        );
        this.West = left;
        left.East = this;
    }

    public void ClearPath()
    {
        _distance = int.MaxValue;
        _nextOnPath = null;
        if (Content.Element.IsActive)
        {
            Content.Element.Toggle();
        }
    }

    public void BecomeDestination()
    {
        _distance = 0;
        _nextOnPath = null;
    }

    public GameTile Next() {
        return _nextOnPath;
    }

    public void ShowPath() {
        if (_distance == 0)
        {
            //_arrow.gameObject.SetActive(false);
            return;
        }
        for (int dir = (int)Direction.North; dir <= (int)Direction.West; dir++)
        {
            GameTile tile = _neighbors[dir];
            if (_nextOnPath == tile && Content.Element is TileArrow)
            {
                TileArrow arrow = Content.Element as TileArrow;
                arrow.RotateTo((Direction)dir);
            }
        }
    }

    public void SetEnableArrow(bool value) { 
        if(Content.Element.IsActive != value)
        {
            Content.Element.Toggle();
        }
    }

    public float DistanceTo(GameTile tile) { 
        return Vector3.Distance(this.transform.position, tile.transform.position);
    }

    GameTile[] IState<GameTile>.GetChilds()
    {
        return _neighbors;
    }

    public bool Equals(GameTile other)
    {
        return _position.x == other._position.x && _position.y == other._position.y;
    }

    public void SetPosition(Vector2Int pos) {
        _position = pos;
    }
}
