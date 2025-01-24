using Perikan.Gameplay.Controller;
using Perikan.Infra.Gameplay;
using TMPro;
using UnityEngine;

namespace Perikan.Gameplay.Entity
{
    public class Projectile : GameAsset
    {
        private bool _isLive = true;
        public override bool IsAlive => _isLive;
        Vector3 launchPoint, targetPoint, launchVelocity;
        float age, blastRadius, damage;

        public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity, float blastRadius, float damage) {
            this.launchPoint = launchPoint;
            this.targetPoint = targetPoint;
            this.launchVelocity = launchVelocity;
        }

        public override void GameUpdate()
        {
            age += Time.deltaTime;
            Vector3 p = launchPoint + launchVelocity * age;
            p.y -= 0.5f * 9.81f * age * age;


            if (p.y <= 0f || IsInvalidPosition(p))
            {
                //Game.SpawnExplosion().Initialize(targetPoint, blastRadius, damage);
                Recycle();
                return;
            }

            transform.position = p;
            Vector3 d = launchVelocity;
            d.y -= 9.81f * age;
            transform.localRotation = Quaternion.LookRotation(d);

            //Game.SpawnExplosion().Initialize(p, 0.1f, 0f);
        }

        private bool IsInvalidPosition(Vector3 pos) {
            return float.IsNaN(pos.x) || float.IsNaN(pos.y) || float.IsNaN(pos.z);
        }

        public override void Recycle()
        {
            base.Recycle();
            _isLive = false;
        }
    }
}
