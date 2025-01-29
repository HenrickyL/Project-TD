using Perikan.Infra.Gameplay;
using System.Collections.Generic;

namespace Perikan.Infra.Collections { 
    [System.Serializable]
    public class GameAssetCollection
    {
        List<GameAsset> assets = new List<GameAsset>();

        public void Add(GameAsset enemy)
        {
            assets.Add(enemy);
        }

        public void GameUpdate()
        {
            for (int i = 0; i < assets.Count; i++)
            {
                GameAsset enemy = assets[i];
                if (!enemy.IsAlive)
                {
                    int lastIndex = assets.Count - 1;
                    assets[i] = assets[lastIndex];
                    assets.RemoveAt(lastIndex);
                    i -= 1;
                }
                else { 
                    enemy.GameUpdate();
                }
            }
        }
    }
}