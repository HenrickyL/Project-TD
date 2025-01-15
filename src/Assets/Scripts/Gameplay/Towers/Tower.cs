using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class Tower : GameTileContent
{
    [SerializeField, Range(1.5f, 10.5f)]
    float targetingRange = 1.5f;

    [SerializeField]
    Transform turret, laserBeam = default;

    [SerializeField, Range(1f, 100f)]
    float damagePerSecond = 10f;


    private TargetPoint _target;

    const int _enemyLayerMask = 1 << (int)LayersEnum.Targets;

    static Collider[] _targetsBuffer = new Collider[1];

    Vector3 _laserBeamScale;

    bool onHit = false;

    void Awake()
    {
        _laserBeamScale = laserBeam.localScale;
    }

    public override void GameUpdate()
    {
        base.GameUpdate();
        if (TrackTarget() || AcquireTarget())
        {
            Shoot();
        }
        else {
            laserBeam.localScale = Vector3.zero;
        }
    }

    /* --------------------------------------------- */

    private void Shoot() {
        Vector3 point = _target.Position;
        turret.LookAt(point);
        laserBeam.localRotation = turret.localRotation;

        float d = Vector3.Distance(turret.position, point);
        _laserBeamScale.z = d;
        laserBeam.localScale = _laserBeamScale;

        laserBeam.localPosition =
            turret.localPosition + 0.5f * d * laserBeam.forward;

        if (!onHit) { 
            onHit = true;
        }
        _target.Enemy.HandleDamage(damagePerSecond * Time.deltaTime);

    }

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
