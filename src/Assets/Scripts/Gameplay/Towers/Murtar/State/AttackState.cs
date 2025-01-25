using Perikan.Gameplay.Controller;
using Perikan.Infra.Animation;
using Perikan.Infra.Gameplay;
using System.Collections;
using UnityEngine;

namespace Perikan.Gameplay.Entity.Tower.Mortar.States
{
    public record MortarAttributes {
        public Transform LaucherPoint {get; set;}
        public float ShellBlastRadius {get; set;}= 1f;
        public float ShellDamage {get; set;}= 10f;
    }

    public class AttackState : ATowerState
    {
        public delegate bool TargetHandler(ref TargetPoint target);

        TargetHandler _trackTarget;

        MortarAttributes _attributes;
        Vector3 _targetPos;
        Vector3 _targetSpeed;
        TargetPoint _target;

        float lookAtSmoothFactor = 8.0f;
        float launchSpeed;

        public AttackState(MortarAttributes attributes, TargetHandler trackTarget) : base("Attack")
        { 
            this._attributes = attributes;
            _trackTarget = trackTarget;
        }

        public override void Enter(GameElement context)
        {
            base.Enter(context);
            float x = tower.TargetingRange + 0.25001f;
            float y = -1 * tower.Turret.position.y;
            launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
            tower.StartCoroutine(HandleAttack());
        }

        public override void Exit()
        {
            base.Exit();
            tower.AnimationController.ChangeAnimator(AnimationStateEnum.Attack, 0);
        }

        private IEnumerator HandleAttack()
        {
            AnimationStateController animationController = tower.AnimationController;
            animationController.ChangeAnimator(AnimationStateEnum.Attack);
            yield return new WaitForSeconds(animationController.GetAnimationLength(AnimationTypeEnum.Attack)-1f);
            ShootTarget();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            UpdateTarget();
            if (_trackTarget(ref _target)) { 
                LookTarget();
            }
        }

        public void SetTarget(ref TargetPoint target) { 
            _target = target;
        }

        /* -------------------------------------------------- */


        private void LookTarget()
        {
            Vector3 point = _targetPos;
            Quaternion targetRotation = Quaternion.LookRotation(point - tower.Turret.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            tower.Turret.rotation = Quaternion.Lerp(tower.Turret.rotation, targetRotation, Time.deltaTime * lookAtSmoothFactor);
        }

        private void ShootTarget()
        {
            Vector3 launchPoint = tower.Turret.position;
            Vector3 targetPoint = _targetPos;

            //adjust to speed to enemy
            Vector3 targetVelocity = _targetSpeed * 2f;
            float travelTime = Vector3.Distance(launchPoint, targetPoint) / launchSpeed;
            targetPoint += targetVelocity * travelTime;

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
                _attributes.ShellBlastRadius, _attributes.ShellDamage);
            tower.ChangeToAlertState();
        }
        //private Vector3 GetAdjustedLaunchDirection(Vector3 launchPoint, Vector3 targetPoint, Vector3 targetVelocity, float launchSpeed)
        //{
        //    Vector3 launchDirection = targetPoint - launchPoint;
        //    float distance = launchDirection.magnitude;

        //    if (distance < 0.1f)
        //    {
        //        return (targetPoint - launchPoint).normalized;
        //    }

        //    float travelTime = distance / launchSpeed;

        //    Vector3 futureTargetPosition = targetPoint + targetVelocity * travelTime;
        //    Vector3 adjustedLaunchDirection = futureTargetPosition - launchPoint;
        //    return adjustedLaunchDirection.normalized;
        //}

        private void UpdateTarget() {
            if (_target == null) return;
            _targetPos = _target.Position;
            _targetSpeed = _target.Enemy.Speed;
        }
    }
}