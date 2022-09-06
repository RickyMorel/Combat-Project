using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace CombatProject.Player
{
    public class PlayerInput : MonoBehaviour
    {
        #region Private Variables

        protected static PlayerInput _instance;

        private Vector2 _moveDirection;

        private InputAction _move;

        #endregion

        #region Public Properties

        public static PlayerInput Instance { get { return _instance; } }

        public PlayerInputActions playerControls;

        public Vector2 MoveDirection => _moveDirection;

        #endregion

        #region Unity Loops

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            playerControls = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _move = playerControls.Player.Move;

            _move.Enable();
        }

        private void OnDisable()
        {
            _move.Disable();
        }

        private void Update()
        {
            _moveDirection = _move.ReadValue<Vector2>();
        }

        #endregion
    }
}
