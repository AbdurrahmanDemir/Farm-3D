using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem waterParticle;
    [Header("Settings")]
    [SerializeField] private float moveSpeedMultiplier;


    private void Start()
    {
        Time.timeScale = 1;
    }
    public void manageAnimations(Vector3 moveVector)
    {
        if (moveVector.magnitude>0)
        {
            animator.SetFloat("moveSpeed",moveVector.magnitude*moveSpeedMultiplier);
            playRunAnimation();

            animator.transform.forward = moveVector.normalized;
        }
        else
        {
            playIdleAnimation();
        }
    }
    public void playRunAnimation()
    {
        animator.Play("Run");
    }
    public void playIdleAnimation()
    {
        animator.Play("Idle");
    }

    public void playSowAnimation()
    {
        animator.SetLayerWeight(1, 0.35f);
    }

    public void stopSowAnimation()
    {
        animator.SetLayerWeight(1, 0);
    }
    public void PlayWaterAnimation()
    {
        animator.SetLayerWeight(2, 0.35f);
    }
    public void StopWaterAnimation()
    {
        animator.SetLayerWeight(2, 0);
        waterParticle.Stop();
    }
    public void PlayHarvestAnimation()
    {
        animator.SetLayerWeight(3, 0.35f);
    }

    public void StopHarvestAnimation()
    {
        animator.SetLayerWeight(3, 0);
    }


}
