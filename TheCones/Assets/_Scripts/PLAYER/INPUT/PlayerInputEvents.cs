using System;
using UnityEngine.InputSystem;

namespace Cimmerial
{
    public class PlayerInputEvents
    {
        public event Action<InputAction.CallbackContext> OnMovePerformed;
        public event Action<InputAction.CallbackContext> OnMoveCanceled;
        public event Action<InputAction.CallbackContext> OnLook;
        // public event Action OnMoveCanceled;
        // public event Action OnShiftStarted;
        // public event Action OnShiftCanceled;

        public event Action OnJumpStarted;

        // public event Action OnLeftClickStarted;
        // public event Action OnRightClickStarted;
        // public event Action OnDoubleLeftClick;

        // public event Action<InputAction.CallbackContext> OnScrollPerformed;


        public void MovePerformed(InputAction.CallbackContext context) => OnMovePerformed?.Invoke(context);
        public void MoveCanceled(InputAction.CallbackContext context) => OnMoveCanceled?.Invoke(context);
        public void Look(InputAction.CallbackContext context) => OnLook?.Invoke(context);
        // public void MoveCanceled() => OnMoveCanceled?.Invoke();
        // public void ShiftStarted() => OnShiftStarted?.Invoke();
        // public void ShiftCanceled() => OnShiftCanceled?.Invoke();
        public void JumpStarted() => OnJumpStarted?.Invoke();

        // public void LeftClickStarted() => OnLeftClickStarted?.Invoke();
        // public void RightClickStarted() => OnRightClickStarted?.Invoke();

        // public void ScrollPerformed(InputAction.CallbackContext context) => OnScrollPerformed?.Invoke(context);

        // public void EscapeStarted() => OnEscapeStarted?.Invoke();

    }
}
