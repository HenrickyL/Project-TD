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


    private float _healthMax = 0f;

    public float HealthMax { get { return _healthMax; } set { _healthMax = value; } }

    private float _health = 0f;
    public float Health { 
        get { return _health; }
        protected set { _health = value; }
    }
    public bool IsAlive=> _health > 0;

    private IEntityState _currentState;

    [SerializeField]
    private string _stateName = default;

    public IEntityState CurrentState => _currentState;

    /* ------------------------------------------------- */
    public virtual void SetDeath() {
        _health = 0f;
    }
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
