using System;
using System.Collections.Generic;
using UnityEngine;

namespace Perikan.Infra.VFX
{
    public class ParticleCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _projectile;
    private List<ParticleCollisionEvent> _collisionEvents;
    private bool _hasCollider = false;

    public ParticleSystem Particle => _projectile;

    public Action<Vector3, GameObject> OnCollision { get; set; }

    void Start()
    {
        _collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        //if (_hasCollider)
        //    return;
        //Vector3 collisionPosition = transform.position;
        //OnCollision.Invoke(collisionPosition, other);
        //_hasCollider = true;
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
        _hasCollider = false;
    }
}
}
