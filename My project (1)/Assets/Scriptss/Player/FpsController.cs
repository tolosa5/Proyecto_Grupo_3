using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    GameObject cam;
    Transform prota;

    Camera camComp;
    [HideInInspector] public Vector3 movementY;
    float factorG = -9.81f;
    Vector3 direction;

    CharacterController chC;
    Animator anim;
    AnimatorStateInfo animState;
    Rigidbody rb;


    //cosas para movimiento
    [HideInInspector] public float h;
    [HideInInspector] public float v;
    float currentSpeed;
    float standingSpeed = 6;
    float dashCooldown;
    bool isDashing;
    bool isGrappling;

    //Dash
    int dashSpeed = 30;
    bool preparingDash;


    //Gancho
    bool hookAnim;

    GameObject lastWall;
    [HideInInspector] public bool onWall;
    [HideInInspector] public bool onAir;
    Vector3 hookShot;
    [SerializeField] LayerMask isHookeable, isWalkable;
    bool activated;
    bool corniseHooked;
    bool wallHooked;
    Vector3 aimedPoint;

    //Estados del Script
    State state;


    enum State
    {
        Normal,
        Flying,
    }

    void Start()
    {
        cam = Camera.main.gameObject;
        camComp = cam.GetComponent<Camera>();
        chC = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        prota = transform.GetChild(0);

        currentSpeed = standingSpeed;
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                if (!Player.player.busy)
                {
                    Movement();
                    Debug.Log("ey");
                    Debug.Log(v);
                }
                if (!preparingDash)
                {
                    Inputs();
                }

                Jump();
                Dash();
                HookStart();
                
                break;
            
            case State.Flying:
                StartCoroutine(HookAnimation());
                if (!hookAnim)
                {
                    HookMovementWall(); // al final no se usa no da tiempo, pero lo dejo para cuando sigamos el juego en verano
                }
                
                break;
        }


        //Funcionamiento del dash
        if (isDashing) //hace cosas raras
        {
            dashCooldown += Time.deltaTime;
            if (dashCooldown <= 0.5f)
            {
                preparingDash = true;
                
            }
            else if(dashCooldown >= 0.5f && dashCooldown <= 0.6f)
            {
                chC.Move(transform.forward * Mathf.Lerp(dashSpeed, 0, Time.deltaTime * 0.2f) * Time.deltaTime);
                
            }
            else if (dashCooldown >= 1f && dashCooldown <= 2f)
            {
                preparingDash = false;
            }
            else if(dashCooldown > 2f)
            {
                isDashing = false;
                dashCooldown = 0;
            }
        }
    }
    
    void Inputs()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    void Movement()
    {
        direction = transform.forward * v + transform.right * h;
        if (h != 0 || v != 0 )
        {
            anim.SetBool("BoolWalk", true);
        }
        else
        {
            anim.SetBool("BoolWalk", false);
        }

        chC.Move(direction.normalized * currentSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W))
        {
            prota.Rotate(new Vector3(prota.eulerAngles.x, 0, prota.eulerAngles.x), 20 * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            prota.Rotate(new Vector3(prota.eulerAngles.x, -90, prota.eulerAngles.x), 20 * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            prota.eulerAngles = new Vector3(prota.eulerAngles.x, 90, prota.eulerAngles.x);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            prota.eulerAngles = new Vector3(prota.eulerAngles.x, 180, prota.eulerAngles.x);
        }
    }

    void Jump()
    {
        //Translate para la gravedad. Dos deltas porque m/s^2
        movementY.y += factorG * Time.deltaTime;

        chC.Move(movementY * Time.deltaTime);

        if (chC.isGrounded)
        {
            //Saltar
            movementY.y = 0;
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isDashing)
        {
            isDashing = true;
        }
    }
    
    //Ganchos
    void HookStart()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 200, isHookeable))
            {
                if (hit.transform.gameObject.CompareTag("Wall"))
                {
                    hookShot = hit.point;
                    if (corniseHooked)
                    {
                        corniseHooked = false;
                    }

                    wallHooked = true;
                    
                    anim.SetTrigger("HookTrigger");
                    hookAnim = true;

                    state = State.Flying;
                }
            }
        }
    }

    IEnumerator HookAnimation()
    {
        Debug.Log("quietecito mama");
        yield return new WaitUntil(() => animState.IsName("Player@Hook") == false);
        Debug.Log("dale");
        hookAnim = false;

    }

    void HookMovementWall()
    {
        Vector3 hookshotDirection = (hookShot - transform.position).normalized;

        float minSpeed = 40f;
        float maxSpeed = 100f;
        float hookShotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookShot), minSpeed, maxSpeed);
        float hookSpeedMultiplier = 3f;

        chC.Move(hookshotDirection * hookShotSpeed * hookSpeedMultiplier * Time.deltaTime);

        float minimumDistanceToHook = 1.5f;
        if (Vector3.Distance(transform.position, hookShot) < minimumDistanceToHook)
        {
            wallHooked = false;
            state = State.Normal;
            movementY.y = 0f;
        }
    }

    //Se llaman en PlayerScr
    public void GoDinamic()
    {
        isGrappling = true;
        rb.isKinematic = false;
        chC.enabled = false;
    }

    public void GoKinematic()
    {
        isGrappling = false;
        rb.isKinematic = true;
        chC.enabled = true;
    }

    void PauseTime()
    {
        Time.timeScale = 0.1f;
    }
    void ResumeTime()
    {
        Time.timeScale = 1f;
    }
}
