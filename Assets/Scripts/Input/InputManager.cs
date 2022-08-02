using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : Singleton<InputManager>
{
    public bool IsMovingPlayer1 { get; private set; }
    public Vector2 MoveDirectionPlayer1 { get; private set; }

    public bool IsMovingPlayer2 { get; private set; }
    public Vector2 MoveDirectionPlayer2 { get; private set; }

    public bool IsStonePlayer1 { get; private set; }
    public bool IsStonePlayer2 { get; private set; }
    public bool IsPause { get; private set; } = false;

    public void MovePressedPlayer1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsMovingPlayer1 = true;
            MoveDirectionPlayer1 = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            IsMovingPlayer1 = false;
            MoveDirectionPlayer1 = context.ReadValue<Vector2>();
        }
    }

    public void MovePressedPlayer2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsMovingPlayer2 = true;
            MoveDirectionPlayer2 = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            IsMovingPlayer2 = false;
            MoveDirectionPlayer2 = context.ReadValue<Vector2>();
        }
    }

    public void StonePressedPlayer1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsStonePlayer1 = true;
        }
        else
        {
            IsStonePlayer1 = false;
        }
    }
    public void StonePressedPlayer2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsStonePlayer2 = true;
        }
        else
        {
            IsStonePlayer2 = false;
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed) GameController.Instance.PauseButtonPressed();
    }
}
