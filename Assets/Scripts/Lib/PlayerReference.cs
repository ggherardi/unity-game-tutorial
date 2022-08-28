using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerReference
    {
        public GameObject GameObject { get; private set; }
        public PlayerMovement PlayerMovementHandler { get; private set; }

        public PlayerReference(GameObject gameObject) 
        {
            GameObject = gameObject;
            PlayerMovementHandler = gameObject.GetComponent("PlayerHandler") as PlayerMovement;
        }
    }
}
