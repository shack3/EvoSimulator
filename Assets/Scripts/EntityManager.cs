using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;

delegate float HardnessEater();

public class EntityManager : MonoBehaviour
{
    private Rigidbody rb;

    public int age;
    public float energy, bulk, movementEfficiency, movementSpeed;
    
    
    
    
    private void Awake()
    {
        gameObject.GetComponent<gravity>().gravityTarget = GameObject.FindGameObjectWithTag("Earth").transform;
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        var sun = GameObject.FindWithTag("Sun");
        energy = Eat(Photosintetic, HardnessEater, sun);
        Debug.Log(energy);
    }

    

    #region Movement
    
    
    float horizontal;
    float vertical;
    
    //Public methods for moving axis
    // W1 D1 A-1 S-1
    public void MoveForward()
    {
        horizontal += 0;
        vertical += 1;
    }

    public void MoveBackward()
    {
        horizontal += 0;
        vertical += -1;
    }

    public void MoveLeft()
    {
        horizontal += -1;
        vertical += 0;
    }

    public void MoveRight()
    {
        horizontal += 1;
        vertical += 0;
    }

    public float MoveStatus()
    {
        return horizontal * 10 + vertical;
    }
    
    public void MoveReset()
    {
        horizontal = 0;
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

    #region Eating

    
    
    private float Eat(System.Func<Transform, float> photosintetic, HardnessEater hardnessEater, GameObject gameObject)
    {
        var sun = GameObject.FindWithTag("Sun");
        if(gameObject.CompareTag("Sun")) //<- IM PHOTOSINTETIC
            return photosintetic(sun.transform);
        else
            return hardnessEater();
    }
    
    private float Photosintetic(Transform sunTransform)
    {
        return DistanceBetween(gameObject.transform, sunTransform) * 0.1f; //Multiply var needed in genome.
    }
    

    private float HardnessEater()
    {
        return 20;
    }

    private static float DistanceBetween(Transform a, Transform b) //<-E01_01_01 - distance_to_sun method renamed.
    {
        return Vector3.Distance(a.position, b.position);
    }


    #endregion
}
