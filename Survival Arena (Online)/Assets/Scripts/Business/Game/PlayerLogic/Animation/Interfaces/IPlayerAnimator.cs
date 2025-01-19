using System;

namespace Business.Game.PlayerLogic.Animation.Interfaces
{
    public interface IPlayerAnimator
    {
        event Action OnAttackAnimationEnd;
        void Idle(float speed);
        void Walk(float speed);
        void Attack(float speed);
        void Die(float speed);
        void MonitorAttackAnimationEnd();
    }
}