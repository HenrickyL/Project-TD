using Perikan.Infra.Gameplay;
using UnityEngine;

namespace Perikan.Gameplay.Entity
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : GameAsset
    {
        private void Update()
        {
            if(transform.position.y <=0 )
                this.Recycle();
        }

        public void SpawnOn(Vector3 position, Vector3 speed) {
            this.transform.position = position;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(speed*10f);
        }
        public override void Recycle()
        {
            Destroy(gameObject);
        }
    }
}
