using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.VersionControl.Asset;


public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    float xvel, yvel, zvel;
    public Animator anim;

    InputAction moveAction;
    InputAction jumpAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveValue = moveAction.ReadValue<Vector3>();

        if(jumpAction.IsPressed())
        {
            
        }

        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;
        zvel = rb.linearVelocity.z;
    }
}
