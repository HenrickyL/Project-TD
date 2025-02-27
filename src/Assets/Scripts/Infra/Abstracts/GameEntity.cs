using UnityEngine;

namespace Perikan.Infra.Gameplay
{
    public abstract class GameEntity : GameElement
    {
        [SerializeField]
        Transform _model = default;
        public Vector3 ModelLocalPosition {
            get => _model.localPosition;
            set{ _model.localPosition = value; }
        }
        public Vector3 ModelLocalScale
        {
            get => _model.localScale;
            set { _model.localScale = value; }
        }

        public float PathOffset { get; set; }
        public Vector3 Speed { get; set; }
        public float SpeedMagnitude { get; set; }

        public Vector3 Velocity => Speed * SpeedMagnitude;

        public void SetSpeed(Direction direction)
        {
            Speed = direction.GetVector() * SpeedMagnitude; // Converte a dire��o em vetor e ajusta pela magnitude
        }

        private float _healthMax = 0f;
        public float HealthMax { get { return _healthMax; } set { _healthMax = value; } }
        [SerializeField]
        private float _health = 0f;
        public float Health { 
            get { return _health; }
            protected set { _health = value; }
        }
        public override bool IsAlive => _health > 0;

        /* ------------------------------------------------- */
        public virtual void SetDeath() {
            _health = 0f;
        }
    }
}