using UnityEngine;

namespace Perikan.Gameplay.Entity.War
{
    public class Explosion : WarEntity
    {

        [SerializeField, Range(0f, 1f)] float _duration = 0.5f;
        float age;
        Vector3 finalScale;

        /* -------------------------------------------------------------------------- */
        public void Initialize(Vector3 position, float blastRadius, float damage)
        {
            TargetPoint.FillBuffer(position, blastRadius);
            for (int i = 0; i < TargetPoint.BufferedCount; i++)
            {
                TargetPoint.GetBuffered(i).Enemy.ApplyDamage(damage);
            }
            transform.position = position;
            transform.localScale = Vector3.zero;
            finalScale = Vector3.one * (2f * blastRadius);
        }

        public override void GameUpdate()
        {
            base.GameUpdate();
            age += Time.deltaTime;

            float t = Mathf.Clamp01(age / _duration);
            transform.localScale = Vector3.Lerp(Vector3.zero, finalScale, t);
            if (age >= _duration) {
                this.Recycle();
            }
        }

    }
}

