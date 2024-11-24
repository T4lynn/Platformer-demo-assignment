using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
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
        float movementControl = Input.GetAxis("Horizontal");
        // sets a new vector which the horizontal input float as the x value. 
        Vector2 playerInput = new Vector2(movementControl, 0);
        //calls movementUpdate with the previously defined vector.
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        // wrote a debug.log when troubleshooting, to determine whether the
        //problem was addforce or input.getaxis. 
        //Debug.Log("direction:" +  playerInput);
        //adds a force in the direction indicated by movement control, using a constant force. 
        rb.AddForce(playerInput, ForceMode2D.Force);
    }

    public bool IsWalking()
    {
        return false;
    }
    public bool IsGrounded()
    {
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        return FacingDirection.left;
    }
}
