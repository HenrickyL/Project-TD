using UnityEngine;
namespace Perikan.VFX { 
    public class ParticleSystemController : MonoBehaviour
    {
        private ParticleSystem[] _particleSystems;

        void Awake()
        {
            _particleSystems = GetComponentsInChildren<ParticleSystem>();
        }

        public void Play()
        {
            foreach (var ps in _particleSystems)
            {
                ps.Play();
            }
        }

        public void Stop()
        {
            foreach (var ps in _particleSystems)
            {
                ps.Stop();
            }
        }

        //public void SetStartSpeed(float speed)
        //{
        //    foreach (var ps in _particleSystems)
        //    {
        //        var main = ps.main;
        //        main.startSpeed = speed;
        //    }
        //}
    }
}
