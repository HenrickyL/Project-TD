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
    public void Initialize(float scale)
    {
        _model.localScale = new Vector3(scale, scale, scale);
    }

    /* -------------------------------------------- */

    private void PrepareIntro() {
        _positionFrom = _tileFrom.transform.localPosition;
        _positionTo = _tileFrom.ExitPoint;
        _direction = _tileFrom.PathDirection;
        _directionChange = DirectionChange.None;
        _directionAngleFrom = _directionAngleTo = _direction.GetAngle();
        transform.localRotation = _direction.GetRotation();
        _progressFactor = 2f;
    }

    void PrepareOutro()
    {
        _positionTo = _tileFrom.transform.localPosition;
        _directionChange = DirectionChange.None;
        _directionAngleTo = _direction.GetAngle();
        _model.localPosition = Vector3.zero;
        transform.localRotation = _direction.GetRotation();
        _progressFactor = 2f;
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
        _model.localPosition = Vector3.zero;
        _progressFactor = 1f;
    }

    private void PrepareTurnRight()
    {
        _directionAngleTo = _directionAngleFrom + 90f;
        _model.localPosition = new Vector3(-0.5f, 0f);
        transform.localPosition = _positionFrom + _direction.GetHalfVector();
        _progressFactor = 1f / (Mathf.PI * 0.25f);
    }

    void PrepareTurnLeft()
    {
        _directionAngleTo = _directionAngleFrom - 90f;
        _model.localPosition = new Vector3(0.5f, 0f);
        transform.localPosition = _positionFrom + _direction.GetHalfVector();
        _progressFactor = 1f / (Mathf.PI * 0.25f);
    }

    private void PrepareTurnAround()
    {
        _directionAngleTo = _directionAngleFrom + 180f;
        _model.localPosition = Vector3.zero;
        transform.localPosition = _positionFrom;
        _progressFactor = 2f;
    }

    private IEnumerator HandleDeath()
    {
        _animController.ChangeAnimator(AnimationStateEnum.Death);

        yield return new WaitForSeconds(_animController.GetAnimationLength(AnimationStateEnum.Death)+0.1f);

        OriginFactory.Reclaim(this);
    }
}
