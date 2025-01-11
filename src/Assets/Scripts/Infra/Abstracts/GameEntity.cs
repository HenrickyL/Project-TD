using UnityEngine;

public abstract class GameEntity : GameAsset
{
    [SerializeField]
    Transform _model = default;
    public Vector3 ModelLocalPosition {
        get => _model.localPosition;
        set{ _model.localPosition = value; }
    }
    public Vector3 ModelLocalScale
    {
        get => _model.localScale;
        set { _model.localScale = value; }
    }


    [SerializeField]
    AnimationStateController _animController;
    public AnimationStateController AnimationController
    {
        get => _animController;
        protected set { _animController = value; }
    }

    public float PathOffset { get; set; }
    public float Speed { get; set; }


    public abstract void SpawnOn(GameTile tile);
    public abstract bool GameUpdate();
}
