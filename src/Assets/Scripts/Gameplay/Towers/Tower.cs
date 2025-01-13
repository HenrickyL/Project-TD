using UnityEngine;
public class Tower : GameTileContent
{
    [SerializeField, Range(1.5f, 10.5f)]
    float targetingRange = 1.5f;

    private TargetPoint _target;

    const int _enemyLayerMask = 1 << 9;


    public override void GameUpdate()
    {
        base.GameUpdate();
        if (TrackTarget() || AcquireTarget())
        {
            Debug.Log("Locked on target!");
        }
    }

    /* --------------------------------------------- */

    private bool AcquireTarget()
    {
        Collider[] targets = Physics.OverlapSphere(
            Position, targetingRange, _enemyLayerMask
        );
        if (targets.Length > 0)
        {
            _target = targets[0].GetComponent<TargetPoint>();
            Debug.Assert(_target != null, "Targeted non-enemy!", targets[0]);
            return true;
        }
        _target = null;
        return false;
    }

    private bool TrackTarget() {
        if (_target == null || !_target.Enemy.IsAlive) {
            return false;
        }
        Vector3 towerPos = Position;
        Vector3 targetPos = _target.Position;
        if (Vector3.Distance(towerPos, targetPos) > targetingRange + _target.Radius)
        {
            _target = null;
            return false;
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 position = transform.position;
        position.y += 0.01f;
        Gizmos.DrawWireSphere(position, targetingRange);

        if (_target != null)
        {
            Gizmos.DrawLine(position, _target.Position);
        }
    }
}
