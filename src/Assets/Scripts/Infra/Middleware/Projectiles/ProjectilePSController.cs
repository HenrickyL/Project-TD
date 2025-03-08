using Perikan.Infra.Gameplay;
using Perikan.Infra.VFX;
using System;
using UnityEngine;

namespace Perikan.Infra.Projectiles
{
    // particleSystem(PS) ;
    public class ProjectilePSController : ParticleSystemController, IProjectile
    {
        [SerializeField]protected ParticleCollisionHandler _projectile;
        [SerializeField]protected float _startSpeed = 0f;
        [SerializeField]protected Transform _launchPoint = default;
        [SerializeField]protected Transform _targetPoint = default;

        private ParticleSystem Projectile => _projectile.Particle;

        private void Start()
        {
            _projectile.OnCollision = HandleProjectileHit;
            _startDelay = 0;
            ApplyStartDelay();
        }

        public void Initialize(Vector3 launchPoint, Vector3 targetPoint)
        {
            // Define a posição do prefab no targetPoint
            transform.position = targetPoint;
            _projectile.transform.position = launchPoint; // Posição local relativa ao prefab

            // Calcula a direção no plano XZ e determina a rotação em Y
            Vector3 direction = launchPoint - targetPoint;
            direction.y = 0; // Ignora a diferença de altura
            float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angleY, 0); // Aplica a rotação Y

            // Distância horizontal (XZ) e diferença de altura
            float distanceXZ = direction.magnitude;
            float heightDifference = launchPoint.y - targetPoint.y;

            // Gravidade do sistema de partículas
            float gravity = Projectile.main.gravityModifier.constant;

            // Calcula ângulo X e velocidade inicial
            float angleX, startSpeed;
            CalculateLaunchParameters(distanceXZ, heightDifference, gravity, out angleX, out startSpeed);

            // Define a rotação X do projétil (ângulo de lançamento)
            _projectile.transform.localRotation = Quaternion.Euler(180+angleX, 0, 0);
            Debug.Log($"H: {heightDifference} - D: {distanceXZ} - X: {angleX} - speed: {startSpeed} - g: {gravity}");
            // Aplica a velocidade inicial no sistema de partículas
            var main = Projectile.main;
            main.startSpeed = startSpeed;
        }
        

        private static void CalculateLaunchParameters(float distance, float heightDifference, float gravity, out float angleX, out float startSpeed)
        {
            //TODO: improve the calculation
            angleX = 55;
            float sinValue = Mathf.Sin(angleX * Mathf.Deg2Rad);
            float cosValue = Mathf.Cos(angleX * Mathf.Deg2Rad);

            float numerator = gravity * (0.5f + distance);
            float denominator = sinValue * cosValue + (heightDifference/ distance)*cosValue*cosValue;

            float vSquared = numerator / denominator;

            if (denominator <= 0)
            {
                Debug.LogError("Impossível calcular a velocidade inicial. Ajustando valores...");
                startSpeed = 14f; // Valor arbitrário caso o cálculo falhe
                return;
            }

            startSpeed = Mathf.Sqrt(2*vSquared);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                Vector3 targetPoint = _targetPoint.position;
                Vector3 launchPoint = _launchPoint.position;

                //Initialize(launchPoint, targetPoint);
                _projectile.ResetAndPlay();
            }
        }


        private void HandleProjectileHit(Vector3 position, GameObject hitObject)
        {
            //_projectile.Stop();
            PlaySelective();
            Console.WriteLine("1");
        }
    }
}
