using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvent : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private ParticleSystem seedParticle;
    [SerializeField] private ParticleSystem waterParticle;

    [Header("Events")]
    [SerializeField] private UnityEvent startHarvestingEvent;
    [SerializeField] private UnityEvent stopHarvestingEvent;

    public void seedParticlePlay()
    {
        seedParticle.Play();    
    }
    public void waterParticlePlay()
    {
        waterParticle.Play();
    }

    private void startHarvestingCallBack()
    {
        startHarvestingEvent?.Invoke();
    }
    private void stopHarvestingCallBack()
    {
        stopHarvestingEvent?.Invoke();
    }
}
