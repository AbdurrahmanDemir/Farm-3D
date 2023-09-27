using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private mobilejoystick joystick;
    private PlayerAnimator playerAnimator;
    private CharacterController characterController;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        manageMovement();
    }
    private void manageMovement()
    {
        Vector3 moveVector = joystick.getMoveVector()*moveSpeed*Time.deltaTime/Screen.width;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        characterController.Move(moveVector);

        playerAnimator.manageAnimations(moveVector);
    }

}
