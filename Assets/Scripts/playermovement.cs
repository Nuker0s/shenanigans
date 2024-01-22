using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class playermovement : MonoBehaviour
{
    public Camera cam;
    public float grounddrag=1;
    public float groundstopdrag=5;
    public float skydrag=0.5f;
    public float speed=1300;
    public float flyspeed=500;
    public float groundspeedlimit=13;
    public float jumpforce=500;
    public int jumps;
    public float fallspeed = 0.5f;
    public float maxadditionalfallspeed = 5;
    public int maxjumps = 2;
    public float sense = 20;

    [Header("inputs")]
    public PlayerInput pinput;
    public InputAction move;
    public InputAction look;
    public InputAction jump;
    public InputAction mousemodebutt;
    [Header("vault")]
    public float vaultspeed = 0;
    public Transform va1;
    public Transform va2;
    [Header("rb and groundcheck")]
    public Rigidbody rb;
    public bool jumpsched = false;
    public Transform groundchecker;
    public LayerMask ground;
    public float groundcheckrange = 0.3f;
    public bool grounded = true;
    [Header("camera")]
    public Vector2 camclamp;
    public bool mousemode = false;

    void Awake()
    {
        move = pinput.actions.FindAction("Move");
        look = pinput.actions.FindAction("Look");
        jump = pinput.actions.FindAction("Jump");
        mousemodebutt = pinput.actions.FindAction("Mousemode");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mousemodebutt.WasPressedThisFrame())
        {
            if (mousemode)
            {
                mousemode = false;
            }
            else
            {
                mousemode = true;
            }
            if (mousemode)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (!mousemode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (grounded)
            {
                rb.drag = grounddrag;
                if (!move.IsPressed())
                {
                    rb.drag = groundstopdrag;
                }
            }
            else
            {
                rb.drag = skydrag;
            }
            if (isgrounded() && !grounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            if (isgrounded())
            {
                grounded = true;
                jumps = maxjumps;
            }
            else
            {
                grounded = false;
            }
            if (jump.WasPressedThisFrame())
            {
                jumpsched = true;
            }
            /*if (grounded)
            {
                rb.drag = 5;
            }
            else
            {
                rb.drag = 1f;
            }*/
            lookero();
        }


    }
    private void FixedUpdate()
    {
        if (!mousemode)
        {
            movemento();
            jumperro();
        }

        if (!grounded && rb.velocity.y < maxadditionalfallspeed)
        {
            rb.velocity += new Vector3(0, -fallspeed, 0);
        }
        limitvelocity();
    }
    public void limitvelocity()
    {
        if (rb.velocity.magnitude>groundspeedlimit & grounded)
        {
            Vector3 limitspeed = rb.velocity.normalized;
            limitspeed *= groundspeedlimit;
            rb.velocity = new Vector3(limitspeed.x, rb.velocity.y, limitspeed.z);
        }
    }
    public void movemento()
    {
        Vector2 minput = move.ReadValue<Vector2>();
        Vector3 moveforce = (minput.y * transform.forward + minput.x * transform.right);
        if (grounded)
        {
            rb.AddForce(moveforce * Time.deltaTime * speed);
        }
        else
        {
            rb.AddForce(moveforce * Time.deltaTime * flyspeed);
        }
        if (!Physics.CheckSphere(va1.position, 0.2f, ground)&Physics.CheckSphere(va2.position, 0.2f, ground))
        {
            rb.velocity =new Vector3(rb.velocity.x, vaultspeed*Time.deltaTime, rb.velocity.z);
        }

        
    }

    private float totalRotation = 0.0f;

    public void lookero()
    {
        Vector2 limput = look.ReadValue<Vector2>() * sense * Time.deltaTime;
        transform.Rotate(0, limput.x, 0);

        // Add the input to the total rotation
        totalRotation += limput.y * -1;

        // Clamp the total rotation to be within a certain range
        totalRotation = Mathf.Clamp(totalRotation, -80, 80);

        // Apply the rotation to the camera
        cam.transform.localEulerAngles = new Vector3(totalRotation, 0, 0);
    }

    public void jumperro()
    {
        if (jumpsched)
        {
            if ((isgrounded() || jumps > 0))
            {
                jumps -= 1;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(0, jumpforce, 0);
                jumpsched = false;
            }
            else
            {
                jumpsched=false;
            }

        }
    }
    bool isgrounded()
    {
        return Physics.CheckSphere(groundchecker.position, groundcheckrange,ground);
    }
}
