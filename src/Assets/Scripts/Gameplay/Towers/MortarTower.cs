using Perikan.Gameplay.Controller;
using UnityEngine;

namespace Perikan.Gameplay.Entity.Tower
{
    public class MortarTower : Tower
    {
        [SerializeField, Range(0.5f, 2f)]
        float shotsPerSecond = 1f;

        [SerializeField]
        Transform mortar = default;
        [SerializeField]
        Transform _laucherPoint = default;
        [SerializeField, Range(0.5f, 3f)]
        float shellBlastRadius = 1f;
        [SerializeField, Range(1f, 100f)]
        float shellDamage = 10f;

        float lookAtSmoothFactor = 8.0f;
        float launchProgress = 5;
        float launchSpeed;

        public override TowerType TowerType => TowerType.Mortar;
        private TargetPoint _target;

        void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            float x = targetingRange + 0.25001f;
            float y = -mortar.position.y;
            launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
        }


        protected override void Shoot()
        {
            LookTarget();
            if (launchProgress < shotsPerSecond)
            {
                return;
            }
            ShootTarget();

            _target = null;
            launchProgress = 0;
        }
             
        public override void GameUpdate()
        {
            base.GameUpdate();
            if (_target != null) launchProgress += Time.deltaTime;
            if (TrackTarget(ref _target) || AcquireTarget(out _target))
            {
                Shoot();
            }
        }


        private void LookTarget() {
            Vector3 point = _target.Position;
            Quaternion targetRotation = Quaternion.LookRotation(point - mortar.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            mortar.rotation = Quaternion.Lerp(mortar.rotation, targetRotation, Time.deltaTime * lookAtSmoothFactor);
        }

        private void ShootTarget() {
            Vector3 launchPoint = mortar.position;
            Vector3 targetPoint = _target.Position;

            Vector3 targetDirection = (targetPoint - launchPoint).normalized;
            float travelTime = Vector3.Distance(launchPoint, targetPoint) / launchSpeed;
            targetPoint += targetDirection * _target.Enemy.Speed.magnitude * travelTime;

            targetPoint.y = 0f;

            Vector2 dir;
            dir.x = targetPoint.x - launchPoint.x;
            dir.y = targetPoint.z - launchPoint.z;
            float x = dir.magnitude;
            float y = -launchPoint.y;
            dir /= x;

            float g = 9.81f;
            float s = launchSpeed;
            float s2 = s * s;

            float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
            Debug.Assert(r >= 0f, "Launch velocity insufficient for range!");
            float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
            float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
            float sinTheta = cosTheta * tanTheta;

            Projectile projectile = GameController.SpawnProjectile();
            projectile.Initialize(
                launchPoint, targetPoint,
                new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y),
                shellBlastRadius, shellDamage);
        }

    }

}