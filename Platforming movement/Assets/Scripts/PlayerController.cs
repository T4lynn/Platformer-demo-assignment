using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D GroundedBox;
    public float maxSpeed; 
    public float minSpeed;
    public float speedModifier;
    public float JumpMultiplier;
    float movementControl;
    RaycastHit2D hit;
   public LayerMask groundLayer;
    bool isgrounded;
    float maxJumpHeight = 5;
    float maxJumpTime = 1;
    float terminalSpeed = 6;
    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.NameToLayer("Ground");
        rb.gravityScale = 2 * maxJumpHeight/(maxJumpTime * maxJumpTime);
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(rb.velocity.y, -terminalSpeed, terminalSpeed);
        //determines what the movement input is, and assigns it to movementControl. 
        movementControl = Input.GetAxis("Horizontal");
        Mathf.Clamp(movementControl, minSpeed, maxSpeed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space Pressed");
            MovementUpdate(new Vector2(0, 1 * JumpMultiplier));
        }
        // sets a new vector which the horizontal input float as the x value. 
        Vector2 playerInput = new Vector2(speedModifier * movementControl, 0);
        //calls movementUpdate with the previously defined vector.
        MovementUpdate(playerInput);
        hit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y -1), new Vector2(1, 1), 0, -transform.up);
       
      if (hit)
        {
            if (hit.transform.gameObject.layer == groundLayer)
            {
                isgrounded = true;
            } else { isgrounded = false; }
        }

    }

    private void MovementUpdate(Vector2 playerInput)
    {
        // wrote a debug.log when troubleshooting, to determine whether the
        //problem was addforce or input.getaxis. 
       // Debug.Log("direction:" +  playerInput);
        //adds a force in the direction indicated by movement control, using a constant force. 
           rb.AddForce(playerInput, ForceMode2D.Force);
    }

    public bool IsWalking()
    {
        if (movementControl == 0)
        {
            return false;
        } else { return true; }
    }
    public bool IsGrounded()
    {
        if (isgrounded) { Debug.Log("hit!"); return true; }
        if (!isgrounded) { Debug.Log("Not Hit!"); return false; }
        else { return false; }
    }

    public FacingDirection GetFacingDirection()
    {
        if (movementControl > 0) { return FacingDirection.right; }
        if (movementControl < 0) {return FacingDirection.left; }
        else { return FacingDirection.left;}
    }
}
