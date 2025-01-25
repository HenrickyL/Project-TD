using Perikan.Infra.Gameplay;

namespace Perikan.Gameplay.Entity.Tower.Mortar.States
{
    public class AlertState : ATowerState
    {
        public delegate bool TargetHandler(ref TargetPoint target);
        TargetHandler _validateTrack;
        TargetPoint _target;

        float launchProgress = 5;

        public AlertState(
            TargetHandler validateTrack
        ) : base("Alert")
        {
            _validateTrack = validateTrack;
        }

        public override void Enter(GameElement context)
        {
            base.Enter(context);
            _target = null;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            //launchProgress += tower.ShotsPerSecond * Time.deltaTime;
            //while (launchProgress >= 1f)
            //{
                if (_validateTrack(ref _target))
                {
                    tower.ChangeToAttackState(ref _target);
                }
                //else
                //{
                //    launchProgress = 0.999f;
                //}
            //}
        }
    }
}