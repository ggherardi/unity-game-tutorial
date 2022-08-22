using System;
using System.Collections.Generic;
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

        public struct Animations
        {
            public const string Run = "run";
            public const string Grounded = "grounded";
            public const string JumpTrigger = "jumpTrigger";
            public const string AttackTrigger = "attackTrigger";
        }        
    }
}
