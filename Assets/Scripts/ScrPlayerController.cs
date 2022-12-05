using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayerController : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }

    private Vector2 moveInput;

    [Header("Run")]
    public float runMaxSpeed; //Target speed we want the player to reach.
    public float runAcceleration; //The speed at which our player accelerates to max speed, can be set to runMaxSpeed for instant acceleration down to 0 for none at all
    [HideInInspector] public float runAccelAmount; //The actual force (multiplied with speedDiff) applied to the player.
    public float runDecceleration; //The speed at which our player decelerates from their current speed, can be set to runMaxSpeed for instant deceleration down to 0 for none at all
    [HideInInspector] public float runDeccelAmount; //Actual force (multiplied with speedDiff) applied to the player .

    private void OnValidate()
    {
        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;
        
        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
    }

    private void FixedUpdate()
    {
        Run();
    }
    private void Run()
    {
        Vector2 targetSpeed = new Vector2(moveInput.x, moveInput.y) * runMaxSpeed;
        
        float accelRate = (Mathf.Abs(targetSpeed.y) > 0.01f || Mathf.Abs(targetSpeed.x) > 0.01f) ? runAccelAmount : runDeccelAmount;
        
        Vector2 speedDif = targetSpeed - rb.velocity;
        Vector2 movement = speedDif * accelRate;
        //Debug.Log("speeddif :" + speedDif);

        rb.AddForce(movement * 5);

        if(Mathf.Abs(moveInput.x) < 0.01f && Mathf.Abs(moveInput.y) < 0.01f)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * runDecceleration);
        }
    }
}
