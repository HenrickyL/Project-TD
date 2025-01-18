using Perikan.Gameplay.Controller;
using UnityEngine;

namespace Perikan.Gameplay.Map { 
    public class TileArrow : TileElement
    {
        [SerializeField]
        private Direction _direction = Direction.North;

        private Renderer arrowRenderer;

        private Quaternion initialRotation;
        private Quaternion targetRotation;
        private float rotationProgress;
        public float Angle { get { return _direction.GetAngle(); } }
        private bool _isAnimating = true; // Toggle to remove animation


        private void Awake()
        {
            transform.localRotation = Quaternion.Euler(90, Angle,0);
            arrowRenderer = GetComponent<Renderer>();
            ResetMaterial();
        }

        private void Update()
        {
            if (_isAnimating && rotationProgress < 1)
            {
                rotationProgress += Time.deltaTime * 0.5f; // Interpolação suave da rotação
                transform.localRotation = Quaternion.Lerp(initialRotation, targetRotation, rotationProgress);
            }
        }

        private void Start()
        {
            ResetRotation();
        }

        public override void Initialize()
        {
            base.Initialize();
            ResetRotation();
        }

        private void ResetRotation()
        {
            targetRotation = Quaternion.Euler(90, Angle, 0);
            if (_isAnimating)
            {
                initialRotation = transform.localRotation;
                rotationProgress = 0.0f;
            }
            else {
                transform.localRotation = targetRotation;
            }
        }

        /* ---------------------------------------------- */
        public void RotateTo(Direction dir)
        {
            _direction = dir;
            ResetRotation();
        }


        public override void Toggle() {
            base.Toggle();
            ResetMaterial();
        }

        private void ResetMaterial() {
            Material currentMaterial = IsActive ? GameController.ArrowMaterial : GameController.ArrowDisableMaterial;
            arrowRenderer.material = currentMaterial;
        }
    }
}