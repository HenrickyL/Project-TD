using UnityEngine;

namespace Perikan.Gameplay.Entity.Tower { 
    public class LaserTower : Tower
    {
        [SerializeField, Range(1f, 100f)]
        float damagePerSecond = 10f;

        [SerializeField]
        Transform laserBeam = default;
        
        Vector3 _laserBeamScale;
        private TargetPoint _target;
        float lookAtSmoothFactor = 8.0f;

        public override TowerType TowerType => TowerType.Laser;

        /* --------------------PUBLIC--------------------------- */
        public override void GameUpdate()
        {
            base.GameUpdate();
            if (TrackTarget(ref _target) || AcquireTarget(out _target))
            {
                Shoot();
            }
            else
            {
                ResetLaser();
            }
        }

        /* --------------------PRIVATE--------------------------- */

        protected override bool AcquireTarget(out TargetPoint target)
        {
            ResetLaser();
            return base.AcquireTarget(out target);
        }

        protected void Shoot()
        {
            Vector3 point = _target.Position;
            Quaternion targetRotation = Quaternion.LookRotation(point - Turret.position);

            Turret.rotation = Quaternion.Lerp(Turret.rotation, targetRotation, Time.deltaTime* lookAtSmoothFactor);
            laserBeam.localRotation = Turret.localRotation;

            //turret.LookAt(point);
            //laserBeam.localRotation = turret.localRotation;

            float d = Vector3.Distance(Turret.position, point);
            _laserBeamScale.z = d;
                laserBeam.localScale = _laserBeamScale;

                laserBeam.localPosition =
                    Turret.localPosition + 0.5f * d* laserBeam.forward;

            _target.Enemy.HandleDamage(damagePerSecond* Time.deltaTime);
        }
        public override void Initialize()
        {
            base.Initialize();
            _laserBeamScale = laserBeam.localScale;
        }
        private void ResetLaser()
        {
            laserBeam.localScale = Vector3.Lerp(laserBeam.localScale, Vector3.zero,Time.deltaTime*2f);
        }
        
    }
}