using Perikan.AI;
using System;
using UnityEngine;

namespace Perikan.Gameplay.Map { 
    public class GameTile : MonoBehaviour, IState<GameTile>, IEquatable<GameTile>
    {
        [SerializeField]
        private GameTile[] _neighbors;

        GameTileContent _content;

        public bool isWall => _content.Type == GameTileContentType.Wall;
        public bool isEmpty => _content.Type == GameTileContentType.Empty;
        public bool isDestination => _content.Type == GameTileContentType.Destination;
        public Vector3 ExitPoint { get; private set; }


        public Direction PathDirection { get; private set; }

        public GameTileContent Content
        {
            get => _content;
            set
            {
                Debug.Assert(value != null, "Null assigned to content!");
                if (_content != null)
                {
                    _content.Recycle();
                }
                _content = value;
                // Definir o pai da instância
                _content.transform.SetParent(this.transform, false);
            }
        }

        public Direction[] NeighborsOrder {
            get {
                return IsAlternative ?
                    DirectionOrder.GetPrimary() :
                    DirectionOrder.GetReverse();
            }
        }

        public GameTile[] Neighbors => _neighbors;

        public GameTile North { get { return _neighbors[(int)Direction.North]; } set { _neighbors[(int)Direction.North] = value; } }
        public GameTile East { get { return _neighbors[(int)Direction.East]; } set { _neighbors[(int)Direction.East] = value; } }
        public GameTile South { get { return _neighbors[(int)Direction.South]; } set { _neighbors[(int)Direction.South] = value; } }
        public GameTile West { get { return _neighbors[(int)Direction.West]; } set { _neighbors[(int)Direction.West] = value; } }
    
        [SerializeField]
        GameTile _nextOnPath;
        public GameTile NextTileOnPath => _nextOnPath;

        [SerializeField]
        int _distance;

        public int Distance { get { return _distance; } }
        public bool HasPath => _distance != int.MaxValue;

        private Vector2Int _position;
        public bool IsAlternative { get; set; }


        /* -------------------------------------------------------------- */

        private void Awake()
        {
            _neighbors = new GameTile[(int)Direction.West + 1];
            this.PathDirection = Direction.North;
        }
        public GameTile GrowPathTo(GameTile neighbor, Direction direction)
        {
            Debug.Assert(HasPath, "No path!");
            //if (neighbor == null || neighbor.HasPath)
            //{
            //    return null;
            //}
            neighbor._distance = _distance + 1;
            neighbor._nextOnPath = this;
            neighbor.ExitPoint = (neighbor.transform.localPosition + this.transform.localPosition) * 0.5f;
            neighbor.PathDirection = direction.Inverse();
            return neighbor.Content.BlocksPath ? null : neighbor;
        }

        /* -------------------------------------------------------------- */


        public void MakeBelowNeighbors(GameTile below)
        {
            Debug.Assert(
                this.North == null && below.South == null, "Redefined neighbors!"
            );
            this.South = below;
            below.North = this;
        }

        public void MakeLeftNeighbors(
            GameTile left
        )
        {
            Debug.Assert(
                this.West == null && left.East == null, "Redefined neighbors!"
            );
            this.West = left;
            left.East = this;
        }

        public void ClearPath()
        {
            _distance = int.MaxValue;
            _nextOnPath = null;
        }

        public void BecomeDestination()
        {
            _distance = 0;
            _nextOnPath = null;
            ExitPoint = transform.localPosition;
        }

        public GameTile Next() {
            return _nextOnPath;
        }

        public void ShowPath() {
            TileArrow arrow = _content?.Content as TileArrow;
            if (isEmpty && arrow != null)
                arrow.Enable();
            if (_distance == 0)
            {
                return;
            }
            if(_nextOnPath != null)
                arrow?.RotateTo(_nextOnPath.PathDirection);
        }

        public void HidePath() {
            if (Content.Content is TileArrow arrow) {
                arrow.Disable();
            }
        }

        public float DistanceTo(GameTile tile) { 
            return Vector3.Distance(this.transform.position, tile.transform.position);
        }

        GameTile[] IState<GameTile>.GetChilds()
        {
            return _neighbors;
        }

        public bool Equals(GameTile other)
        {
            return _position.x == other._position.x && _position.y == other._position.y;
        }

        public void SetPosition(Vector2Int pos) {
            _position = pos;
        }
    }
}