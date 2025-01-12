using System.Collections;
using UnityEngine;

public class SpawnState : AEnemyState
{
    private GameTile _tile;

    private Vector3 Position => enemy.Position;

    public SpawnState(GameTile tile)
    {
        _tile = tile;
    }

    public override void Enter(GameEntity entity)
    {
        base.Enter(entity);
        enemy.transform.position = _tile.transform.position;
        float angle = _tile.PathDirection.GetAngle();
        enemy.transform.localRotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 oldPos = Position;
        oldPos.y += 1;

        enemy.transform.localPosition = oldPos;
    }

    public override IEnumerator UpdateState()
    {
        if (Position.y <= 0.05)
        {
            Vector3 old = enemy.transform.localPosition;
            old.y = 0;
            enemy.transform.localPosition = old;


            enemy.ChangeState(new MovimentState());
        }
        yield break;
    }
}
