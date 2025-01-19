using UnityEngine;

namespace Mono.Game.Character.Animations
{
    public class PlayerAnimationHashHolder
    {
        public int Idle { get; }
        public int Walk { get; }
        public int Attack { get; }

        public PlayerAnimationHashHolder(string idle, string walk, string attack)
        {
            Idle = Animator.StringToHash(idle);
            Walk = Animator.StringToHash(walk);
            Attack = Animator.StringToHash(attack);
        }
    }
}