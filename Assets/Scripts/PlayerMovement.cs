using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatProject.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : NetworkBehaviour
    {
        #region Editor Fields

        [SerializeField] private float _movementSpeed = 10f;

        #endregion

        #region Private Variables

        private Rigidbody _rb;
        private Animator _anim;

        #endregion

        #region Unity Loops

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _anim = GetComponent<Animator>();
        }

        [ClientCallback]
        private void FixedUpdate()
        {
            if (!hasAuthority) { return; }

            Vector3 moveDirection = new Vector3(PlayerInput.Instance.MoveDirection.x, 0f, PlayerInput.Instance.MoveDirection.y) * _movementSpeed * Time.deltaTime;

            MovePlayer(moveDirection);
            Animate();
        }

        #endregion

        #region Server

        #endregion

        #region Client

        [Client]
        private void MovePlayer(Vector3 moveDirection)
        {
            _rb.AddRelativeForce(moveDirection, ForceMode.VelocityChange);
        }

        private void Animate()
        {
            _anim.SetFloat("InputX", PlayerInput.Instance.MoveDirection.y);
            _anim.SetFloat("InputY", PlayerInput.Instance.MoveDirection.x);
        }

        #endregion
    }
}
