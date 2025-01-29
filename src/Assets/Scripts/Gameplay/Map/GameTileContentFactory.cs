using Perikan.Gameplay.Entity.Tower;
using Perikan.Gameplay.Map;
using Perikan.Infra.Factory;
using Perikan.Infra.Gameplay;
using UnityEngine;

namespace Perikan.Gameplay.Factory
{
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
        GameTileContent[] towerPrefabs = default;

        /* --------------------------------------------------------------------------------- */

        private GameTileContent Get(GameTileContent prefab)
        {
            return base.Get<GameTileContent>(prefab);
        }

        public GameTileContent Get(TowerType type)
        {
            Debug.Assert((int)type < towerPrefabs.Length, "Unsupported tower type!");
            GameTileContent prefab = towerPrefabs[(int)type];
            Tower tower = prefab.Element as Tower;
            if (tower != null) { 
                Debug.Assert(type == tower.TowerType, "Tower prefab at wrong index!");
                return Get(prefab);
            }
            else
            {
                throw new System.Exception("Invalid Context Type");
            }
        }

        public GameTileContent Get(GameTileContentType type)
        {
            switch (type)
            {
                case GameTileContentType.Destination: return Get(destinationPrefab);
                case GameTileContentType.Empty: return Get(emptyPrefab);
                case GameTileContentType.Wall: return Get(wallPrefab);
                case GameTileContentType.SpawnPoint:return Get(spawnPoint);
            }
            Debug.Assert(false, "Unsupported non-tower type: " + type);
            return null;
        }
        
    }
}