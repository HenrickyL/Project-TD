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
                TargetPoint.FillBuffer(targetPoint, _blastRadius);
                for (int i = 0; i < TargetPoint.BufferedCount; i++)
                {
                    TargetPoint.GetBuffered(i).Enemy.ApplyDamage(_damage);
                }
                //Game.SpawnExplosion().Initialize(targetPoint, blastRadius, damage);
                Recycle();
                return;
            }

            transform.position = p;
            Vector3 d = launchVelocity;
            d.y -= 9.81f * _age;
            transform.localRotation = Quaternion.LookRotation(d);

            //Game.SpawnExplosion().Initialize(p, 0.1f, 0f);
        }

        private bool IsInvalidPosition(Vector3 pos) {
            return float.IsNaN(pos.x) || float.IsNaN(pos.y) || float.IsNaN(pos.z);
        }        
    }
}
