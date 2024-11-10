using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField]
    TileArrow _arrow;

    private GameTile[] _neighbors;

    public GameTile[] Neighbors { get { return _neighbors; } }

    public GameTile North { get { return _neighbors[(int)Direction.North]; } set { _neighbors[(int)Direction.North] = value; } }
    public GameTile East { get { return _neighbors[(int)Direction.East]; } set { _neighbors[(int)Direction.East] = value; } }
    public GameTile South { get { return _neighbors[(int)Direction.South]; } set { _neighbors[(int)Direction.South] = value; } }
    public GameTile West { get { return _neighbors[(int)Direction.West]; } set { _neighbors[(int)Direction.West] = value; } }
    GameTile _nextOnPath;
    int _distance;
    public bool HasPath => _distance != int.MaxValue;

    
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
        return neighbor;
    }

    /* -------------------------------------------------------------- */


    public void MakeAboveNeighbors(
        GameTile above
    )
    {
        Debug.Assert(
            this.North == null && above.South == null, "Redefined neighbors!"
        );
        this.North = above;
        above.South = this;
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
        _arrow.SetActive( false );
    }

    public void BecomeDestination()
    {
        _distance = 0;
        _nextOnPath = null;
    }


    public void ShowPath() {
        if (_distance == 0)
        {
            _arrow.gameObject.SetActive(false);
            return;
        }

        for (int dir = (int)Direction.North; dir <= (int)Direction.West; dir++) {
            GameTile tile = _neighbors[dir];
            if (_nextOnPath == tile) {
                _arrow.RotateTo((Direction)dir);
            }
        }
    }

    public void SetEnableArrow(bool value) { 
        _arrow.SetActive(value);
    }
}
