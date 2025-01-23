using Perikan.Gameplay.Controller;
using Perikan.Infra.Gameplay;
using UnityEngine;

namespace Perikan.Gameplay.Entity
{
    public class Projectile : GameAsset
    {
        private bool _isLive = true;
        public override bool IsAlive => _isLive;
        Vector3 launchPoint, targetPoint, launchVelocity;
        float age, blastRadius, damage;

        public void SpawnOn(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity, float blastRadius, float damage) {
            
            GameController.AddProjectile(this);
            this.launchPoint = launchPoint;
            this.targetPoint = targetPoint;
            this.launchVelocity = launchVelocity;
        }

        public override void GameUpdate()
        {
            age += Time.deltaTime;
            Vector3 p = launchPoint + launchVelocity * age;
            p.y -= 0.5f * 9.81f * age * age;

            if (p.y <= 0f)
            {
                //Game.SpawnExplosion().Initialize(targetPoint, blastRadius, damage);
                Recycle();
            }

            transform.localPosition = p;
            Vector3 d = launchVelocity;
            d.y -= 9.81f * age;
            transform.localRotation = Quaternion.LookRotation(d);

            //Game.SpawnExplosion().Initialize(p, 0.1f, 0f);
        }

        public override void Recycle()
        {
            base.Recycle();
            _isLive = false;
        }
    }
}
