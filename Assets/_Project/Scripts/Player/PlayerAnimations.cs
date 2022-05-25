using UnityEngine;

namespace pepipe.DeathRun.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour {
        [SerializeField] PlayerController m_PlayerController;

        static readonly int IsJumping = Animator.StringToHash("IsJumping");
        static readonly int Die = Animator.StringToHash("Die");
        
        Animator _animator;

        void Awake() {
            _animator = GetComponent<Animator>();
        }

        void OnEnable() {
            m_PlayerController.Jumping += OnJump;
            m_PlayerController.StopJumping += OnStopJump;
            m_PlayerController.Dying += OnDie;
        }

        void OnDisable() {
            m_PlayerController.Jumping -= OnJump;
            m_PlayerController.Dying -= OnDie;
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
    }
}
