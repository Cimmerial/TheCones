using UnityEngine;

namespace Cimmerial
{
    public class PlayerInputManager : MonoBehaviour
    {
        private void Awake()
        {
            PlayerInputActions playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Move.performed += ctx => EventsManager.instance.playerInputEvents.MovePerformed(ctx);
            playerInputActions.Player.Move.canceled += ctx => EventsManager.instance.playerInputEvents.MoveCanceled(ctx);
            // playerInputActions.Player.Move.canceled += ctx => EventsManager.instance.playerInputEvents.MoveCanceled();

            playerInputActions.Player.Jump.started += ctx => EventsManager.instance.playerInputEvents.JumpStarted();

            playerInputActions.Player.Look.performed += ctx => EventsManager.instance.playerInputEvents.Look(ctx);
            // playerInputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;
        }
    }
}
