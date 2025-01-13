using UnityEngine;

[CreateAssetMenu(fileName = "GameTileContentFactory", menuName = "Factory/GameTileContentFactory")]
public class GameTileContentFactory : AbstractGameAssetFactory
{

    [SerializeField]
    GameTileContent destinationPrefab = default;

    [SerializeField]
    GameTileContent emptyPrefab = default;

    [SerializeField]
    GameTileContent wallPrefab = default;

    [SerializeField]
    GameTileContent spawnPoint = default;

    [SerializeField]
    Tower towerPrefab = default;


    /* --------------------------------------------------------------------------------- */

    private GameTileContent Get(GameTileContent prefab)
    {
        return base.Get<GameTileContent>(prefab);
    }

    public GameTileContent Get(GameTileContentType type)
    {
        switch (type)
        {
            case GameTileContentType.Destination: return Get(destinationPrefab);
            case GameTileContentType.Empty: return Get(emptyPrefab);
            case GameTileContentType.Wall: return Get(wallPrefab);
            case GameTileContentType.SpawnPoint:return Get(spawnPoint);
            case GameTileContentType.Tower: return Get(towerPrefab);
        }
        Debug.Assert(false, "Unsupported type: " + type);
        return null;
    }
  
}