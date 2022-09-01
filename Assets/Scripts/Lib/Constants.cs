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
        }

        public struct Animations
        {
            public struct Player
            {
                public const string Run = "run";
                public const string Grounded = "grounded";
                public const string JumpTrigger = "jumpTrigger";
                public const string AttackTrigger = "attackTrigger";
                public const string HurtTrigger = "hurtTrigger";
                public const string DeathTrigger = "deathTrigger";
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
