using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class EntityManager : MonoBehaviour
{
    private Rigidbody rb;
    private bool iGaveBirth = false;
    private float cicleTime = 30;
    public GameObject baby;

    //All variables related to entities base characteristics
    public int age, maxAge;
    public float energy, bulk;

    public float movementCost;
    public float movementSpeed;


    //Unity Functions

    private void Awake()
    {
        gameObject.GetComponent<gravity>().gravityTarget = GameObject.FindGameObjectWithTag("Earth").transform;
        baby = GameObject.Find("Entity");
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetOlder();
        Mitosis();
        Eat();
        OnDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sun"))
            imOnSun = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sun"))
            imOnSun = false;
    }
    

    private void GetOlder()
    {   //30s
        
        cicleTime -= Time.deltaTime;
        if (cicleTime <= 0)
        {    age++;
            cicleTime = 30;
        }
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

    // <-------------------------------> GENOME <-------------------------------


    #region Genetic Characteristics

    //https://github.com/shack3/EvoSimulator/tree/Documentation#52-genetic-characteristics

    //General-REF_GC_1
    public bool photosynthetic;
    private bool imOnSun;

    public float Aging = 0.05f;
    public float Maximum_Bulk = 1f;
    public float Energy_cost_by_Bulk = 0.01f;
    public float Physical_Hardness = 1f;
    public float Photosynthesis_Efficiency = 1;


    //Nutrition_Related-REF_GC_2
    public bool hardEater;

    public float Ability_to_Eat_Hardness = 0;
    public float Efficiency_to_Digest_Hardness = 0;


    //Reproduction_Related-REF_GC_3
    public int Birth_Cycles = 1;
    public float Reproduction_Efficiency = 1;
    public int Genetic_Sex = 1;
    public int Sexual_Maturity = 4;

    #endregion

    #region Nutrition

    private void Eat()
    {
        if (photosynthetic)
        {
            if (imOnSun) //Direct Proportion c=d*a/b
                energy += Time.deltaTime * Photosynthesis_Efficiency * bulk;
            else //Inverse Proportion c = a*b/d
                energy -= Time.deltaTime * bulk;
        }

        if (hardEater)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region Reproduction

    private void Spores()
    {
        var position = gameObject.transform.position;
        var size = .5f;

        Vector3 area = position + new Vector3(UnityEngine.Random.Range(-size / 2, size / 2),
            UnityEngine.Random.Range(-size / 2, size / 2), UnityEngine.Random.Range(-size / 2, size / 2));
        
        
        if (age > Sexual_Maturity && iGaveBirth == false && UnityEngine.Random.Range(0, 1000) == 7)
        {
            Instantiate(baby, area, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<LoadSystem>().birthsCount++;
            iGaveBirth = !iGaveBirth;
        }
    }

    private void Mitosis()
    {
        GameObject gb = (GameObject)Resources.Load("Entity");
        gb.GetComponent<EntityManager>().photosynthetic = true;
        if (age > Sexual_Maturity && iGaveBirth == false && UnityEngine.Random.Range(0, 1000) == 7)
        {
            Instantiate(gb, transform.position, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<LoadSystem>().birthsCount++;
            iGaveBirth = !iGaveBirth;
        }
    }
    #endregion


    private void OnDeath()
    {
        //Camera parent have to be null if his target is this gameobject.
        GameObject camera = GameObject.Find("Main Camera");


        if (energy < 0 || age > maxAge)//Im dead?!?! :(
        {
            GameObject.Find("GameManager").GetComponent<LoadSystem>().deathsCount++;
            if (camera.transform.parent == gameObject.transform)
            {
                camera.GetComponent<CameraFollow>().findNewTarget=true;
                Destroy(gameObject, 1);
            }
            else
            {
                Destroy(gameObject);
            }
        }
            
    }
}