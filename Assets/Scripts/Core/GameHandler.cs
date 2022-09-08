using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallsLayer;
    private static GameHandler _instance;
    private static Text _debugText;
    public static PlayerReference Player { get; private set; }
    public static CameraReference Camera { get; private set; }
    public static float HorizontalInput { get; private set; }
    public static bool HasRightClicked { get; private set; }    
    public static LayerMask GroundLayer => _instance._groundLayer;
    public static LayerMask WallLayer => _instance._wallsLayer;    

    private void Awake()
    {        
        Player = new PlayerReference(GameObject.Find("Player"));
        Camera = new CameraReference(GameObject.Find("Main Camera"));
        _debugText = GameObject.Find("DebugScreen").GetComponent<Text>();
        _instance = this;
    }

    private void OnGUI()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        HorizontalInput = Input.GetAxis("Horizontal");
        HasRightClicked = Input.GetMouseButton(0);
    }

    public static void WriteDebug(string text, bool appendText = true)
    {
        if (appendText)
        {
            _debugText.text = $"{text}{Environment.NewLine}{_debugText.text}";
        }
        else
        {
            _debugText.text = text;
        }
    }
}
