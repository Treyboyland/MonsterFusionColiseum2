using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileCursor : MonoBehaviour
{
    [SerializeField]
    GameField gameField;

    [SerializeField]
    GameManager manager;

    [SerializeField]
    GameCardsInPlay gameCardsInPlay;

    public bool CanMove = true;

    Vector2Int currentPosition;

    public void SetPosition(Vector2Int pos)
    {
        currentPosition = manager.AttemptMoveToPosition(pos, currentPosition);
        transform.position = gameField.GetTileAtPosition(currentPosition);
    }

    #region Input Controls
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();

        if (movementVector == Vector2.zero)
        {
            return;
        }

        if (context.phase != InputActionPhase.Started)
        {
            return;
        }

        if (Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.y))
        {
            SetPosition(currentPosition + (movementVector.x > 0 ? Vector2Int.right : Vector2Int.left));
        }
        else
        {
            SetPosition(currentPosition + (movementVector.y > 0 ? Vector2Int.up : Vector2Int.down));
        }
    }

    public void OnAccept(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            manager.OnPositionSelected(currentPosition);
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            manager.OnPositionCancel(currentPosition);
        }
    }

    public void OnFlip(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            manager.OnPositionFlip(currentPosition);
        }
    }

    public void OnInfo(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            manager.OnPositionInfo(currentPosition);
        }
    }

    public void OnAbility(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            manager.OnPositionAbility(currentPosition);
        }
    }
    #endregion
}
