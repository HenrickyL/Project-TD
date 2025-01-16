using UnityEngine;

public class Enemy : GameEntity
{
    public GameTile TileTo { get; set; }
    public GameTile TileFrom { get; set; }



    public float Progress { get; set; } = 0f;

    public override void SpawnOn(GameTile tile)
    {
        Debug.Assert(tile.NextTileOnPath != null, "Nowhere to go!", this);
        TileFrom = tile;
        TileTo = tile.NextTileOnPath;
        ChangeState(new SpawnState(tile));
    }


    public void Initialize(float scale, float speed, float pathOffset)
    {
        ModelLocalScale = new Vector3(scale, scale, scale);
        Speed = speed;
        Scale = scale;
        PathOffset = pathOffset;
        Health = 100f * scale;
    }

    /* -------------------------------------------- */

    private void ApplyDamage(float damage) {
        Debug.Assert(damage >= 0f, "Negative damage applied.");
        Health -= damage;
    }
    public void HandleDamage(float damage)
    {
        ApplyDamage(damage);
        if (!this.IsAlive)
        {
            base.ChangeState(new DeathState(this));
        }
        //ChangeState(new HitState(this, damage, ApplyDamage));
    }
}
