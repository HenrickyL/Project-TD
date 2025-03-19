using Perikan.Infra.Gameplay;
using Perikan.Infra.VFX;
using UnityEngine;

namespace Perikan.Infra.Projectiles
{
    // particleSystem(PS) ;
    public class ProjectilePSController : MonoBehaviour, IProjectile
    {
        [SerializeField]protected ParticleCollisionHandler _projectile;
        [SerializeField]protected ParticleSystemController _explosion;
        [SerializeField]protected float _startSpeed = 0f;
        [SerializeField]protected Transform _launchPoint = default;
        [SerializeField]protected Transform _targetPoint = default;

        private ParticleSystem Projectile => _projectile.Particle;

        private void Start()
        {
            _projectile.OnCollision = HandleProjectileHit;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 targetPoint = _targetPoint.position;
                Vector3 launchPoint = _launchPoint.position;
                AllReset();
                Initialize(launchPoint, targetPoint);
                _projectile.ResetAndPlay();
            }
        }

        private void ApplyTransformations(Vector3 launchPoint, Vector3 targetPoint, float distance, float angleX, float angleY) {
            AllReset();
            //_projectile.transform.position = launchPoint;//new Vector3(0,0, distance);
            //transform.position = targetPoint;
            //_projectile.transform.position = launchPoint; // Posição local relativa ao prefab
            //transform.rotation = Quaternion.Euler(0, angleY, 0); // Aplica a rotação Y
            //_projectile.Particle.transform.localRotation = Quaternion.Euler(180 + angleX, 0, 0);

            _explosion.transform.position = targetPoint;
            _projectile.transform.position = launchPoint; // Posição local relativa ao prefab
            _projectile.transform.rotation = Quaternion.Euler(0, angleY, 0); // Aplica a rotação Y
            Projectile.transform.localRotation = Quaternion.Euler(180+angleX, 0, 0);
        }

        private void AllReset()
        {
            _explosion.Stop();
            Projectile.Stop();
        }

        public void Initialize(Vector3 launchPoint, Vector3 targetPoint)
        {
            //// Define a posição do prefab no targetPoint
            //transform.position = targetPoint;
            //_projectile.transform.position = launchPoint; // Posição local relativa ao prefab

            // Calcula a direção no plano XZ e determina a rotação em Y
            Vector3 direction = launchPoint - targetPoint;
            direction.y = 0; // Ignora a diferença de altura
            // Distância horizontal (XZ) e diferença de altura
            float distanceXZ = direction.magnitude;
            float heightDifference = launchPoint.y - targetPoint.y;

            float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0, angleY, 0); // Aplica a rotação Y

            // Gravidade do sistema de partículas
            float gravity = Projectile.main.gravityModifier.constant;

            // Calcula ângulo X e velocidade inicial
            float angleX, startSpeed;
            CalculateLaunchParameters(distanceXZ, heightDifference, gravity, out angleX, out startSpeed);

            // Define a rotação X do projétil (ângulo de lançamento)
            //_projectile.transform.localRotation = Quaternion.Euler(180+angleX, 0, 0);
            Debug.Log($"H: {heightDifference} - D: {distanceXZ} - X: {angleX} - speed: {startSpeed} - g: {gravity}");
            // Aplica a velocidade inicial no sistema de partículas
            var main = Projectile.main;
            main.startSpeed = startSpeed;

            ApplyTransformations(launchPoint, targetPoint, distanceXZ, angleX, angleY); 
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


        public void HandleProjectileHit(Vector3 position, GameObject hitObject)
        {
            //_projectile.Stop();
            _explosion.transform.position = position;
            _explosion.Play();
        }
    }
}
