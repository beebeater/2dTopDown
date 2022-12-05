using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayerController : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D coll;

    private Vector2 moveInput;

    [Header("Run")]
    public float runMaxSpeed; //target speed player cann reach
    public float runAcceleration; //acceleration applied on player. can be set to runMaxSpeed for instant acceleration
    [HideInInspector] public float runAccelAmount; //actual force (multiplied with speedDiff) applied to the player
    public float runDecceleration; //decceleration applied on player, can be set to runMaxSpeed for instant deceleration
    [HideInInspector] public float runDeccelAmount; //actual force (multiplied with speedDiff) applied to the player

    private void OnValidate()
    {
        runAccelAmount = (50 * runAcceleration) / runMaxSpeed; //calculates force that will actually be applied on player
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed; //time*acc/speed

        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed); //so that acceleration/decceleration isnt set to be more than the speed
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();


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
        Vector2 targetSpeed = new Vector2(moveInput.x, moveInput.y) * runMaxSpeed; //stores the direction player is accelerating in

        float accelRate = (Mathf.Abs(targetSpeed.y) > 0.01f || Mathf.Abs(targetSpeed.x) > 0.01f) ? runAccelAmount : runDeccelAmount; //depending on the direction, checks if player needs to be accelerating or deccelerating

        Vector2 speedDif = targetSpeed - rb.velocity; //checks diifference between max speed and current
        Vector2 movement = speedDif * accelRate; 
        //Debug.Log("speeddif :" + speedDif);

        rb.AddForce(movement * 5); //adds force that is acceleration multiplied by difference between the max and currennt speed. the *5 is just to make it feel snappier

        if (Mathf.Abs(moveInput.x) < 0.01f && Mathf.Abs(moveInput.y) < 0.01f)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * runDecceleration); //moves velocity to 0 quickly if no input detected. feels slippery without this
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile")) //checks if player is hit by projectile
        {
            Debug.Log("Player Dead!");
            //Destroy(other.gameObject);
            //Destroy(gameObject);
        }
    }
}
