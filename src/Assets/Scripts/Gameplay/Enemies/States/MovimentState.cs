using Perikan.Gameplay.Map;
using Perikan.Infra.Animation;
using Perikan.Infra.Gameplay;
using UnityEngine;

namespace Perikan.Gameplay.EnemyState { 
    public class MovimentState : AEnemyState
    {
        public MovimentState() : base("Moviment") { 
        
        }
        GameTile TileTo => enemy.TileTo;
        GameTile TileFrom => enemy.TileFrom;


        Vector3 _positionFrom, _positionTo;
        float _progress, _progressFactor;

        Direction _direction;
        DirectionChange _directionChange;
        float _directionAngleFrom, _directionAngleTo;

        public override void Enter(GameAsset entity)
        {
            base.Enter(entity);
            enemy.TileTo = enemy.TileFrom.NextTileOnPath;
            _progress =  enemy.Progress > 0 ? enemy.Progress :  0f;
            PrepareIntro();
            animationController.ChangeAnimator(AnimationStateEnum.Walk);
        }
        public override void UpdateState()
        {
            _progress += Time.deltaTime * _progressFactor;
            while (_progress > 1f)
            {
                if (TileTo == null)
                {
                    Entity.ChangeState(new DeathState(Entity));
                    return;
                }
                _progress = (_progress - 1f) / _progressFactor;
                PrepareNextState();
                _progress *= _progressFactor;

            }
            if (_directionChange == DirectionChange.None)
            {
                Entity.transform.localPosition = Vector3.LerpUnclamped(_positionFrom, _positionTo, _progress);
            }
            else
            {
                float angle = Mathf.LerpUnclamped(
                    _directionAngleFrom, _directionAngleTo, _progress
                );
                Entity.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
            }
        }


        public override void Exit()
        {
            base.Exit();
            enemy.Progress = _progress;
        }

        /* ------------------------------------------------------------------- */

        /// <summary>
        /// Configura o estado inicial (intro) do inimigo.
        /// No estado de introdução, o inimigo se move do centro do ladrilho inicial para sua borda.
        /// Como não há mudança de direção neste estado, os ângulos "From" e "To" são iguais.
        /// </summary>
        private void PrepareIntro()
        {
            _positionFrom = TileFrom.transform.localPosition;
            _positionTo = TileFrom.ExitPoint;
            _direction = TileFrom.PathDirection;
            _directionChange = DirectionChange.None;
            _directionAngleFrom = _directionAngleTo = _direction.GetAngle();
            enemy.ModelLocalPosition = new Vector3(enemy.PathOffset, 0f);
            Entity.transform.localRotation = _direction.GetRotation();
            _progressFactor = 2f * enemy.Speed;
        }
        /// <summary>
        /// Configura o estado final (outro) do inimigo.
        /// Neste estado, o inimigo retorna para o centro do ladrilho atual antes de ser removido do jogo.
        /// </summary>
        void PrepareOutro()
        {
            _positionTo = TileFrom.transform.localPosition;
            _directionChange = DirectionChange.None;
            _directionAngleTo = _direction.GetAngle();
            enemy.ModelLocalPosition = new Vector3(enemy.PathOffset, 0f);
            Entity.transform.localRotation = _direction.GetRotation();
            _progressFactor = 2f * enemy.Speed;
        }

        private void PrepareNextState()
        {
            enemy.TileFrom = TileTo;
            enemy.TileTo = TileTo.NextTileOnPath;
            _positionFrom = _positionTo;
            if (TileTo == null)
            {
                PrepareOutro();
                return;
            }
            _positionTo = TileFrom.ExitPoint;
            _directionChange = _direction.GetDirectionChangeTo(TileFrom.PathDirection);
            _direction = TileFrom.PathDirection;
            _directionAngleFrom = _directionAngleTo;

            switch (_directionChange)
            {
                case DirectionChange.None: PrepareForward(); break;
                case DirectionChange.TurnRight: PrepareTurnRight(); break;
                case DirectionChange.TurnLeft: PrepareTurnLeft(); break;
                default: PrepareTurnAround(); break;
            }
        }


        private void PrepareForward()
        {
            Entity.transform.localRotation = _direction.GetRotation();
            _directionAngleTo = _direction.GetAngle();
            enemy.ModelLocalPosition = new Vector3(enemy.PathOffset, 0f); ;
            _progressFactor = enemy.Speed;
        }

        private void PrepareTurnRight()
        {
            _directionAngleTo = _directionAngleFrom + 90f;
            enemy.ModelLocalPosition = new Vector3(enemy.PathOffset - 0.5f, 0f);
            Entity.transform.localPosition = _positionFrom + _direction.GetHalfVector();
            _progressFactor = enemy.Speed / (Mathf.PI * 0.5f * (0.5f - enemy.PathOffset));
        }

        void PrepareTurnLeft()
        {
            _directionAngleTo = _directionAngleFrom - 90f;
            enemy.ModelLocalPosition = new Vector3(enemy.PathOffset + 0.5f, 0f);
            Entity.transform.localPosition = _positionFrom + _direction.GetHalfVector();
            _progressFactor = enemy.Speed / (Mathf.PI * 0.5f * (0.5f + enemy.PathOffset));
        }

        private void PrepareTurnAround()
        {
            _directionAngleTo = _directionAngleFrom + (enemy.PathOffset < 0f ? 180f : -180f);
            enemy.ModelLocalPosition = new Vector3(enemy.PathOffset, 0f);
            Entity.transform.localPosition = _positionFrom;
            _progressFactor = enemy.Speed / (Mathf.PI * Mathf.Max(Mathf.Abs(enemy.PathOffset), 0.2f));
        }
    }
}