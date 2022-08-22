using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerBody;
    private Animator _playerAnimator;
    private Collider2D _playerCollider;
    private float _wallJumpCooldown;

    #region Serialized fields
    public float Speed;
    public float JumpForce;
    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        _playerBody = GetComponent<Rigidbody2D>();       
        _playerAnimator = GetComponent<Animator>();
        _playerCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        // Flips character based on direction
        if (GameHandler.HorizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        } 
        else if (GameHandler.HorizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Sets run animation
        _playerAnimator.SetBool(Constants.Animations.Run, GameHandler.HorizontalInput != 0);
        _playerAnimator.SetBool(Constants.Animations.Grounded, IsGrounded());

        if(_wallJumpCooldown > 0.2f)
        {
            _playerBody.velocity = new Vector2(GameHandler.HorizontalInput * Speed, _playerBody.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                _playerBody.gravityScale = 0;
                _playerBody.velocity = Vector2.zero;
            }
            else
            {
                _playerBody.gravityScale = 7;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }            
        }
        else
        {
            _wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _playerAnimator.SetTrigger(Constants.Animations.JumpTrigger);
            _playerBody.velocity = new Vector2(_playerBody.velocity.x, JumpForce);
        }       
        else if (OnWall())
        {
            if (GameHandler.HorizontalInput == 0)
            {
                _playerBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                _playerBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            _wallJumpCooldown = 0;            
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //}

    private bool IsGrounded()
    {        
        // Uses the player box collider with a vector that points down
        RaycastHit2D rayCastHit = Physics2D.BoxCast(_playerCollider.bounds.center, _playerCollider.bounds.size, 0, Vector2.down, 0.1f, GameHandler.GroundLayer);
        return rayCastHit.collider != null;
    }

    private bool OnWall()
    {
        // Uses the player box collider with a vector that points to the direction of the movement
        Vector2 movementDirection = new Vector2(transform.localScale.x, 0);
        RaycastHit2D rayCastHit = Physics2D.BoxCast(_playerCollider.bounds.center, _playerCollider.bounds.size, 0, movementDirection, 0.1f, GameHandler.WallLayer);
        return rayCastHit.collider != null;
    }

    public bool CanAttack()
    {
        return GameHandler.HorizontalInput == 0 && IsGrounded() && !OnWall();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawCube(_playerCollider.bounds.center, _playerCollider.bounds.size);
    }
}
