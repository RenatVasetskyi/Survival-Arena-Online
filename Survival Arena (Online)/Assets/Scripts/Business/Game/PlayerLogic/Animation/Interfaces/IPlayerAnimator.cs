namespace Business.Game.PlayerLogic.Animation.Interfaces
{
    public interface IPlayerAnimator
    {
        void Idle(float speed);
        void Walk(float speed);
        void Attack(float speed);
        void Die(float speed);
    }
}