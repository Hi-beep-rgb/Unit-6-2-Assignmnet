using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    float xvel, yvel, zvel;
    public Animator anim;

    public Transform cam;
    float turnSmoothVelocity;
    float turnSmoothTime = 0.1f;
    float speed = 6;
    bool isJumping;
    bool isGrounded;

    InputAction moveAction;
    InputAction jumpAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJumping = false;
        isGrounded = false;

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        PlayerJump();

        /*
        Vector2 moveValue = moveAction.ReadValue<Vector2>();


        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;
        zvel = rb.linearVelocity.z;


        print("jump=" + jumpAction.IsPressed());
        print("move xy=" + moveValue.x + "  " + moveValue.y);




        if(jumpAction.IsPressed())
        {
            yvel = 5;
        }


        rb.linearVelocity = new Vector3( xvel, yvel, zvel);
        */

    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        // If NO movement input → instantly stop all horizontal momentum
        if (direction.magnitude < 0.1f)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            //anim.SetFloat("Speed", 0);
            anim.SetBool("isRunning", false);
            return;
        }

        anim.SetBool("isRunning", true);

        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);

        //anim.SetFloat("Speed", 1);
    }

    void PlayerJump()
    {
        //if the y velocity is not zero, then play the jump animation

        if (jumpAction.IsPressed() && isGrounded == true /*isJumping==false*/)
        {
            isGrounded = false;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 20, rb.linearVelocity.z);
            isJumping = true;
            anim.SetBool("isJumping", true);

        }

        /*//check for the player landing on the ground
        if ( rb.linearVelocity.y <-1 )
        {
            //check for oncollision
            isGrounded = false;
            anim.SetBool("isJumping", false);
            isJumping = false;
        }
        else
        {
            isGrounded = true;
        }*/




    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
