using Perikan.Infra.Gameplay;

namespace Perikan.Gameplay.Entity.War { 
    public class WarEntity : GameAsset
    {
        protected bool _isAlive = true;
        public override bool IsAlive => _isAlive;

        public override void Recycle()
        {
            base.Recycle();
            _isAlive = false;
        }
    }
}
