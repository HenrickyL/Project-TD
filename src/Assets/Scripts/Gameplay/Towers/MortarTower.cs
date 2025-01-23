using Perikan.Gameplay.Factory;
using UnityEngine;

namespace Perikan.Gameplay.Entity.Tower
{
    public class MortarTower : Tower
    {
        [SerializeField, Range(0.5f, 2f)]
        float shotsPerSecond = 1f;

        [SerializeField]
        WarFactory projectileFactory = default;
        [SerializeField]
        Transform mortar = default;
        [SerializeField]
        Transform _laucherPoint = default;

        float lookAtSmoothFactor = 8.0f;
        float progress = 5;

        public override TowerType TowerType => TowerType.Mortar;


        private TargetPoint _target;

        protected override void Shoot()
        {
        }
             
        public override void GameUpdate()
        {
            base.GameUpdate();
            if (TrackTarget(ref _target) || AcquireTarget(out _target))
            {
                Launch(_target);
            }
            if (_target != null) progress += Time.deltaTime;
        }

        private void Launch(TargetPoint target)
        {
            Vector3 point = target.Position;
            Quaternion targetRotation = Quaternion.LookRotation(point - mortar.position);
            targetRotation.x = 0;
            targetRotation.z = 0;

            mortar.rotation = Quaternion.Lerp(mortar.rotation, targetRotation, Time.deltaTime * lookAtSmoothFactor);
            if (progress < shotsPerSecond)
            {
                return;
            }
            

            Projectile projectile = projectileFactory.Get();
            //projectile.SpawnOn(_laucherPoint.position, initialSpeed);
            _target = null;
            progress = 0;
        }

        

    }

}