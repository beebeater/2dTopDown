using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrTurretController : MonoBehaviour
{
    public Transform target;
    public float rotSpeed;

    public float range;
    [SerializeField] LayerMask playerLayer;
    bool playerCheck;

    [SerializeField] SpriteRenderer sprite;

    [Header("Projectile")]
    [SerializeField] Transform projectileSpawn;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private float minShootWaitTime = 0.1f, maxShootWaitTime = 0.2f;

    Rigidbody2D bulletRB;
    public Vector2 projectileSpeed;
    private float waitTime;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        waitTime = Time.time + Random.Range(minShootWaitTime, maxShootWaitTime); //choses a random wait time betweenn two consecutive bullets
    }

    // Update is called once per frame
    void Update()
    {
        playerCheck = Physics2D.OverlapCircle(transform.position, range, playerLayer); //draws a circle around the axis of the turret of radius range. if player overlaps with this boolean is true.

        if (playerCheck)
        {
            Vector3 direction = target.position - transform.position; //gets the offset from players starting position to the turrets
            direction.z = 0;
            Quaternion targetRot = Quaternion.FromToRotation(Vector3.right, direction); //creates a rotation where the x axis points towards direction
            transform.rotation = Quaternion.RotateTowards(transform.localRotation, targetRot, Time.deltaTime * rotSpeed); //rotation of turret points towards players location which moves according to rotSpeed 
        }
        
        if (transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z < 180)
        {
            sprite.sortingOrder = 1;
        }
        else
        { 
            sprite.sortingOrder = 3; 
        }
    }
    private void FixedUpdate()
    {
        if (Time.time > waitTime)
        {
            if (playerCheck)
            {
                waitTime = Time.time + Random.Range(minShootWaitTime, maxShootWaitTime);
                ShootingProjectile();
            }
        }
    }

    void ShootingProjectile()
    {
        //GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation); 
        GameObject projectile = ScrProjectilePool.instance.GetPooledProjectile();
        if(projectile != null)
        {
            projectile.transform.position = projectileSpawn.position;
            projectile.transform.rotation = projectileSpawn.rotation;
            projectile.SetActive(true);
        }
        bulletRB = projectile.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(projectileSpawn.rotation * projectileSpeed, ForceMode2D.Impulse); //pushes projectile towards its x rotation
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range); //draws range
    }
}
