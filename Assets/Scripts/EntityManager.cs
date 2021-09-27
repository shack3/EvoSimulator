using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed;
    float horizontal;
    float vertical;
    private void Awake()
    {
        gameObject.GetComponent<gravity>().gravityTarget = GameObject.FindGameObjectWithTag("Earth").transform;
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
    }

    

    #region Movement

    //Public methods for moving axis
    // W1 D1 A-1 S-1
    public void MoveForward()
    {
        horizontal = 0;
        vertical = 1;
    }

    public void MoveBackward()
    {
        horizontal = 0;
        vertical = -1;
    }

    public void MoveLeft()
    {
        horizontal = -1;
        vertical = 0;
    }

    public void MoveRight()
    {
        horizontal = 1;
        vertical = 0;
    }

    private void FixedUpdate()
    {
        horizontal *= movementSpeed * Time.deltaTime;
        vertical *= movementSpeed * Time.deltaTime;

        Vector3 origin = Vector3.zero;

        Quaternion hq = Quaternion.AngleAxis(-horizontal, Vector3.up);
        Quaternion vq = Quaternion.AngleAxis(vertical, Vector3.right);

        Quaternion q = hq * vq;

        rb.MovePosition(q * (rb.transform.position - origin) + origin);
        transform.LookAt(Vector3.zero);

    }

    #endregion
}
