using System.Collections;
using UnityEngine;

public class Enemy : GameEntity
{
    public GameTile TileTo { get; set; }
    public GameTile TileFrom { get; set; }

    

    public override void SpawnOn(GameTile tile)
    {
        Debug.Assert(tile.NextTileOnPath != null, "Nowhere to go!", this);
        TileFrom = tile;
        TileTo = tile.NextTileOnPath;
        AnimationController.ChangeAnimator(AnimationStateEnum.Walk);
        ChangeState(new SpawnState(tile));
    }

    
    public void Initialize(float scale, float speed, float pathOffset)
    {
        ModelLocalScale = new Vector3(scale, scale, scale);
        Speed = speed;
        PathOffset = pathOffset;
    }

    /* -------------------------------------------- */

    
    public IEnumerator HandleDeath()
    {
        IsAlive = false;
        AnimationController.ChangeAnimator(AnimationStateEnum.Death);
        yield return new WaitForSeconds(AnimationController.GetAnimationLength(AnimationTypeEnum.Death)+0.15f);
        OriginFactory.Reclaim(this);
    }
}
