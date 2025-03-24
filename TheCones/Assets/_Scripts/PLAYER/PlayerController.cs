using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Cimmerial
{
    public class PlayerController : MonoBehaviour
    {

        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- VARIABLES -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

        [Header("REFS")]
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform cameraTransform;

        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- BASE FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

        private void Awake()
        {
            // TESTING
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerManager = GetComponent<PlayerManager>();
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            cameraTransform = playerManager.CameraTransform;
        }

        private void OnEnable()
        {
            EventsManager.instance.playerInputEvents.OnMovePerformed += Move_performed;
            EventsManager.instance.playerInputEvents.OnMoveCanceled += Move_canceled;
            EventsManager.instance.playerInputEvents.OnLook += Look;
            EventsManager.instance.playerInputEvents.OnJumpStarted += Jump;
        }

        private void OnDisable()
        {
            EventsManager.instance.playerInputEvents.OnMovePerformed -= Move_performed;
            EventsManager.instance.playerInputEvents.OnMoveCanceled -= Move_canceled;
            EventsManager.instance.playerInputEvents.OnLook -= Look;
            EventsManager.instance.playerInputEvents.OnJumpStarted -= Jump;
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            UpdatePlayerVelocity();
        }

        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- CLASS FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

        private void Move_performed(InputAction.CallbackContext context) => HandleMovement(context.ReadValue<Vector2>());
        private void Move_canceled(InputAction.CallbackContext context) => playerManager.PlayerMoveInput = Vector3.zero;
        private void Look(InputAction.CallbackContext context) => HandleLook(context.ReadValue<Vector2>());

        private void HandleMovement(Vector2 movementDirection)
        {
            // Calculate movement direction relative to camera
            Vector3 forward = transform.forward * movementDirection.y;
            Vector3 right = transform.right * movementDirection.x;
            Vector3 moveDirection = (forward + right).normalized;

            playerManager.PlayerMoveInput = moveDirection;
        }

        private void HandleLook(Vector2 lookDirection)
        {
            // Horizontal rotation (turning)
            transform.Rotate(Vector3.up, lookDirection.x * playerManager.MouseSensitivity);

            // Vertical rotation (looking up/down)
            playerManager.CameraPitch -= lookDirection.y * playerManager.MouseSensitivity;
            playerManager.CameraPitch = Mathf.Clamp(playerManager.CameraPitch, -playerManager.MaxLookAngle, playerManager.MaxLookAngle);
            cameraTransform.localRotation = Quaternion.Euler(playerManager.CameraPitch, 0f, 0f);
        }

        //===================================================================================================================

        private void UpdatePlayerVelocity() => rb.MovePosition(rb.position + playerManager.MoveSpeed * Time.fixedDeltaTime * playerManager.PlayerMoveInput);

        private void Jump()
        {
            if (playerManager.IsGrounded)
            {
                rb.AddForce(Vector3.up * playerManager.JumpForce, ForceMode.Impulse);
            }
        }

        private void CheckGrounded()
        {
            float rayLength = 1.1f;
            playerManager.IsGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength);
        }
    }
}
