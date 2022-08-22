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
        public GameObject GameObject { get; private set; }
        public PlayerMovement PlayerMovementHandler { get; private set; }

        public Player(GameObject gameObject) 
        {
            GameObject = gameObject;
            PlayerMovementHandler = gameObject.GetComponent("PlayerHandler") as PlayerMovement;
        }
    }
}
