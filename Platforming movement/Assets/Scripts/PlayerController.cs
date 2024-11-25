using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D GroundedBox;
    public float maxSpeed; 
    public float minSpeed;
    public float speedModifier;
    float movementControl;
    RaycastHit2D hit;
    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //determines what the movement input is, and assigns it to movementControl. 
        movementControl = Input.GetAxis("Horizontal");
        Mathf.Clamp(movementControl, minSpeed, maxSpeed);
        // sets a new vector which the horizontal input float as the x value. 
        Vector2 playerInput = new Vector2(speedModifier * movementControl, 0);
        //calls movementUpdate with the previously defined vector.
        MovementUpdate(playerInput);
       Vector2 groundedSensorEnd = new Vector2(rb.transform.position.x, rb.transform.position.y - 1f);
        Vector2 groundedSensorStart = new Vector2(rb.transform.position.x, rb.transform.position.y - 0.7f);
       hit = Physics2D.Linecast(groundedSensorStart, groundedSensorEnd);
        Debug.DrawLine(groundedSensorStart, groundedSensorEnd);
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
        if (hit) { Debug.Log("hit!"); return true; }
        if (!hit) { Debug.Log("Not Hit!"); return false; }
        else { return false; }
    }

    public FacingDirection GetFacingDirection()
    {
        if (movementControl > 0) { return FacingDirection.right; }
        if (movementControl < 0) {return FacingDirection.left; }
        else { return FacingDirection.left;}
    }
}
