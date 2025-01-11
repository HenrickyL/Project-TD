using System.Collections;
using UnityEngine;

public class Enemy : GameEntity
{
    GameTile _tileFrom, _tileTo;
    Vector3 _positionFrom, _positionTo;
    float _progress, _progressFactor;

    Direction _direction;
    DirectionChange _directionChange;
    float _directionAngleFrom, _directionAngleTo;

    public override void SpawnOn(GameTile tile)
    {
        Debug.Assert(tile.NextTileOnPath != null, "Nowhere to go!", this);
        _tileFrom = tile;
        _tileTo = tile.NextTileOnPath;
        _progress = 0f;
        PrepareIntro();
        AnimationController.ChangeAnimator(AnimationStateEnum.Walk);
    }

    public override bool GameUpdate()
    {
        _progress += Time.deltaTime * _progressFactor;
        while (_progress > 1f) {        
            if (_tileTo == null)
            {
                //OriginFactory.Reclaim(this);
                StartCoroutine(HandleDeath());
                return false;
            }
            _progress = (_progress - 1f) / _progressFactor;
            PrepareNextState();
            _progress *= _progressFactor;

        }
        if (_directionChange == DirectionChange.None) { 
            transform.localPosition = Vector3.LerpUnclamped(_positionFrom, _positionTo, _progress);
        }
        else
        {
            float angle = Mathf.LerpUnclamped(
                _directionAngleFrom, _directionAngleTo, _progress
            );
            transform.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
        return true;
    }
    public void Initialize(float scale, float speed, float pathOffset)
    {
        ModelLocalScale = new Vector3(scale, scale, scale);
        Speed = speed;
        PathOffset = pathOffset;
    }

    /* -------------------------------------------- */

    /// <summary>
    /// Configura o estado inicial (intro) do inimigo.
    /// No estado de introdução, o inimigo se move do centro do ladrilho inicial para sua borda.
    /// Como não há mudança de direção neste estado, os ângulos "From" e "To" são iguais.
    /// </summary>
    private void PrepareIntro() {
        _positionFrom = _tileFrom.transform.localPosition;
        _positionTo = _tileFrom.ExitPoint;
        _direction = _tileFrom.PathDirection;
        _directionChange = DirectionChange.None;
        _directionAngleFrom = _directionAngleTo = _direction.GetAngle();
        ModelLocalPosition = new Vector3(PathOffset, 0f);
        transform.localRotation = _direction.GetRotation();
        _progressFactor = 2f * Speed;
    }

    /// <summary>
    /// Configura o estado final (outro) do inimigo.
    /// Neste estado, o inimigo retorna para o centro do ladrilho atual antes de ser removido do jogo.
    /// </summary>
    void PrepareOutro()
    {
        _positionTo = _tileFrom.transform.localPosition;
        _directionChange = DirectionChange.None;
        _directionAngleTo = _direction.GetAngle();
        ModelLocalPosition = new Vector3(PathOffset, 0f);
        transform.localRotation = _direction.GetRotation();
        _progressFactor = 2f * Speed;
    }

    private void PrepareNextState() {
        _tileFrom = _tileTo;
        _tileTo = _tileTo.NextTileOnPath;
        _positionFrom = _positionTo;
        if (_tileTo == null)
        {
            PrepareOutro();
            return;
        }
        _positionTo = _tileFrom.ExitPoint;
        _directionChange = _direction.GetDirectionChangeTo(_tileFrom.PathDirection);
        _direction = _tileFrom.PathDirection;
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
        transform.localRotation = _direction.GetRotation();
        _directionAngleTo = _direction.GetAngle();
        ModelLocalPosition = new Vector3(PathOffset, 0f); ;
        _progressFactor = Speed;
    }

    private void PrepareTurnRight()
    {
        _directionAngleTo = _directionAngleFrom + 90f;
        ModelLocalPosition = new Vector3(PathOffset - 0.5f, 0f);
        transform.localPosition = _positionFrom + _direction.GetHalfVector();
        _progressFactor = Speed / (Mathf.PI * 0.5f * (0.5f - PathOffset));
    }

    void PrepareTurnLeft()
    {
        _directionAngleTo = _directionAngleFrom - 90f;
        ModelLocalPosition = new Vector3(PathOffset+0.5f, 0f);
        transform.localPosition = _positionFrom + _direction.GetHalfVector();
        _progressFactor = Speed / (Mathf.PI * 0.5f * (0.5f + PathOffset));
    }

    private void PrepareTurnAround()
    {
        _directionAngleTo = _directionAngleFrom + (PathOffset < 0f ? 180f : -180f);
        ModelLocalPosition = new Vector3(PathOffset, 0f);
        transform.localPosition = _positionFrom;
        _progressFactor = Speed / (Mathf.PI * Mathf.Max(Mathf.Abs(PathOffset), 0.2f));
    }

    private IEnumerator HandleDeath()
    {
        AnimationController.ChangeAnimator(AnimationStateEnum.Death);

        yield return new WaitForSeconds(AnimationController.GetAnimationLength(AnimationStateEnum.Death)+0.15f);

        OriginFactory.Reclaim(this);
    }
}
