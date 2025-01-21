using System;
using Business.Game.PlayerLogic.Animation.Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Business.Game.PlayerLogic.Animation
{
    public class PlayerAnimator : IPlayerAnimator
    {
        private const string IdleTriggerName = "Idle";
        private const string WalkTriggerName = "Walk";
        private const string AttackTriggerName = "Attack";
        private const string DieTriggerName = "Die";
        
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
                ResetAllTriggers();
                ChangeSpeed(speed);
                _animator.SetTrigger(IdleTriggerName);
            }
        }

        public void Walk(float speed)
        {
            if (_photonView.IsMine)
            {
                ResetAllTriggers();
                ChangeSpeed(speed);
                _animator.SetTrigger(WalkTriggerName);
            }
        }

        public void Attack(float speed)
        {
            if (_photonView.IsMine)
            {
                _monitorAttackAnimationEnd = true;
                ResetAllTriggers();
                ChangeSpeed(speed);
                _animator.SetTrigger(AttackTriggerName);
            }
        }

        public void Die(float speed)
        {
            if (_photonView.IsMine)
            {
                ResetAllTriggers();
                ChangeSpeed(speed);
                _animator.SetTrigger(DieTriggerName);
            }
        }
        
        public void MonitorAttackAnimationEnd()
        {
            if (!_monitorAttackAnimationEnd)
                return;
            
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(AttackTriggerName) && stateInfo.normalizedTime >= 1.0f)
            {
                _monitorAttackAnimationEnd = false;
                OnAttackAnimationEnd?.Invoke();
            }
        }

        public void ChangeSpeed(float speed)
        {
            _animator.speed = speed;
        }

        private void ResetAllTriggers()
        {
            _animator.ResetTrigger(IdleTriggerName);
            _animator.ResetTrigger(WalkTriggerName);
            _animator.ResetTrigger(AttackTriggerName);
            _animator.ResetTrigger(DieTriggerName);
        }
    }
}