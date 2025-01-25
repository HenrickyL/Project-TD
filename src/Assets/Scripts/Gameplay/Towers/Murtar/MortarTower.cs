using Perikan.Gameplay.Entity.Tower.Mortar.States;
using UnityEngine;

namespace Perikan.Gameplay.Entity.Tower
{
    public class MortarTower : Tower
    {
        [SerializeField, Range(0.5f, 2f)]
        private float _shotsPerSecond = 1f;
        public float ShotsPerSecond => _shotsPerSecond;

        [SerializeField]
        Transform _laucherPoint = default;

        [SerializeField, Range(0.5f, 3f)]
        float shellBlastRadius = 1f;
        [SerializeField, Range(1f, 100f)]
        float shellDamage = 10f;

        private AlertState _alertState;
        private AttackState _AttackState;


        public override TowerType TowerType => TowerType.Mortar;


        private bool HandleAcquireTarget(ref TargetPoint target)
        {
            return TrackTarget(ref target) || AcquireTarget(out target);
        }

        private bool HandleTrackTarget(ref TargetPoint target)
        {
            return TrackTarget(ref target);
        }

        public override void Initialize() { 
            base.Initialize();
            ChangeToAlertState();
        }

        public void ChangeToAlertState()
        {
            if (_alertState == null) { 
                _alertState = new AlertState(
                    HandleAcquireTarget
                );
            }
            ChangeState(_alertState);
        }

        public void ChangeToAttackState(ref TargetPoint _target)
        {
            if (_AttackState == null)
            {
                _AttackState = new AttackState(
                    new MortarAttributes
                    {
                        LaucherPoint = _laucherPoint,
                        ShellBlastRadius = shellBlastRadius,
                        ShellDamage = shellDamage,
                    },
                    HandleAcquireTarget
                );
            }
            _AttackState.SetTarget(ref _target);
            ChangeState(_AttackState);
        }

    }

}