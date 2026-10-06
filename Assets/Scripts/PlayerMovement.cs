using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController _characterController;
    public float PlayerSpeed = 5f;
    //Awake()
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        //si se pulsa tecla, movemos el objeto
        Vector3 move = new Vector3(Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue(), 0, Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue())*Runner.DeltaTime*PlayerSpeed;

        _characterController.Move(move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;

        }

    }
}
