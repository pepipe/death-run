using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace pepipe.DeathRun.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour {
        public Action Jumping;
        public Action Dying;
        
        [SerializeField] float m_WalkModifier = 1f;
        [SerializeField] float m_MoveSpeed = 5f;
        [SerializeField] float m_JumpForce = 5f;
        
        Vector2 _moveDirection;
        Vector2 _rawInput;
        bool _isJumping;
        Rigidbody _rb;
        int _doubleJump = 0;
        bool _isGrounded;
        bool _isFalling;

        const string GroundLayer = "Ground";
        const string FallLayer = "Fall";
        const string DeathLayer = "Death";

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate() {
            if (_isFalling) return;
            
            //Move the player
            _rb.velocity = new Vector2(m_WalkModifier * m_MoveSpeed, _rb.velocity.y);

            if (!_isJumping) return;
            //Make the player jump
            _rb.velocity += new Vector3(0f, m_JumpForce / _doubleJump, 0);
            _isJumping = false;
        }

        //Used by the input system
        void OnJump(InputValue value)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;//UI Clicks
            if(!_isGrounded && _doubleJump >= 2) return;
            
            Jumping?.Invoke();
            ++_doubleJump;
            _isJumping = true;
        }
    
        void OnCollisionEnter(Collision other) {
            if (!LayerMask.LayerToName(other.gameObject.layer).Equals(GroundLayer)) return;
            
            Debug.Log("Ground");
            _isGrounded = true;
            _doubleJump = 0;
        }

        void OnCollisionExit(Collision other) {
            if (!LayerMask.LayerToName(other.gameObject.layer).Equals(GroundLayer)) return;
            
            Debug.Log("Exit Ground");
            _isGrounded = false;
        }

        void OnTriggerEnter(Collider other) {
            if (LayerMask.LayerToName(other.gameObject.layer).Equals(FallLayer)) {
                Debug.Log("Fall!");
                _isFalling = true;
            }
            else if (LayerMask.LayerToName(other.gameObject.layer).Equals(DeathLayer)) {
                Debug.Log("Death!");
                Dying?.Invoke();
            }
        }
    }
}
