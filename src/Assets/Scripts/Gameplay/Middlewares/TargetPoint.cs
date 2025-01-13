using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TargetPoint : MonoBehaviour
{
    public Enemy Enemy { get; private set; }
    public Vector3 Position => transform.position;

    private SphereCollider _collider = default;

    public float Radius => _collider.radius * Enemy.Scale;

    void Awake()
    {
        Enemy = transform.root.GetComponent<Enemy>();
        _collider = gameObject.GetComponent<SphereCollider>();
        CheckAssertation();
    }

    private void CheckAssertation() {
        Debug.Assert(Enemy != null, "Target point without Enemy root!", this);
        //Debug.Assert(
        //    GetComponent<SphereCollider>() != null,
        //    "Target point without sphere collider!", this
        //);
        Debug.Assert(gameObject.layer == 9, "Target point on wrong layer!", this);
    }
}
