using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player
    {
        public static GameObject GameObject { get; private set; }
        public static PlayerHandler ScriptHandler { get; private set; }

        public Player(GameObject gameObject) 
        {
            GameObject = gameObject;
            ScriptHandler = gameObject.GetComponent("PlayerHandler") as PlayerHandler;
        }
    }
}
