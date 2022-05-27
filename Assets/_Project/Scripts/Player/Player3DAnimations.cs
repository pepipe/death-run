using UnityEngine;

namespace pepipe.DeathRun.Player
{
    [RequireComponent(typeof(Animator))]
    public class Player3DAnimations : MonoBehaviour {
        [SerializeField] Player3DController m_PlayerController;

        static readonly int IsJumping = Animator.StringToHash("IsJumping");
        static readonly int Die = Animator.StringToHash("Die");
        
        Animator _animator;
        static readonly int GoingLeft = Animator.StringToHash("GoingLeft");
        static readonly int GoingRight = Animator.StringToHash("GoingRight");

        void Awake() {
            _animator = GetComponent<Animator>();
        }

        void OnEnable() {
            m_PlayerController.Jumping += OnJump;
            m_PlayerController.StopJumping += OnStopJump;
            m_PlayerController.Dying += OnDie;
            m_PlayerController.GoingLeft += OnMovingLeft;
            m_PlayerController.GoingRight += OnMovingRight;
            m_PlayerController.StopMoving += OnStopMoving;
        }

        void OnDisable() {
            m_PlayerController.Jumping -= OnJump;
            m_PlayerController.StopJumping -= OnStopJump;
            m_PlayerController.Dying -= OnDie;
            m_PlayerController.GoingLeft -= OnMovingLeft;
            m_PlayerController.GoingRight -= OnMovingRight;
            m_PlayerController.StopMoving -= OnStopMoving;
        }

        void OnJump() {
            _animator.SetBool(IsJumping, true);   
        }
        
        void OnStopJump() {
            _animator.SetBool(IsJumping, false);   
        }
        
        void OnDie() {
            _animator.SetTrigger(Die);
        }

        void OnMovingLeft() {
            _animator.SetBool(GoingLeft, true);
        }
        
        void OnMovingRight() {
            _animator.SetBool(GoingRight, true);
        }

        void OnStopMoving() {
            _animator.SetBool(GoingLeft, false);
            _animator.SetBool(GoingRight, false);
        }
    }
}
