using System;
using System.Collections;
using pepipe.Utils.Logging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace pepipe.DeathRun.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player3DController : MonoBehaviour, IController {
        public Action Jumping { get; set; }
        public Action StopJumping { get; set; }
        public Action Dying { get; set; }
        public Action<int> Score { get; set; }
        public Action GoingLeft { get; set; }
        public Action GoingRight { get; set; }
        public Action StopMoving { get; set; }

        [SerializeField] float m_WalkModifier = 1f;
        [SerializeField] float m_MoveSpeed = 7f;
        [SerializeField] float m_JumpForce = 7f;
        
        [Header("Debug")] 
        [SerializeField] CustomLogger m_Logger;

        Vector2 _moveDirection;
        Vector2 _rawInput;
        bool _isJumping;
        bool _isMovingLeft;
        bool _isMovingRight;
        Rigidbody _rb;
        int _doubleJump;
        bool _isGrounded;
        bool _isFalling;
        Coroutine _checkPlayerPosCoroutine;
        bool _pressedUI;
        Vector3 _targetPosition;
        float _initialPositionX;

        const string GroundLayer = "Ground";
        const string FallLayer = "Fall";
        const string DeathLayer = "Death";

        void Awake() {
            _rb = GetComponent<Rigidbody>();
        }

        void Start() {
            _checkPlayerPosCoroutine = StartCoroutine(CheckPlayerPosition());
        }

        void Update()
        {
            _pressedUI = EventSystem.current.IsPointerOverGameObject();
            
            if (_isMovingLeft) {
                var step = m_MoveSpeed * Time.deltaTime;
                _targetPosition = new Vector3(_initialPositionX - 2, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
            }else if (_isMovingRight) {
                var step = m_MoveSpeed * Time.deltaTime;
                _targetPosition = new Vector3(_initialPositionX + 2, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
            }

            if (Vector3.Distance(transform.position, _targetPosition) > .001f) return;
            
            _isMovingLeft = _isMovingRight = false;
            StopMoving?.Invoke();
        }

        void FixedUpdate() {
            if (_isFalling) return;
            
            //Move the player
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, m_WalkModifier * m_MoveSpeed);

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
            if(!_isGrounded && _doubleJump >= 2) return;
            if (_pressedUI) return;
            
            Jumping?.Invoke();
            ++_doubleJump;
            _isJumping = true;
        }
        
        void OnLeft(InputValue value) {
            if(!_isGrounded || _isMovingLeft || _isMovingRight) return;

            m_Logger.Log("Moving Left", this);
            GoingLeft?.Invoke();
            _initialPositionX = transform.position.x;
            _isMovingLeft = true;
        }
        
        void OnRight(InputValue value) {
            if(!_isGrounded || _isMovingLeft || _isMovingRight) return;
            
            GoingRight?.Invoke();
            _initialPositionX = transform.position.x;
            _isMovingRight = true;
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
