using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ///TODO: Use State machine use as in Menu
    EnemyFactory originFactory;

    GameTile _tileFrom, _tileTo;
    Vector3 _positionFrom, _positionTo;
    float _progress, _progressFactor;

    Direction _direction;
    DirectionChange _directionChange;
    float _directionAngleFrom, _directionAngleTo;

    [SerializeField]
    Transform _model = default;

    [SerializeField]
    AnimationStateController _animController;

    float _pathOffset;
    float _speed;

    public EnemyFactory OriginFactory
    {
        get => originFactory;
        set
        {
            Debug.Assert(originFactory == null, "Redefined origin factory!");
            originFactory = value;
        }
    }

    public void SpawnOn(GameTile tile)
    {
        Debug.Assert(tile.NextTileOnPath != null, "Nowhere to go!", this);
        _tileFrom = tile;
        _tileTo = tile.NextTileOnPath;
        _progress = 0f;
        PrepareIntro();
        _animController.ChangeAnimator(AnimationStateEnum.Walk);
    }

    public bool GameUpdate()
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
        _model.localScale = new Vector3(scale, scale, scale);
        this._speed = speed;
        this._pathOffset = pathOffset;
    }

    /* -------------------------------------------- */

    /// <summary>
    /// Configura o estado inicial (intro) do inimigo.
    /// No estado de introdu��o, o inimigo se move do centro do ladrilho inicial para sua borda.
    /// Como n�o h� mudan�a de dire��o neste estado, os �ngulos "From" e "To" s�o iguais.
    /// </summary>
    private void PrepareIntro() {
        _positionFrom = _tileFrom.transform.localPosition;
        _positionTo = _tileFrom.ExitPoint;
        _direction = _tileFrom.PathDirection;
        _directionChange = DirectionChange.None;
        _directionAngleFrom = _directionAngleTo = _direction.GetAngle();
        _model.localPosition = new Vector3(_pathOffset, 0f);
        transform.localRotation = _direction.GetRotation();
        _progressFactor = 2f * _speed;
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
        _model.localPosition = new Vector3(_pathOffset, 0f);
        transform.localRotation = _direction.GetRotation();
        _progressFactor = 2f * _speed;
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
        _model.localPosition = new Vector3(_pathOffset, 0f); ;
        _progressFactor = _speed;
    }

    private void PrepareTurnRight()
    {
        _directionAngleTo = _directionAngleFrom + 90f;
        _model.localPosition = new Vector3(_pathOffset - 0.5f, 0f);
        transform.localPosition = _positionFrom + _direction.GetHalfVector();
        _progressFactor = _speed / (Mathf.PI * 0.5f * (0.5f - _pathOffset));
    }

    void PrepareTurnLeft()
    {
        _directionAngleTo = _directionAngleFrom - 90f;
        _model.localPosition = new Vector3(_pathOffset+0.5f, 0f);
        transform.localPosition = _positionFrom + _direction.GetHalfVector();
        _progressFactor = _speed / (Mathf.PI * 0.5f * (0.5f + _pathOffset));
    }

    private void PrepareTurnAround()
    {
        _directionAngleTo = _directionAngleFrom + (_pathOffset < 0f ? 180f : -180f);
        _model.localPosition = new Vector3(_pathOffset, 0f);
        transform.localPosition = _positionFrom;
        _progressFactor = _speed / (Mathf.PI * Mathf.Max(Mathf.Abs(_pathOffset), 0.2f));
    }

    private IEnumerator HandleDeath()
    {
        _animController.ChangeAnimator(AnimationStateEnum.Death);

        yield return new WaitForSeconds(_animController.GetAnimationLength(AnimationStateEnum.Death)+0.15f);

        OriginFactory.Reclaim(this);
    }
}
