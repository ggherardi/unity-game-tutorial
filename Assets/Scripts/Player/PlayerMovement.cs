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

    [Header("Coyote Time")]
    [SerializeField] private float _coyoteTime; // How much time the player can hang in the air before jumping
    private float _coyoteCounter; // How much time passed since the player ran off the edge

    [Header("Sound")]
    [SerializeField] private AudioClip _jumpSound;

    [Header("Movement values")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

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
        _playerAnimator.SetBool(Constants.Animations.Player.Run, GameHandler.HorizontalInput != 0);
        _playerAnimator.SetBool(Constants.Animations.Player.Grounded, IsGrounded());

        // Sets velocity. In the tutorial is being set in the else of the If(OnWall())
        //_playerBody.velocity = new Vector2(GameHandler.HorizontalInput * Speed, _playerBody.velocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        // Adjustable jump height. If pressed early it divides a big velocity so the body slows up faster and will fall down faster.
        // If key up happens when the jump is almost done, the velocity will be lower but will still be reduced. In that case the body would have jumped higher anyway because
        // it was pressed later
        if (Input.GetKeyUp(KeyCode.W) && _playerBody.velocity.y > 0)
        {
            _playerBody.velocity = new Vector2(_playerBody.velocity.x, _playerBody.velocity.y / 2);
        }

        if (OnWall())
        {
            _playerBody.gravityScale = 0;
            _playerBody.velocity = Vector2.zero;
        }
        else
        {
            _playerBody.gravityScale = GameHandler.DefaultGravity;
            _playerBody.velocity = new Vector2(GameHandler.HorizontalInput * _speed, _playerBody.velocity.y);
            if (IsGrounded())
            {
                // Reset coyote counter when on the ground
                _coyoteCounter = _coyoteTime; 
            }
            else
            {
                _coyoteCounter -= Time.deltaTime; // Start decreasing coyote counter when not on the ground
            }
        }
    }

    //public void ManageHanging()
    //{
    //    if (OnWall() && !IsGrounded())
    //    {
    //        if (!_playerAnimator.GetBool(Constants.Animations.Player.IsHanging))
    //        {
    //            _playerAnimator.SetBool(Constants.Animations.Player.IsHanging, true);
    //        }
    //        _playerAnimator.SetTrigger(Constants.Animations.Player.StartHangingTrigger);
    //        _playerBody.gravityScale = 0;
    //        _playerBody.velocity = Vector2.zero;
    //    }
    //    else
    //    {
    //        if (_playerAnimator.GetBool(Constants.Animations.Player.IsHanging))
    //        {
    //            _playerAnimator.SetBool(Constants.Animations.Player.IsHanging, false);
    //        }
    //    }
    //}

    private void Jump()
    {
        // Can't jump If not on wall and coyote counter has expired
        if(_coyoteTime > 0 && _coyoteCounter <= 0 && !OnWall()) return;

        if (OnWall())
        {
            WallJump();
        }
        else
        {
            if (IsGrounded() || _coyoteCounter > 0)
            {
                // Actual jump
                _playerBody.velocity = new Vector2(_playerBody.velocity.x, _jumpForce);
            }

            // Reset coyote counter to 0 to avoid double jump
            _coyoteCounter = 0;
        }        
    }

    private void WallJump()
    {

    }

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
