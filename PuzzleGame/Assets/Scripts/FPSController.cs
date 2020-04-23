using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson;

public class FPSController : MonoBehaviour {

    public float walkSpeed = 3f;
    public float turnSpeed = 150.0f;
    public float gravityMultiplier = 9.81f;

    private CharacterController controller;
    private MouseLook mouseLook;
    private Camera cameraComponent;
    Vector3 moveDir = Vector3.zero;
    Vector2 input = Vector2.zero;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        cameraComponent = GetComponentInChildren<Camera>();

        mouseLook = new MouseLook();
        mouseLook.Init(transform, cameraComponent.transform);
	}

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update () {

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (input.sqrMagnitude > 1)
        {
            input.Normalize();
        }

        Vector3 DesiredMove = transform.forward * input.y + transform.right * input.x;
        moveDir.x = DesiredMove.x * walkSpeed;
        moveDir.z = DesiredMove.z * walkSpeed;

        if (!controller.isGrounded)
        {
            moveDir += Physics.gravity * gravityMultiplier * Time.deltaTime;
        }
        
        controller.Move(moveDir * Time.deltaTime);

        mouseLook.UpdateCursorLock();

        mouseLook.LookRotation(transform, cameraComponent.transform);

    }
}
