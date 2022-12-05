using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrTurretController : MonoBehaviour
{
    public Transform target;
    public float rotSpeed;
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion targetRot = Quaternion.FromToRotation(Vector3.right, direction);
        transform.rotation = Quaternion.RotateTowards(transform.localRotation, targetRot, Time.deltaTime * rotSpeed);
    }

}
