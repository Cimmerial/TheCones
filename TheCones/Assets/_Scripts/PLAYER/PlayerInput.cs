using UnityEngine;
using UnityEngine.InputSystem;


namespace Cimmerial
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] public float moveSpeed = 5f;
        [SerializeField] public float jumpForce = 5f;

        [Header("Look Settings")]
        [SerializeField] public float mouseSensitivity = 1f;
        [SerializeField] public Transform cameraTransform;
        [SerializeField] public float maxLookAngle = 80f;

        [Header("STATE")]
        [SerializeField] private bool isGrounded;

        [SerializeField] private PlayerInputActions controls;
        [SerializeField] private Vector2 moveInput;
        [SerializeField] private Vector2 lookInput;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float cameraPitch = 0f;

        private void Awake()
        {
            controls = new PlayerInputActions();
            rb = GetComponent<Rigidbody>();

            // Lock and hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Setup input callbacks
            controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

            controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

            controls.Player.Jump.performed += ctx => Jump();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        private void Update()
        {
            // Handle camera rotation
            HandleLook();
        }

        private void FixedUpdate()
        {
            // Handle movement
            HandleMovement();

            // Check if grounded
            CheckGrounded();
        }

        private void HandleMovement()
        {
            // Calculate movement direction relative to camera
            Vector3 forward = transform.forward * moveInput.y;
            Vector3 right = transform.right * moveInput.x;
            Vector3 moveDirection = (forward + right).normalized;

            // Apply movement
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        private void HandleLook()
        {
            // Horizontal rotation (turning)
            transform.Rotate(Vector3.up, lookInput.x * mouseSensitivity);

            // Vertical rotation (looking up/down)
            cameraPitch -= lookInput.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        }

        private void Jump()
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void CheckGrounded()
        {
            // Simple ground check using raycast
            float rayLength = 1.1f; // Slightly more than half the capsule height
            isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength);
        }
    }
}
