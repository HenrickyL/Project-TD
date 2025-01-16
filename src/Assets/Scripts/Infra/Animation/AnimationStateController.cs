using System.Linq;
using UnityEngine;

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

    void Awake()
    {
        animator = GetComponent<Animator>();
        SetStateHash();
        CacheAnimationClips();
        distanceYHash = Animator.StringToHash(AnimationParamsEnum.MovimentY.ToString());
    }

    public void ChangeAnimator(AnimationStateEnum state, float value = 0) {
        switch (state) {
            case AnimationStateEnum.Spawn:
                animator.SetFloat(distanceYHash, value);
                break;
            case AnimationStateEnum.Hit:
                animator.SetBool(stateHash[(int)state], value != 0);
                break;
            default:
                animator.SetBool(stateHash[(int)state], true);
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
        return clip.length;
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
