using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
       
        return inputVector.normalized;
    }

}
