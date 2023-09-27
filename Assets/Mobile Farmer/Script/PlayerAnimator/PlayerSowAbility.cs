using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerSowAbility : MonoBehaviour
{
    [Header("Elements")]
    private PlayerAnimator playerAnimator;
    private PlayerToolSelector playerToolSelector;

    [Header("Settings")]
    private CropField currentCropField;
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerToolSelector = GetComponent<PlayerToolSelector>();
        SeedParticles.onSeedsCollided += SeedsCollidedCallback;
        CropField.onFullySown += CropFiledFullySownCallback;
        playerToolSelector.onToolSelected += ToolSelecedCallback;
    }

    private void OnDestroy()
    {
        SeedParticles.onSeedsCollided -= SeedsCollidedCallback;
        CropField.onFullySown -= CropFiledFullySownCallback;
        playerToolSelector.onToolSelected -= ToolSelecedCallback;
    }

    private void ToolSelecedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!playerToolSelector.CanSow())
            playerAnimator.stopSowAnimation();
    }
    private void SeedsCollidedCallback(Vector3[] seedPositions)
    {
        if (currentCropField == null)
            return;

        currentCropField.SeedCollidedCallback(seedPositions);

    }

    private void CropFiledFullySownCallback(CropField cropField)
    {
        if(cropField==currentCropField)
            playerAnimator.stopSowAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField")&&other.GetComponent<CropField>().IsEmpty())
        {
            currentCropField = other.GetComponent<CropField>();
            EnteredCropField(currentCropField);
        }
    }

    private void EnteredCropField(CropField cropField)
    {
        if(playerToolSelector.CanSow())
            playerAnimator.playSowAnimation();


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            EnteredCropField(other.GetComponent<CropField>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            playerAnimator.stopSowAnimation();
            currentCropField = null;
        }
    }
}
