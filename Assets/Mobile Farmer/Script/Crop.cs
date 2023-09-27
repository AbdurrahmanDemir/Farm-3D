using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform cropRenderer;
    [SerializeField] private ParticleSystem cropHarvestedParticle;

    public void ScaleUp()
    {
        cropRenderer.gameObject.LeanScale(Vector3.one, 1).setEase(LeanTweenType.easeOutBack);

    }

    public void ScaleDown()
    {
        cropRenderer.gameObject.LeanScale(Vector3.zero, 1).
            setEase(LeanTweenType.easeOutBack).setOnComplete(() => Destroy(gameObject)) ;

        
        cropHarvestedParticle.transform.parent = null;
        cropHarvestedParticle.gameObject.SetActive(true);
        cropHarvestedParticle.Play();
    }

   
}
