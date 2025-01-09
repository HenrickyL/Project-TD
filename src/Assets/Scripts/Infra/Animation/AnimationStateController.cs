using UnityEngine;

public enum AnimationStateEnum { 
    Idle, Walk, Death
}

[RequireComponent(typeof(Animator))]
public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    private int[] stateHash;

    void Awake()
    {
        animator = GetComponent<Animator>();
        SetStateHash();
    }

    public void ChangeAnimator(AnimationStateEnum state) {
        animator?.SetBool(stateHash[(int)state], true);
    }

    private void SetStateHash() {
        stateHash = new int[(int)AnimationStateEnum.Death + 1];
        for (AnimationStateEnum state = AnimationStateEnum.Idle; state < AnimationStateEnum.Death; state++)
        {
            stateHash[(int)state] = Animator.StringToHash(state.ToString());
        }
    }
}
