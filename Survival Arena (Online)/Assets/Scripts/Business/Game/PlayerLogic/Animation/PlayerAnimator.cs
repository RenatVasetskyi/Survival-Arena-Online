using System;
using Business.Game.PlayerLogic.Animation.Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Business.Game.PlayerLogic.Animation
{
    public class PlayerAnimator : IPlayerAnimator
    {
        private const string WalkAnimationName = "IsWalking";
        private const string AttackAnimationName = "Attack";
        private const string DieAnimationName = "Die";
        
        private readonly Animator _animator;
        private readonly PhotonView _photonView;
        
        public event Action OnAttackAnimationEnd;

        private bool _monitorAttackAnimationEnd;

        public PlayerAnimator(Animator animator, PhotonView photonView)
        {
            _animator = animator;
            _photonView = photonView;
            _animator = animator;
        }

        public void Idle(float speed)
        {
            if (_photonView.IsMine)
            {
                _animator.speed = speed;
                _animator.SetBool(WalkAnimationName, false);
            }
        }

        public void Walk(float speed)
        {
            if (_photonView.IsMine)
            {
                _animator.speed = speed;
                _animator.SetBool(WalkAnimationName, true);
            }
        }

        public void Attack(float speed)
        {
            if (_photonView.IsMine)
            {
                _monitorAttackAnimationEnd = true;
                _animator.speed = speed;
                _animator.SetTrigger(AttackAnimationName);
            }
        }

        public void Die(float speed)
        {
            if (_photonView.IsMine)
            {
                _animator.speed = speed;
                _animator.SetTrigger(DieAnimationName);
            }
        }
        
        public void MonitorAttackAnimationEnd()
        {
            if (!_monitorAttackAnimationEnd)
                return;
            
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(AttackAnimationName) && stateInfo.normalizedTime >= 1.0f)
            {
                _monitorAttackAnimationEnd = false;
                OnAttackAnimationEnd?.Invoke();
            }
        }
    }
}