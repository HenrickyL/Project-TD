using System.Linq;
using UnityEngine;

namespace Perikan.Infra.Animation
{
    public enum AnimationStateEnum { 
        Idle, Walk, Attack,Hit, Death, Spawn
    }

    public enum AnimationParamsEnum
    {
        MovimentY
    }

    public enum AnimationTypeEnum
    {
        Idle, Walk, Attack, Hit, JumpIdle, JumpLand, Death
    }

    [RequireComponent(typeof(Animator))]
    public class AnimationStateController : MonoBehaviour
    {
        Animator animator;
        private int[] stateHash;
        private int distanceYHash;
        private AnimationClip[] animationClips;

        private int _speedParameter;

        void Awake()
        {
            animator = GetComponent<Animator>();
            SetStateHash();
            CacheAnimationClips();
            distanceYHash = Animator.StringToHash(AnimationParamsEnum.MovimentY.ToString());
            _speedParameter = Animator.StringToHash("speed");
        }

        public void ChangeAnimator(AnimationStateEnum state, float value = 1) {
            switch (state) {
                case AnimationStateEnum.Spawn:
                    animator.SetFloat(distanceYHash, value);
                    break;
                default:
                    animator.SetBool(stateHash[(int)state], value != 0);
                    break;
            }
        }

        public float GetAnimationLength(AnimationTypeEnum state)
        {
            AnimationClip clip = animationClips[(int)state];

            if (clip == null)
            {
                Debug.LogWarning($"Animation clip for state '{state}' not found.");
                return 0f; // Retorna 0 se o clipe não for encontrado
            }
            return clip.length/animator.speed;
        }
        public void SetAnimationSpeed(AnimationTypeEnum state, float finalTime)
        {
            if (finalTime <= 0)
            {
                Debug.LogWarning("A velocidade da animação deve ser maior que zero.");
                return;
            }

            AnimationClip clip = animationClips[(int)state];
            if (clip == null)
            {
                Debug.LogWarning($"Animation clip for state '{state}' not found.");
                return;
            }

            string parameterName = state.ToString();
            int parameterHash = Animator.StringToHash(parameterName);

            if (!animator.parameters.Any(p => p.nameHash == parameterHash))
            {
                Debug.LogWarning($"Animator parameter '{parameterName}' not found.");
                return;
            }
            float x = animator.GetFloat(_speedParameter);
            float y = animator.GetFloat("speed");
            float z = animator.speed;
            animator.speed = clip.length * finalTime;


            //animator.SetFloat(_speedParameter, finalTime / clip.length);
        }

        public void ResetAnimationSpeed(AnimationTypeEnum state)
        {

            AnimationClip clip = animationClips[(int)state];
            if (clip == null)
            {
                Debug.LogWarning($"Animation clip for state '{state}' not found.");
                return;
            }

            string parameterName = state.ToString();
            int parameterHash = Animator.StringToHash(parameterName);

            if (!animator.parameters.Any(p => p.nameHash == parameterHash))
            {
                Debug.LogWarning($"Animator parameter '{parameterName}' not found.");
                return;
            }

            animator.SetFloat(_speedParameter, 1);
        }

        /* ---------------------------------------- */

        private void SetStateHash() {
            stateHash = new int[(int)AnimationStateEnum.Death + 1];
            for (AnimationStateEnum state = AnimationStateEnum.Idle; state <= AnimationStateEnum.Death; state++)
            {
                stateHash[(int)state] = Animator.StringToHash(state.ToString());
            }
        }

        private void CacheAnimationClips()
        {
            // Mapeia os clipes para cada estado
            animationClips = new AnimationClip[(int)AnimationTypeEnum.Death + 1];
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

            for (AnimationTypeEnum type = AnimationTypeEnum.Idle; type <= AnimationTypeEnum.Death; type++)
            {
                string AnimationName = type.ToString();
                animationClips[(int)type] = clips.FirstOrDefault(c => c.name == AnimationName);
            }
        }

    }
}