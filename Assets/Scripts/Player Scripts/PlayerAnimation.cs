using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    public void PlayRun(float speed) {
        playerAnimator.SetFloat(TagManager.RUN_ANIMATION_PARAMETER, speed);
    }
    
    public void PlayJump(bool isGrounded) {
        playerAnimator.SetBool(TagManager.JUMP_ANIMATION_PARAMETER, isGrounded);
    }
}
