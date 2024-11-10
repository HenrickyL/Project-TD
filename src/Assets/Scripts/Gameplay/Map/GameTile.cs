using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField]
    Transform arrow = default;

    private GameTile[] _neighbors;

    public GameTile North { get { return _neighbors[(int)Direction.north]; } set { _neighbors[(int)Direction.north] = value; } }
    public GameTile East { get { return _neighbors[(int)Direction.east]; } set { _neighbors[(int)Direction.east] = value; } }
    public GameTile South { get { return _neighbors[(int)Direction.south]; } set { _neighbors[(int)Direction.south] = value; } }
    public GameTile West { get { return _neighbors[(int)Direction.west]; } set { _neighbors[(int)Direction.west] = value; } }



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




}
