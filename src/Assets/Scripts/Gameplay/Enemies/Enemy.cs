using Perikan.Gameplay.Map;
using Perikan.Infra.Gameplay;
using UnityEngine;
using Perikan.Gameplay.EnemyState;
namespace Perikan.Gameplay.Entity { 
    public class Enemy : GameEntity
    {
        public GameTile TileTo { get; set; }
        public GameTile TileFrom { get; set; }

        public float Progress { get; set; } = 0f;

        public void SpawnOn(GameTile tile)
        {
            Debug.Assert(tile.NextTileOnPath != null, "Nowhere to go!", this);
            TileFrom = tile;
            TileTo = tile.NextTileOnPath;
            ChangeState(new SpawnState(tile));
        }


        public void Initialize(float scale, float speedMagnitude, float pathOffset)
        {
            ModelLocalScale = new Vector3(scale, scale, scale);
            Speed = Vector3.zero;
            Scale = scale;
            SpeedMagnitude = speedMagnitude;
            PathOffset = pathOffset;
            Health = 100f * scale;
        }

        /* -------------------------------------------- */

        private void HandleApplyDamage(float damage) {
            Debug.Assert(damage >= 0f, "Negative damage applied.");
            Health -= damage;
        }
        public void ApplyDamage(float damage)
        {
            HandleApplyDamage(damage);
            if (!this.IsAlive)
            {
                base.ChangeState(new DeathState(this));
            }
            //ChangeState(new HitState(this, damage, ApplyDamage));
        }
    }

}