using Perikan.Gameplay.Entity.War;
using Perikan.Infra.Factory;
using UnityEngine;

namespace Perikan.Gameplay.Factory
{
    [CreateAssetMenu(fileName = "WarFactory", menuName = "Factory/WarFactory")]
    public class WarFactory : AbstractGameEntityFactory
    {
        [SerializeField]Projectile projectilePrefab = default;
        [SerializeField]Explosion explosionPrefab = default;

        public WarEntity Get<T>() where T: WarEntity
        {
            return typeof(T) switch
            {
                var type when type == typeof(Projectile) => base.Get(projectilePrefab),
                var type when type == typeof(Explosion) => base.Get(explosionPrefab),
                _ => throw new System.ArgumentException($"Tipo desconhecido: {typeof(T)}")
            };
        }
    }
}