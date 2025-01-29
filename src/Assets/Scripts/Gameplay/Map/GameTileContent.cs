using Perikan.Infra.Gameplay;
using UnityEngine;

namespace Perikan.Gameplay.Map { 
    public class GameTileContent : GameElement
    {
        [SerializeField]
        private GameTileContentType _type = default;
        public GameTileContentType Type => _type;

        public GameTile Tile { get; set; } = null;

        [SerializeField]
        private GameAsset _tileElement = default;

        public GameAsset Element => _tileElement;

        public bool BlocksPath =>
            Type == GameTileContentType.Wall || Type == GameTileContentType.Tower;

        public override bool IsAlive => true;
    }
}