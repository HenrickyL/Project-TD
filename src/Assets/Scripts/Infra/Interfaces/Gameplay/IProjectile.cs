using UnityEngine;

namespace Perikan.Infra.Gameplay { 
    public interface IProjectile 
    {
        void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity);
    }
}
