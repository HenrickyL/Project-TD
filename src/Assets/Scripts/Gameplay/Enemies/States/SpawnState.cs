using UnityEngine;

public class SpawnState : AEnemyState
{
    private GameTile _tile;

    private Vector3 Position => Entity.LocalPosition;

    public SpawnState(GameTile tile) : base("Spawn")
    {
        _tile = tile;
    }

    public override void Enter(GameAsset entity)
    {
        base.Enter(entity);
        enemy.TileFrom = _tile;
        enemy.TileTo = _tile.NextTileOnPath;
        Entity.transform.position = _tile.transform.position;
        float angle = _tile.PathDirection.GetAngle();
        Entity.transform.localRotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 oldPos = Position;
        oldPos.y += 1;

        Entity.transform.localPosition = oldPos;
    }

    public override void UpdateState()
    {
        if (Position.y >= 0)
        {
            animationController.ChangeAnimator(AnimationStateEnum.Spawn, Position.y);
        }
        else
        {
            Vector3 old = Entity.transform.localPosition;
            old.y = 0.05f;
            Entity.transform.localPosition = old;
            animationController.ChangeAnimator(AnimationStateEnum.Spawn, Position.y);
            Entity.ChangeState(new MovimentState());
        }
    }
}
