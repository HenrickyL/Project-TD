using UnityEngine;
public class Enemy : GameEntity
{
    public GameTile TileTo { get; set; }
    public GameTile TileFrom { get; set; }


    //private void Awake()
    //{
    //    ChangeState(new SpawnState(null));
    //}

    //private void Update()
    //{
    //    GameUpdate();
    //}

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
        Scale = scale;
        PathOffset = pathOffset;
    }

    /* -------------------------------------------- */
    
}
