using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraReference
    {
        public GameObject GameObject { get; private set; }
        public RoomCameraHandler CameraHandler { get; private set; }

        public CameraReference(GameObject gameObject)
        {
            GameObject = gameObject;
            CameraHandler = gameObject.GetComponent("CameraHandler") as RoomCameraHandler;
        }
    }
}
