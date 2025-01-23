using Perikan.Gameplay.Entity;
using Perikan.Infra.Factory;
using Perikan.Infra.Gameplay;
using UnityEngine;

namespace Perikan.Gameplay.Factory
{
    [CreateAssetMenu(fileName = "WarFactory", menuName = "Factory/WarFactory")]
    public class WarFactory : AbstractGameEntityFactory
    {
        [SerializeField]
        Projectile prefab = default;

        public Projectile Get()
        {
            Projectile projectile = base.Get(prefab);
            return projectile;
        }
    }
}