using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyFactory originFactory;

    GameTile _tileFrom, _tileTo;
    Vector3 _positionFrom, _positionTo;
    float _progress;

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
        _positionFrom = _tileFrom.transform.localPosition;
        _positionTo = _tileFrom.ExitPoint;
        transform.localRotation = _tileFrom.PathDirection.GetRotation();
        _progress = 0f;
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
            _positionFrom = _positionTo;
            _positionTo = _tileFrom.ExitPoint;
            transform.localRotation = _tileFrom.PathDirection.GetRotation();
            _progress -= 1f;
        }
        transform.localPosition = Vector3.LerpUnclamped(_positionFrom, _positionTo, _progress);
        return true;
    }
}
