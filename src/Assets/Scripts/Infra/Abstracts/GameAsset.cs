using UnityEngine;

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


    /* --------------------------------------------------- */

    public virtual void GameUpdate() { 
    }

    public virtual void Recycle()
    {
        OriginFactory.Reclaim(this);
    }
}
