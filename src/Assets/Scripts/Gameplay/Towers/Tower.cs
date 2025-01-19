using Perikan.Gameplay.Map;
using UnityEngine;

namespace Perikan.Gameplay.Entity { 
    public abstract class Tower : GameTileContent
    {
        [SerializeField, Range(1.5f, 10.5f)]
        protected float targetingRange = 1.5f;
        
        const int _enemyLayerMask = 1 << (int)LayersEnum.Targets;
        static Collider[] _targetsBuffer = new Collider[10];
        /* --------------------------------------------- */
        void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize() { }

        /* --------------------------------------------- */

        protected abstract void Shoot();

        protected bool AcquireTarget(out TargetPoint target)
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
                target = _targetsBuffer[Random.Range(0, hits)].GetComponent<TargetPoint>();
                Debug.Assert(target != null, "Targeted non-enemy!", _targetsBuffer[0]);
                return true;
            }
            target = null;
            return false;
        }

        protected bool TrackTarget(ref TargetPoint target) {
            if (target == null || !target.Enemy.IsAlive) {
                return false;
            }
            Vector3 a = Position;
            Vector3 b = target.Position;
            float x = a.x - b.x;
            float z = a.z - b.z;
            float r = targetingRange + target.Radius;
            if (x * x + z * z > r * r)
            {
                target = null;
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
        }
                
    }
}