using System.Linq;
using UnityEngine;

public enum AnimationStateEnum { 
    Idle, Walk, Attack, Death
}

[RequireComponent(typeof(Animator))]
public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    private int[] stateHash;
    private AnimationClip[] animationClips;

    void Awake()
    {
        animator = GetComponent<Animator>();
        SetStateHash();
        CacheAnimationClips();
    }

    public void ChangeAnimator(AnimationStateEnum state) {
        animator.SetBool(stateHash[(int)state], true);
    }

    public float GetAnimationLength(AnimationStateEnum state)
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
        animationClips = new AnimationClip[(int)AnimationStateEnum.Death + 1];
        var clips = animator.runtimeAnimatorController.animationClips;

        for (AnimationStateEnum state = AnimationStateEnum.Idle; state <= AnimationStateEnum.Death; state++)
        {
            string stateName = state.ToString();
            animationClips[(int)state] = clips.FirstOrDefault(c => c.name == stateName);
        }
    }

}
