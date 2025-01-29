using Perikan.Gameplay.Controller;
using UnityEngine;

namespace Perikan.Gameplay.Entity.War
{
    public class Projectile : WarEntity
    {
        Vector3 launchPoint, targetPoint, launchVelocity;
        float _age, _blastRadius, _damage;

        public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity, float blastRadius, float damage) {
            this.launchPoint = launchPoint;
            this.targetPoint = targetPoint;
            this.launchVelocity = launchVelocity;
            this.transform.position = launchPoint;
            this._blastRadius = blastRadius;
            this._damage = damage;
        }

        public override void GameUpdate()
        {
            _age += Time.deltaTime;
            Vector3 p = launchPoint + launchVelocity * _age;
            p.y -= 0.5f * 9.81f * _age * _age;


            if (p.y <= 0f || IsInvalidPosition(p))
            {
                Explosion explosion =  GameController.SpawnExplosion();
                explosion.Initialize(targetPoint, _blastRadius, _damage);
                Recycle();
                return;
            }

            transform.position = p;
            Vector3 d = launchVelocity;
            d.y -= 9.81f * _age;
            transform.localRotation = Quaternion.LookRotation(d);
            //GameController.SpawnExplosion().Initialize(p, 0.1f);
        }

        private bool IsInvalidPosition(Vector3 pos) {
            return float.IsNaN(pos.x) || float.IsNaN(pos.y) || float.IsNaN(pos.z);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Vector3 position = transform.position;
            position.y += 0.01f;
            Gizmos.DrawWireSphere(position, _blastRadius);
        }
    }
}
