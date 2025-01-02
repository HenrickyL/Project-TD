using UnityEngine;

public class GameTileContent : MonoBehaviour
{
    [SerializeField]
    private GameTileContentType _type = default;
    public GameTileContentType Type => _type;

    private GameTileContentFactory _originFactory;

    public GameTileContentFactory OriginFactory
    {
        get => _originFactory;
        set
        {
            Debug.Assert(_originFactory == null, "Redefined origin factory!");
            _originFactory = value;
        }
    }

    public void Recycle()
    {
        _originFactory.Reclaim(this);
    }
}
