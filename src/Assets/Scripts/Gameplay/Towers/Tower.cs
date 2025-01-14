using UnityEngine;
public class Tower : GameTileContent
{
    [SerializeField, Range(1.5f, 10.5f)]
    float targetingRange = 1.5f;

    private TargetPoint _target;

    const int _enemyLayerMask = 1 << 9;

    static Collider[] _targetsBuffer = new Collider[1];


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
        Vector3 towerPos = Position;
        Vector3 aux = towerPos;
        aux.y += 2f;
        // OverlapSphere
        int hits = Physics.OverlapCapsuleNonAlloc(
            towerPos, aux, targetingRange, _targetsBuffer, _enemyLayerMask
        );
        if (hits > 0)
        {
            _target = _targetsBuffer[0].GetComponent<TargetPoint>();
            Debug.Assert(_target != null, "Targeted non-enemy!", _targetsBuffer[0]);
            return true;
        }
        _target = null;
        return false;
    }

    private bool TrackTarget() {
        if (_target == null || !_target.Enemy.IsAlive) {
            return false;
        }
        //Vector3 towerPos = Position;
        //Vector3 targetPos = _target.Position;
        Vector3 a = Position;
        Vector3 b = _target.Position;
        float x = a.x - b.x;
        float z = a.z - b.z;
        float r = targetingRange + _target.Radius;
        if (x * x + z * z > r * r)
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
