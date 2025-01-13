using UnityEngine;

public class GameTileContent : GameAsset
{
    [SerializeField]
    private GameTileContentType _type = default;
    public GameTileContentType Type => _type;

    public GameTile Tile { get; set; } = null;

    [SerializeField]
    private TileElement _tileElement = default;

    public TileElement Element => _tileElement;

    public bool BlocksPath =>
        Type == GameTileContentType.Wall || Type == GameTileContentType.Tower;

}
