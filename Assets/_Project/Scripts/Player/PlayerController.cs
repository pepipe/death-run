using System;
using System.Collections;
using pepipe.Utils.Logging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace pepipe.DeathRun.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour {
        public Action Jumping;
        public Action StopJumping;
        public Action Dying;
        public Action<int> Score;

        [SerializeField] float m_WalkModifier = 1f;
        [SerializeField] float m_MoveSpeed = 7f;
        [SerializeField] float m_JumpForce = 7f;
        
        [Header("Debug")] 
        [SerializeField] CustomLogger m_Logger;

        Vector2 _moveDirection;
        Vector2 _rawInput;
        bool _isJumping;
        Rigidbody _rb;
        int _doubleJump = 0;
        bool _isGrounded;
        bool _isFalling;
        Coroutine _checkPlayerPosCoroutine;

        const string GroundLayer = "Ground";
        const string FallLayer = "Fall";
        const string DeathLayer = "Death";

        void Awake() {
            _rb = GetComponent<Rigidbody>();
        }

        void Start() {
            _checkPlayerPosCoroutine = StartCoroutine(CheckPlayerPosition());
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

        IEnumerator CheckPlayerPosition() {
            yield return new WaitForSeconds(.15f);
            var playerPosition = Convert.ToInt32(Math.Floor(transform.position.x));
            Score?.Invoke(playerPosition);
            _checkPlayerPosCoroutine = StartCoroutine(CheckPlayerPosition());
        }

        //Used by the input system
        void OnJump(InputValue value) {
            if (EventSystem.current.IsPointerOverGameObject()) return;//UI Clicks
            if(!_isGrounded && _doubleJump >= 2) return;
            
            Jumping?.Invoke();
            ++_doubleJump;
            _isJumping = true;
        }
    
        void OnCollisionEnter(Collision other) {
            if (!LayerMask.LayerToName(other.gameObject.layer).Equals(GroundLayer)) return;
            
            m_Logger.Log("Ground", this);
            StopJumping?.Invoke();
            _isGrounded = true;
            _doubleJump = 0;
        }

        void OnCollisionExit(Collision other) {
            if (!LayerMask.LayerToName(other.gameObject.layer).Equals(GroundLayer)) return;
            
            m_Logger.Log("Exit Ground", this);
            _isGrounded = false;
        }

        void OnTriggerEnter(Collider other) {
            if (LayerMask.LayerToName(other.gameObject.layer).Equals(FallLayer)) {
                m_Logger.Log("Fall!", this);
                _isFalling = true;
            }
            else if (LayerMask.LayerToName(other.gameObject.layer).Equals(DeathLayer)) {
                m_Logger.Log("Death!", this);
                StopCoroutine(_checkPlayerPosCoroutine);
                _rb.isKinematic = true;
                Dying?.Invoke();
            }
        }
    }
}
