using UnityEngine;

public class Enemy : MonoBehaviour
{
    ///TODO: Use State machine use as in Menu
    EnemyFactory originFactory;

    GameTile _tileFrom, _tileTo;
    Vector3 _positionFrom, _positionTo;
    float _progress;

    Direction _direction;
    DirectionChange _directionChange;
    float _directionAngleFrom, _directionAngleTo;

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
    }

    public bool GameUpdate()
    {
        _progress += Time.deltaTime;
        while (_progress > 1f) {
            _tileFrom = _tileTo;
            if (_tileTo == null)
            {
                OriginFactory.Reclaim(this);
                return false;
            }
            _tileTo = _tileTo.NextTileOnPath;
            if (_tileTo == null)
            {
                OriginFactory.Reclaim(this);
                return false;
            }
            PrepareNextState();
            _progress -= 1f;
        }
        transform.localPosition = Vector3.LerpUnclamped(_positionFrom, _positionTo, _progress);
        if (_directionChange != DirectionChange.None)
        {
            float angle = Mathf.LerpUnclamped(
                _directionAngleFrom, _directionAngleTo, _progress
            );
            transform.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
        return true;
    }

    /* -------------------------------------------- */

    private void PrepareIntro() {
        _positionFrom = _tileFrom.transform.localPosition;
        _positionTo = _tileFrom.ExitPoint;
        _direction = _tileFrom.PathDirection;
        _directionChange = DirectionChange.None;
        _directionAngleFrom = _directionAngleTo = _direction.GetAngle();
        transform.localRotation = _direction.GetRotation();
    }

    private void PrepareNextState() {
        _positionFrom = _positionTo;
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
    }

    private void PrepareTurnRight()
    {
        _directionAngleTo = _directionAngleFrom + 90f;
    }

    void PrepareTurnLeft()
    {
        _directionAngleTo = _directionAngleFrom - 90f;
    }

    private void PrepareTurnAround()
    {
        _directionAngleTo = _directionAngleFrom + 180f;
    }
}
