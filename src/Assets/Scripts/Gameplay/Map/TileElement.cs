using Perikan.Infra.Gameplay;

namespace Perikan.Gameplay.Map{
    public class TileElement : GameAsset
    {
        public bool IsActive { get; set; } = false;

        public virtual void Enable()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            this.gameObject.SetActive(false);
        }

        public override void Recycle()
        {
            Destroy(this.gameObject);
        }
    }
}
