using Photon.Pun;
using UnityEngine;

namespace Business.Game.PlayerLogic.Animations
{
    public class PlayerAnimator : MonoBehaviourPun
    {
        private const string WalkAnimationName = "IsWalking";
        private const string AttackAnimationName = "Attack";
        private const string DieAnimationName = "Die";
        
        [SerializeField] private Animator _animator;

        public void Idle(float speed)
        {
            if (photonView.IsMine)
            {
                _animator.speed = speed;
                _animator.SetBool(WalkAnimationName, false);
            }
        }

        public void Walk(float speed)
        {
            if (photonView.IsMine)
            {
                _animator.speed = speed;
                _animator.SetBool(WalkAnimationName, true);
            }
        }

        public void Attack(float speed)
        {
            if (photonView.IsMine)
            {
                _animator.speed = speed;
                _animator.SetTrigger(AttackAnimationName);
            }
        }

        public void Die(float speed)
        {
            if (photonView.IsMine)
            {
                _animator.speed = speed;
                _animator.SetTrigger(DieAnimationName);
            }
        }
    }
}