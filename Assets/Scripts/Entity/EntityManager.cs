using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;


delegate float HardnessEater();

public class EntityManager : MonoBehaviour
{
    private Rigidbody rb;
    
    #region REF_BC_1
    
    public int age;
    public float energy, bulk;
    
    public float movementCost;
    public float movementSpeed;   
            
    #endregion
    
    
    //Unity Functions
    
    private void Awake()
    {
        gameObject.GetComponent<gravity>().gravityTarget = GameObject.FindGameObjectWithTag("Earth").transform;
        rb = GetComponent<Rigidbody>();
        age = 1;
        energy = 100;
        bulk = 1;
    }

    private void Update()
    {
        Eat();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sun")
            imOnSun = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sun")
            imOnSun = false;
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
    
    /*private void Assign_Movement_Cost()
    {
        movementCost = 1 / (Genome.Maximum_Bulk - bulk);
    }

    private void Assign_movementSpeed()
    {
        movementSpeed = energy / age;
    }
    */

    #region <-------------------------------> GENOME <-------------------------------

    //https://github.com/shack3/EvoSimulator/tree/Documentation#52-genetic-characteristics

    
    
    
    #region General-REF_GC_1

    public bool photosynthetic;
    public bool imOnSun;
    
    public float Aging = 0.05f;
    public float Maximum_Bulk = 1f;
    public float Energy_cost_by_Bulk = 0.01f;
    public float Physical_Hardness = 1f;
    public float Photosynthesis_Efficiency = 1;
    
    #endregion
    
    #region Nutrition_Related-REF_GC_2

    public bool hardEater;
    
    public float Ability_to_Eat_Hardness = 0;
    public float Efficiency_to_Digest_Hardness = 0;
    
    
    #endregion
    
    #region Reproduction_Related-REF_GC_3

    public int Birth_Cycles = 1;
    public float Reproduction_Efficiency = 1;
    public int Genetic_Sex = 1;
    public int Sexual_Maturity = 4;
        
    #endregion




    #region Eat
    public void Eat()
    {
        if (photosynthetic)
        {
            if (imOnSun)    //Direct Proportion c=d*a/b
                energy += Time.deltaTime;
            else                    //Inverse Proportion c = a*b/d
                energy -= Time.deltaTime * bulk;
        }

        if (hardEater)
        {
            
        }
    }
    

    #endregion
    
    
    
    #endregion

}
