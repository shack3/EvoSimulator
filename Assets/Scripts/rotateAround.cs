using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public Vector3 angle;

    void  Update()
    {
        transform.RotateAround(target.transform.position, angle, speed * Time.deltaTime);
    }
}
