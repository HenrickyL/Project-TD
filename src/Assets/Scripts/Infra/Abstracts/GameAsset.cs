using UnityEngine;

namespace Perikan.Infra.Gameplay
{
    [SelectionBase]
    public abstract class GameAsset : MonoBehaviour
    {
        public Vector3 LocalPosition => gameObject.transform.localPosition;
        public Vector3 Position => gameObject.transform.position;
        public virtual void Initialize() { }
        public abstract void Recycle();

        public virtual void GameUpdate() { }
    }
}