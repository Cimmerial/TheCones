
// var storage
using System;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Cimmerial
{
    public class PlayerManager : MonoBehaviour
    {

        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- VARIABLES -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

        [Header("STATE")]
        [SerializeField] private PlayerState _playerState;
        [Serializable]
        public class PlayerState
        {

            public Toggleables _toggleables;
            [Serializable]
            public class Toggleables
            {
                public bool isGrounded;
            }

        }

        [Header("INPUT")]
        [SerializeField] private PlayerInput _playerInput;
        [Serializable]
        public class PlayerInput
        {

            public Movement movement;
            [Serializable]
            public class Movement
            {
                public float maxLookAngle = 80f;
                public Vector2 moveInput; // readonly
                public Vector2 lookInput; // readonly
            }

            public Looking looking;
            [Serializable]
            public class Looking
            {
                public float mouseSensitivity = 1f;
                public float maxLookAngle = 80f;
                public float cameraPitch = 0f;  // readonly
            }

        }

        [Header("MOVEMENT")]
        [SerializeField] private PlayerMovement _playerMovement;
        [Serializable]
        public class PlayerMovement
        {
            public float moveSpeed = 5f;
            public float jumpForce = 5f;
        }

        [Header("REFERENCES")]
        [SerializeField] private PlayerReferences _playerReferences;
        [Serializable]
        public class PlayerReferences
        {
            public Rigidbody rb;
            public Transform cameraTransform;
        }

        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- BASE FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

        private void Awake()
        {
            _playerReferences.rb = GetComponent<Rigidbody>();
        }


        //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- CLASS FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

        public PlayerState State { get => _playerState; set => _playerState = value; }

        // IsGrounded Property (shortcut to toggleables)
        public bool IsGrounded
        {
            get => _playerState._toggleables.isGrounded;
            set => _playerState._toggleables.isGrounded = value;
        }

        // PlayerInput Properties
        public PlayerInput Input { get => _playerInput; set => _playerInput = value; }

        // Movement Properties

        public Transform CameraTransform
        {
            get => _playerReferences.cameraTransform;
            set => _playerReferences.cameraTransform = value;
        }

        public float MaxLookAngle
        {
            get => _playerInput.movement.maxLookAngle;
            set => _playerInput.movement.maxLookAngle = value;
        }

        public Vector2 MoveInput
        {
            get => _playerInput.movement.moveInput;
            set => _playerInput.movement.moveInput = value;
        }

        public Vector2 LookInput
        {
            get => _playerInput.movement.lookInput;
            set => _playerInput.movement.lookInput = value;
        }

        // Looking Properties
        public float MouseSensitivity
        {
            get => _playerInput.looking.mouseSensitivity;
            set => _playerInput.looking.mouseSensitivity = value;
        }

        public float LookingMaxLookAngle
        {
            get => _playerInput.looking.maxLookAngle;
            set => _playerInput.looking.maxLookAngle = value;
        }

        public float CameraPitch
        {
            get => _playerInput.looking.cameraPitch;
            set => _playerInput.looking.cameraPitch = value;
        }

        // PlayerMovement Properties
        public PlayerMovement Movement { get => _playerMovement; set => _playerMovement = value; }

        public float MoveSpeed
        {
            get => _playerMovement.moveSpeed;
            set => _playerMovement.moveSpeed = value;
        }

        public float JumpForce
        {
            get => _playerMovement.jumpForce;
            set => _playerMovement.jumpForce = value;
        }

        // PlayerReferences Properties
        public PlayerReferences References { get => _playerReferences; set => _playerReferences = value; }

        public Rigidbody Rb
        {
            get => _playerReferences.rb;
            set => _playerReferences.rb = value;
        }

    }
}
