using UnityEngine;

[SelectionBase]
public abstract class GameAsset : MonoBehaviour
{
    private AbstractGameAssetFactory _originFactory;
    public AbstractGameAssetFactory OriginFactory
    {
        get => _originFactory;
        set
        {
            Debug.Assert(_originFactory == null, "Redefined origin factory!");
            _originFactory = value;
        }
    }

    public Vector3 LocalPosition => gameObject.transform.localPosition;
    public Vector3 Position => gameObject.transform.position;

    public float Scale { get; protected set; }

    /* --------------------------------------------------- */
    public virtual void GameUpdate() { 
    }

    public virtual void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}
