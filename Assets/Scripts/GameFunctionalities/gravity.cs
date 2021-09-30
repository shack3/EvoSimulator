using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{

    public Transform gravityTarget;
    public Material material;

    public static float power = 15000f;
    public static float torque = 500f;
    public static float gravedad = 9.81f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ProcessGravity();
    }

    private void Update()
    {
        if (transform.childCount == 0)
            GetComponent<MeshRenderer>().material = material;
    }


    public int ProcessGravity()
    {
        Vector3 diff = transform.position - gravityTarget.position; //Direccion
        rb.AddForce(- diff.normalized * gravedad * (rb.mass)); // fuerza atractora
        //Debug.DrawRay(transform.position, diff.normalized, Color.green);
        return 0;
    }


}
