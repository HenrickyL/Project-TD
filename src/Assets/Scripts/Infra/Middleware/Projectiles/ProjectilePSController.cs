using UnityEngine;
using Perikan.Infra.Gameplay;
using Perikan.Gameplay.Entity.War;

namespace Perikan.Infra.VFX {
    // particleSystem(PS) ;
    public class ProjectilePSController : ParticleSystemController, IProjectile
    {
        [SerializeField]protected ParticleSystem _projectile;
        [SerializeField]protected float _startSpeed = 0f;
        [SerializeField] protected Transform _launchPoint = default;
        [SerializeField] protected Transform _targetPoint = default;


        public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity)
        {
            //transform.position = launchPoint;
            //if (_projectile != null)
            //{
            //    var main = _projectile.main;
            //    main.startSpeed = _startSpeed;
            //}

            // ------Define a posição inicial do projétil
            transform.position = targetPoint; // ok
            _projectile.transform.position = launchPoint; // ok

            // Calcula a direção no plano horizontal (rotação Y)
            Vector3 direction = launchPoint - targetPoint;
            direction.y = 0; // Ignora a diferença de altura para calcular a rotação Y
            float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Calcula o ângulo de elevação (rotação X) e a velocidade inicial
            float gravity = _projectile.main.gravityModifier.constant;//Physics.gravity.y;
            float distance = direction.magnitude;
            float heightDifference = targetPoint.y - launchPoint.y;

            // Fórmula do lançamento oblíquo para calcular o ângulo X
            float angleX = CalculateLaunchAngle(distance, heightDifference, gravity, _startSpeed); // startSpeed tem que ser descoberto para que alcance o target
            //_projectile.transform.rotation = Quaternion.Euler(angleX, 0, 0);

            // ---------Aplica a rotação ao projétil
            transform.rotation = Quaternion.Euler(0, angleY, 0); // ok

            // Configura a velocidade inicial no sistema de partículas
            if (_projectile != null)
            {
                var main = _projectile.main;
                main.startSpeed = _startSpeed;
            }
        }

        private float CalculateLaunchAngle(float distance, float heightDifference, float gravity, float speed)
        {
            // Fórmula para calcular o ângulo de lançamento
            float speedSquared = speed * speed;
            float discriminant = Mathf.Pow(speedSquared, 2) - gravity * (gravity * distance * distance + 2 * heightDifference * speedSquared);

            if (discriminant < 0)
            {
                Debug.LogError("Velocidade insuficiente para alcançar o alvo!");
                return 45f; // Ângulo padrão se a velocidade for insuficiente
            }

            float numerator = speedSquared + Mathf.Sqrt(discriminant);
            float denominator = gravity * distance;
            float angleRadians = Mathf.Atan(numerator / denominator);

            return angleRadians * Mathf.Rad2Deg;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                Initialize(_launchPoint.position, _targetPoint.position, Vector3.zero);
                this.Play();
            }
        }
    }
}
