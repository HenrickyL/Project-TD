using UnityEngine;
using Perikan.Infra.Gameplay;

namespace Perikan.Infra.VFX {
    // particleSystem(PS) ;
    public class ProjectilePSController : ParticleSystemController, IProjectile
    {
        [SerializeField]protected ParticleSystem _projectile;
        [SerializeField]protected float _startSpeed = 0f;
        [SerializeField] protected Vector3 _launchPoint = default;
        [SerializeField] protected Vector3 _targetPoint = default;


        public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity)
        {
            transform.position = launchPoint;
            if (_projectile != null)
            {
                var main = _projectile.main;
                main.startSpeed = _startSpeed;
            }
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                Initialize(_launchPoint, _targetPoint, Vector3.zero);
                this.Play();
            }
        }
    }
}
