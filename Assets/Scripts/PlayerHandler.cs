using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    public float Speed;
    private bool _grounded;
    private Rigidbody2D _playerBody;
    private SpriteRenderer _playerSprite;
    private Animator _playerAnimator;
    private Animation _playerAnimation;

    // Start is called before the first frame update
    private void Awake()
    {
        _playerBody = GetComponent<Rigidbody2D>();       
        _playerSprite = GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponent<Animator>();
        _playerAnimation = GetComponent<Animation>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        _playerBody.velocity = new Vector2(GameHandler.HorizontalInput * Speed, _playerBody.velocity.y);        

        // Sets run animation
        _playerAnimator.SetBool(Constants.Animations.Run, GameHandler.HorizontalInput != 0);
        _playerAnimator.SetBool(Constants.Animations.Grounded, _grounded);

        // Flips character based on direction
        if (GameHandler.HorizontalInput > 0)
        {
            transform.localScale = Vector3.one;
        } 
        else if (GameHandler.HorizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.W) && _grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _grounded = false;
        _playerAnimator.SetTrigger(Constants.Animations.JumpTrigger);
        _playerBody.velocity = new Vector2(_playerBody.velocity.x, Speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Constants.GameObjects.Platform)
        {
            _grounded = true;
        }
    }
}
