using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal class Constants
    {
        public struct GameObjects
        {
            public const string Player = "Player";
            public const string Platform = "Platform";
        }

        public struct Tags
        {
            public const string Player = "Player";
            public const string PlayerFireball = "PlayerFireball";
            public const string Enemy = "Enemy";
        }

        public struct Directions
        {
            public const int Right = 1;
            public const int Left = -1;
        }

        public struct Animations
        {
            public struct Generics
            {
                public const string MeleeAttack = "meleeAttack";
                public const string DeathTrigger = "deathTrigger";
                public const string HurtTrigger = "hurtTrigger";
            }

            public struct Player
            {
                public const string Run = "run";
                public const string Grounded = "grounded";
                public const string IsHanging = "isHanging";
                public const string JumpTrigger = "jumpTrigger";
                public const string Fire = "fireboltTrigger";
                public const string StartHangingTrigger = "startHangingTrigger";
            }            

            public struct Bandit
            {
                public const string IsRunning = "isRunning";
                public const string MeleeAttack = "meleeAttack";
                public const string RangedAttack = "rangedAttack";
                public const string Hurt = "hurt";
                public const string Death = "death";
            }

            public struct Fireball
            {
                public const string ExplodeTrigger = "explodeTrigger";
            }

            public struct Firetrap
            {
                public const string IsHit = "isHit";
                public const string Active = "active";
            }
        }        

        public struct Layers
        {
            public const int Player = 7;
            public const int Enemy = 8;
        }
    }
}
