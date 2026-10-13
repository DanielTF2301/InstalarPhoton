using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _jumpPressed;


    private CharacterController _controller;

    public Camera cameraPlayer; 
    public float PlayerSpeed = 2f;

    public float JumpForce = 5f;
    public float GravityValue = -9.81f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _jumpPressed = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        // FixedUpdateNetwork is only executed on the StateAuthority

        if (_controller.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
        }

        //Vector3 move = new Vector3(Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue(), 0, Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue()) * Runner.DeltaTime * PlayerSpeed;
        Quaternion cameraRotationY = Quaternion.Euler(0, cameraPlayer.transform.rotation.eulerAngles.y, 0);
        Vector3 move = cameraRotationY * new Vector3(Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue(), 0, Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue()) * Runner.DeltaTime * PlayerSpeed;
        
        
        _velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && _controller.isGrounded)
        {
            _velocity.y += JumpForce;
        }

        _controller.Move(move + _velocity * Runner.DeltaTime);

        //if (move != Vector3.zero)
        {
            //gameObject.transform.forward = move;
            gameObject.transform.forward = new Vector3(cameraPlayer.transform.forward.x, 0, cameraPlayer.transform.forward.z);//.normalized ;
        }

        _jumpPressed = false;
    }

    public override void Spawned()
    {
        if (HasStateAuthority == false)
            return;

        cameraPlayer = Camera.main;
        cameraPlayer.GetComponent<FirstPersonCamera>().Target = transform;
    }
}