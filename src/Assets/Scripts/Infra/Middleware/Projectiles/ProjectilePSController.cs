using Perikan.Infra.Gameplay;
using UnityEngine;

namespace Perikan.Infra.VFX
{
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

            // Define a posi��o do prefab no targetPoint
            transform.position = targetPoint;
            _projectile.transform.position = launchPoint; // Posi��o local relativa ao prefab

            // Calcula a dire��o no plano XZ e determina a rota��o em Y
            Vector3 direction = launchPoint - targetPoint;
            direction.y = 0; // Ignora a diferen�a de altura
            float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angleY, 0); // Aplica a rota��o Y

            // Dist�ncia horizontal (XZ) e diferen�a de altura
            float distanceXZ = direction.magnitude;
            float heightDifference = launchPoint.y - targetPoint.y;

            // Gravidade do sistema de part�culas
            float gravity = _projectile.main.gravityModifier.constant;

            // Calcula �ngulo X e velocidade inicial
            float angleX, startSpeed;
            CalculateLaunchParameters(distanceXZ, heightDifference, gravity, out angleX, out startSpeed);

            // Define a rota��o X do proj�til (�ngulo de lan�amento)
            _projectile.transform.localRotation = Quaternion.Euler(angleX, 0, 0);
            Debug.Log($"X: {angleX} - speed: {startSpeed}");


            // Aplica a velocidade inicial no sistema de part�culas
            var main = _projectile.main;
            main.startSpeed = startSpeed;
        }
        //private bool CalculateLaunchParameters(float distance, float heightDifference, float gravity, out float angleX, out float startSpeed)
        //{
        //    for (float testAngle = 80f; testAngle >= 10f; testAngle -= 2.5f) // Testa �ngulos mais altos primeiro
        //    {
        //        float angleRad = testAngle * Mathf.Deg2Rad;
        //        float cos2 = Mathf.Cos(angleRad) * Mathf.Cos(angleRad);
        //        float numerator = gravity * distance * distance;
        //        float denominator = 2 * cos2 * (distance * Mathf.Tan(angleRad) - heightDifference);

        //        if (denominator <= 0) continue; // Evita divis�es inv�lidas

        //        float v2 = numerator / denominator;
        //        if (v2 > 0)
        //        {
        //            angleX = testAngle;
        //            startSpeed = Mathf.Sqrt(v2);
        //            return true;
        //        }
        //    }

        //    angleX = 0f;
        //    startSpeed = 0f;
        //    return false;
        //}

        //private bool CalculateLaunchParameters(float distance, float heightDifference, float gravity, out float angleX, out float startSpeed)
        //{

        //    for (float testAngle = 80f; testAngle >= 10f; testAngle -= 2.5f) // Testa �ngulos de 10� a 80�
        //    {
        //        float angleRad = testAngle * Mathf.Deg2Rad;
        //        float cos2 = Mathf.Cos(angleRad) * Mathf.Cos(angleRad);
        //        float numerator = gravity * distance * distance;
        //        float denominator = 2 * cos2 * (distance * Mathf.Tan(angleRad) - heightDifference);

        //        if (denominator <= 0) continue; // Pula �ngulos inv�lidos

        //        float v2 = numerator / denominator;
        //        if (v2 > 0)
        //        {
        //            angleX = testAngle;
        //            startSpeed = Mathf.Sqrt(v2);
        //            return true;
        //        }
        //    }

        //    angleX = 0f;
        //    startSpeed = 0f;
        //    return false;
        //}

        private void CalculateLaunchParameters(float distance, float heightDifference, float gravity, out float angleX, out float startSpeed)
        {
            // Tentamos calcular um �ngulo X adequado
            float vSquared = gravity * distance * distance / (2 * (distance * Mathf.Tan(-45 * Mathf.Deg2Rad) - heightDifference));

            if (vSquared <= 0)
            {
                Debug.LogError("Imposs�vel calcular a velocidade inicial. Ajustando valores...");
                angleX = -45f;
                startSpeed = 14f; // Valor arbitr�rio caso o c�lculo falhe
                return;
            }

            startSpeed = Mathf.Sqrt(vSquared);
            angleX = Mathf.Atan((startSpeed * startSpeed - Mathf.Sqrt(startSpeed * startSpeed * startSpeed * startSpeed - gravity * (gravity * distance * distance + 2 * heightDifference * startSpeed * startSpeed))) / (gravity * distance)) * Mathf.Rad2Deg;
        }

        //private bool CalculateLaunchParameters(float distance, float heightDifference, float gravity, out float angleX, out float startSpeed)
        //{
        //    // Tenta �ngulos de -90 a 0
        //    for (float testAngle = -30f; testAngle >= -75f; testAngle -= 1.75f)
        //    {
        //        float angleRad = testAngle * Mathf.Deg2Rad;
        //        float cos2 = Mathf.Cos(angleRad) * Mathf.Cos(angleRad);
        //        float numerator = gravity * distance * distance;
        //        float denominator = 2 * cos2 * (distance * Mathf.Tan(angleRad) - heightDifference);

        //        if (denominator <= 0) continue; // Pula �ngulos inv�lidos

        //        float v2 = numerator / denominator;
        //        if (v2 > 0)
        //        {
        //            angleX = testAngle;
        //            startSpeed = Mathf.Sqrt(v2);
        //            return true;
        //        }
        //    }

        //    angleX = 0f;
        //    startSpeed = 0f;
        //    return false;
        //}

        //private bool CalculateLaunchParameters(float distance, float heightDifference, float gravity, out float angleX, out float startSpeed)
        //{
        //    // �ngulo de lan�amento inicial (45 graus)
        //    float testAngle = -45f;

        //    // Converte o �ngulo para radianos
        //    float angleRad = testAngle * Mathf.Deg2Rad;

        //    // Calcula o numerador e o denominador da equa��o
        //    float numerator = gravity * distance * distance;
        //    float denominator = 2 * Mathf.Pow(Mathf.Cos(angleRad), 2) * (distance * Mathf.Tan(angleRad) - heightDifference);

        //    // Verifica se o denominador � v�lido
        //    if (denominator <= 0)
        //    {
        //        Debug.LogError("Imposs�vel calcular a velocidade inicial. Ajustando valores...");
        //        angleX = 45f;
        //        startSpeed = 10f; // Valor arbitr�rio caso o c�lculo falhe
        //        return false;
        //    }

        //    // Calcula a velocidade inicial ao quadrado
        //    float vSquared = numerator / denominator;

        //    // Verifica se a velocidade � v�lida
        //    if (vSquared <= 0)
        //    {
        //        Debug.LogError("Velocidade inicial inv�lida. Ajustando valores...");
        //        angleX = 45f;
        //        startSpeed = 10f; // Valor arbitr�rio caso o c�lculo falhe
        //        return false;
        //    }

        //    // Calcula a velocidade inicial e o �ngulo X
        //    startSpeed = Mathf.Sqrt(vSquared);
        //    angleX = testAngle;

        //    return true;
        //}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                Vector3 targetPoint = _targetPoint.position;
                Vector3 launchPoint = _launchPoint.position;

                Initialize(launchPoint, targetPoint, Vector3.zero);
                this.Play();
            }
        }
    }
}
