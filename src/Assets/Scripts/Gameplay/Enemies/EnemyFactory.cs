using Perikan.Gameplay.Entity;
using Perikan.Infra.Factory;
using UnityEngine;

namespace Perikan.Gameplay.Factory
{
    [CreateAssetMenu(fileName = "EnemyFactory", menuName = "Factory/EnemyFactory")]
    public class EnemyFactory : AbstractGameEntityFactory
    {
        [SerializeField]
        Enemy prefab = default;

        [SerializeField, FloatRangeSlider(0.5f, 2f)]
        FloatRange scale = new FloatRange(1f);

        [SerializeField, FloatRangeSlider(0.2f, 5f)]
        FloatRange speed = new FloatRange(1f);

        [SerializeField, FloatRangeSlider(-0.4f, 0.4f)]
        FloatRange pathOffset = new FloatRange(0f);


        public Enemy Get()
        {
            Enemy enemy = base.Get<Enemy>(prefab);
            enemy.Initialize(scale.RandomValueInRange, speed.RandomValueInRange, pathOffset.RandomValueInRange);
            return enemy;
        }
        
    }
}