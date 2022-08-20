using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static Player Player { get; private set; }
    public static float HorizontalInput { get; private set; }

    private void Awake()
    {        
        Player = new Player(GameObject.Find("Player"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
    }
}
