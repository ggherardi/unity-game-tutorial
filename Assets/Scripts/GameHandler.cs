using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler _instance;
    private Rect _debugRect;
    private static string _debugText = string.Empty;
    public static Player Player { get; private set; }
    public static float HorizontalInput { get; private set; }

    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private LayerMask _wallsLayer;
    public static LayerMask GroundLayer => _instance._groundLayer;
    public static LayerMask WallLayer => _instance._wallsLayer;

    private void Awake()
    {        
        Player = new Player(GameObject.Find("Player"));
        _debugRect = new Rect(10f, 10f, 300f, 200f);
        _instance = this;
    }

    private void OnGUI()
    {
       GUI.TextArea(_debugRect, _debugText);            
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

    public static void WriteDebug(string text, bool appendText = true)
    {
        if (appendText)
        {
            _debugText = $"{text}{Environment.NewLine}{_debugText}";
        }
        else
        {
            _debugText = text;
        }
    }
}
