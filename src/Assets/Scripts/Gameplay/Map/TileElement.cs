using UnityEngine;

namespace Perikan.Gameplay.Map{
    public class TileElement : MonoBehaviour
    {
        public bool IsActive { get; set; } = false;

        public virtual void Initialize()
        {
        }

        public virtual void Toggle()
        {
            IsActive = !IsActive;
        }

        public virtual void Enable()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            this.gameObject.SetActive(false);
        }
    }
}
