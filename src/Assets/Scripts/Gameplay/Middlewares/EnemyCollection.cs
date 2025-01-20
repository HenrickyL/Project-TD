using Perikan.Gameplay.Entity;
using System.Collections.Generic;

namespace Perikan.Gameplay { 
    [System.Serializable]
    public class EnemyCollection
    {
        List<Enemy> _enemies = new List<Enemy>();

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void GameUpdate()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                Enemy enemy = _enemies[i];
                if (!enemy.IsAlive)
                {
                    int lastIndex = _enemies.Count - 1;
                    _enemies[i] = _enemies[lastIndex];
                    _enemies.RemoveAt(lastIndex);
                    i -= 1;
                }
                else { 
                    enemy.GameUpdate();
                }
            }
        }
    }
}