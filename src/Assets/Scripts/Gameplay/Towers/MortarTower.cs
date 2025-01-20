using UnityEngine;

namespace Perikan.Gameplay.Entity.Tower {
    public class MortarTower : Tower
    {
        [SerializeField, Range(0.5f, 2f)]
        float shotsPerSecond = 1f;

        [SerializeField]
        Transform mortar = default;

        public override TowerType TowerType => TowerType.Mortar;

        protected override void Shoot()
        {
        }
    }

}