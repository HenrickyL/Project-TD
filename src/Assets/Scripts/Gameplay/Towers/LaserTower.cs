using UnityEngine;

namespace Perikan.Gameplay.Entity.Tower { 
    public class LaserTower : Tower
    {
        [SerializeField, Range(1f, 100f)]
        float damagePerSecond = 10f;

        [SerializeField]
        Transform turret, laserBeam = default;
        Vector3 _laserBeamScale;

        private TargetPoint _target;

        float rotationSpeed = 30.0f;
        float initialTurretY;
        float frequency = 1.3f;
        float amplitude = 0.05f;
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
            AnimateTurret();
        }

        /* --------------------PRIVATE--------------------------- */

        protected override void Shoot()
        {
            Vector3 point = _target.Position;
            Quaternion targetRotation = Quaternion.LookRotation(point - turret.position);

            turret.rotation = Quaternion.Lerp(turret.rotation, targetRotation, Time.deltaTime* lookAtSmoothFactor);
                laserBeam.localRotation = turret.localRotation;

                //turret.LookAt(point);
                //laserBeam.localRotation = turret.localRotation;

                float d = Vector3.Distance(turret.position, point);
            _laserBeamScale.z = d;
                laserBeam.localScale = _laserBeamScale;

                laserBeam.localPosition =
                    turret.localPosition + 0.5f * d* laserBeam.forward;

            _target.Enemy.HandleDamage(damagePerSecond* Time.deltaTime);
        }
        public override void Initialize()
        {
            base.Initialize();
            _laserBeamScale = laserBeam.localScale;
            initialTurretY = turret.localPosition.y;
        }
        private void ResetLaser()
        {
            laserBeam.localScale = Vector3.zero;
        }
        private void AnimateTurret()
        {
            if (_target != null) return;
            //vertical Moviment - sin wave
            float newY = initialTurretY + Mathf.Sin(Time.time * frequency) * amplitude; ;
            Vector3 newPosition = new Vector3(turret.localPosition.x, newY, turret.localPosition.z);
            turret.localPosition = newPosition;

            //rotation
            Vector3 targetEulerAngles = new Vector3(0f, Time.time * rotationSpeed, 0f);
            Quaternion newRotation = Quaternion.Euler(targetEulerAngles);
            turret.localRotation = newRotation;
        }
    }
}