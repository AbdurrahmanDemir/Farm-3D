using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobilejoystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform joyOutline;
    [SerializeField] private RectTransform joyKnob;
    [Header("Settings")]
    [SerializeField] private float moveFactor;
    private bool canControl;
    private Vector3 clickedPos;
    private Vector3 move;

    void Start()
    {
        hideJoystik();
    }

    void Update()
    {
        if (canControl)
            controlJoystick();
    }

    public void clickedJoystickZone()
    {
        clickedPos = Input.mousePosition;
        joyOutline.position=clickedPos;
        showJoystick();
    }

    public void showJoystick()
    {
        joyOutline.gameObject.SetActive(true);
        canControl = true;
    }
    public void hideJoystik()
    {
        joyOutline.gameObject.SetActive(false);
        canControl = false;

        move = Vector3.zero;

    }
    private void controlJoystick()
    {
        Vector3 currentPos = Input.mousePosition;
        Vector3 direction = currentPos - clickedPos;
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;

        float moveMagnitude = direction.magnitude * moveFactor * canvasScale;

        moveMagnitude = Mathf.Min(moveMagnitude, joyOutline.rect.width / 2);
        float absoluteWidth = joyOutline.rect.width / 2;
        float realWidth = absoluteWidth * canvasScale;
        moveMagnitude = Mathf.Min(moveMagnitude, realWidth);

        move = direction.normalized*moveMagnitude;

        Vector3 targetPos = clickedPos + move;


        joyKnob.position = targetPos;

        if (Input.GetMouseButtonUp(0))
            hideJoystik();
        
    }

    public Vector3 getMoveVector()
    {
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        return move / canvasScale;
    }
}
