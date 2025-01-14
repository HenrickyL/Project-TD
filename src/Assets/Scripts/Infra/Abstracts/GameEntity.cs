using UnityEngine;


// Is a StateMachine
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
    public bool IsAlive { get; set; } = true;

    private IEntityState _currentState;

    [SerializeField]
    private string _stateName = default;

    public IEntityState CurrentState => _currentState;

    /* ------------------------------------------------- */
    public abstract void SpawnOn(GameTile tile);
    public override void GameUpdate() {
        base.GameUpdate();
        _currentState?.UpdateState();
    }

    public void ChangeState(IEntityState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _stateName = _currentState.Name();
        _currentState.Enter(this);
    }
}
