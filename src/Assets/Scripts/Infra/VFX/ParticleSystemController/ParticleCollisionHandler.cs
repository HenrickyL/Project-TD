using System;
using System.Collections.Generic;
using UnityEngine;

namespace Perikan.Infra.VFX
{
    public class ParticleCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _projectile;

    public ParticleSystem Particle => _projectile;

    public Action<Vector3, GameObject> OnCollision { get; set; }

    private void Start()
    {
        var main = _projectile.main;
        main.playOnAwake = false;
        _projectile.Stop();
    }

    void OnParticleCollision(GameObject other)
    {
        //Vector3 collisionPosition = transform.position;
        //OnCollision.Invoke(collisionPosition, other);
        List<ParticleCollisionEvent> _collisionEvents = new List<ParticleCollisionEvent>();
        int numCollisionEvents = _projectile.GetCollisionEvents(other, _collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 collisionPosition = _collisionEvents[i].intersection;
            OnCollision.Invoke(collisionPosition, other);
        }
    }

    public void ResetAndPlay()
    {
        this.Stop();
        _projectile.Play();
    }

    public void Stop()
    {
        _projectile.Clear();
        //Particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        Particle.Stop();
    }
}
}
