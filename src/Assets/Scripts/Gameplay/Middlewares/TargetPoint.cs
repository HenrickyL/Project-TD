using Perikan.Gameplay.Entity;
using Perikan.Gameplay.Enums;
using UnityEngine;

namespace Perikan.Gameplay { 
    [RequireComponent(typeof(SphereCollider))]
    public class TargetPoint : MonoBehaviour
    {
        public Enemy Enemy { get; private set; }
        public Vector3 Position => transform.position;

        private SphereCollider _collider = default;

        public float Radius => _collider.radius * Enemy.Scale;
        /* ---------------------------------------------------------------- */
        const int enemyLayerMask = 1 << (int)LayersEnum.Targets;
        static Collider[] buffer = new Collider[100];
        public static int BufferedCount { get; private set; }

        /* ----------------------------PUBLIC------------------------------------ */
        public static TargetPoint RandomBuffered => GetBuffered(Random.Range(0, BufferedCount));
        public static bool FillBuffer(Vector3 position, float range) {
            Vector3 top = position;
            top.y += 3f;
            BufferedCount = Physics.OverlapCapsuleNonAlloc(
                position, top, range, buffer, enemyLayerMask
            );
            return BufferedCount > 0;
        }
        public static TargetPoint GetBuffered(int index) {
            TargetPoint target = buffer[index].GetComponent<TargetPoint>();
            Debug.Assert(target != null, "Targeted non-enemy!", buffer[0]);
            return target;
        }
        /* ----------------------------PRIVATE------------------------------------ */
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
            Debug.Assert(gameObject.layer == (int)LayersEnum.Targets, "Target point on wrong layer!", this);
        }
    }
}