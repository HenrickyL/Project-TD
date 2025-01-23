using Perikan.Infra.Factory;
using UnityEngine;

namespace Perikan.Infra.Gameplay
{
    [SelectionBase]
    public abstract class GameAsset : MonoBehaviour
    {
        public Vector3 LocalPosition => gameObject.transform.localPosition;
        public Vector3 Position => gameObject.transform.position;

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

        public virtual void Initialize() { }
        public virtual void GameUpdate() { }

        public virtual void Recycle()
        {
            OriginFactory.Reclaim(this);
        }
    }
}