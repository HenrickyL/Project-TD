using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameTile : MonoBehaviour
{
    [SerializeField]
    Transform arrow = default;

    private GameTile[] _neighbors;

    public GameTile North { get { return _neighbors[(int)Direction.north]; } set { _neighbors[(int)Direction.north] = value; } }
    public GameTile East { get { return _neighbors[(int)Direction.east]; } set { _neighbors[(int)Direction.east] = value; } }
    public GameTile South { get { return _neighbors[(int)Direction.south]; } set { _neighbors[(int)Direction.south] = value; } }
    public GameTile West { get { return _neighbors[(int)Direction.west]; } set { _neighbors[(int)Direction.west] = value; } }

    GameTile _nextOnPath;
    int _distance;
    public bool HasPath => _distance != int.MaxValue;

    private void Awake()
    {
        _neighbors = new GameTile[(int)Direction.west+1];
    }


    public void MakeAboveNeighbors(
        GameTile above
    ){
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

    public void CleanPath() { 
        _distance = int.MaxValue;
        _nextOnPath = null;
    }

    public void BecomeDestination()
    {
        _distance = 0;
        _nextOnPath = null;
    }



}
