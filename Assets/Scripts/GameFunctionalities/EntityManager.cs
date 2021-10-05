using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;


public class EntityManager : MonoBehaviour
{
    private Rigidbody rb;
    private float cicleTime = 30;
    public GameObject baby;
    public Genome myGenome;

    //All variables related to entities base characteristics
    public int age, maxAge;
    public float energy, bulk;

    public float movementCost;
    public float movementSpeed;
    private bool imOnSun;


    //Unity Functions

    private void Awake()
    {
        gameObject.GetComponent<gravity>().gravityTarget = GameObject.FindGameObjectWithTag("Earth").transform;
        baby = GameObject.Find("Entity");
        rb = GetComponent<Rigidbody>();
        myGenome = new Genome();
    }

    private void Update()
    {
        GetOlder();
        Mitosis();
        Eat();
    }

  
    

    private void GetOlder()
    {   //cycle times 30s
        if (DeadByOld())
        {
            OnDeath();
        }
        else
        {
            EnergyConsumption();
            if (energy > 0)
            {
                age += myGenome.Aging;
            }
            else
            {
                OnDeath();
            }
        }
    }

    public float EnergyConsumption()
    {
        energy -= Time.deltaTime * bulk;
        return energy;
    }
    
    
    private Boolean DeadByOld()
    {
        cicleTime -= Time.deltaTime;
        if (cicleTime <= 0)
        {    
            int dead_or_not = UnityEngine.Random.Range(0, 1000);
            if (dead_or_not < age)
            {
                return true;
            }
            cicleTime = cicleTime;
        }

        return false;
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

    #region Nutrition

    private void Eat()
    {
        if (myGenome.photosynthetic)
        {
            if (imOnSun) //Direct Proportion c=d*a/b
                energy += Time.deltaTime * myGenome.Photosynthesis_Efficiency * bulk;
        }

        if (myGenome.hardEater)
        {
            throw new NotImplementedException();
        }
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

    #endregion

    #region Reproduction
/*
    private void Spores()
    {
        var position = gameObject.transform.position;
        var size = .5f;

        Vector3 area = position + new Vector3(UnityEngine.Random.Range(-size / 2, size / 2),
            UnityEngine.Random.Range(-size / 2, size / 2), UnityEngine.Random.Range(-size / 2, size / 2));
        
        GameObject gb = (GameObject)Resources.Load("Entity");
        if (age > Sexual_Maturity && iGaveBirth == false && UnityEngine.Random.Range(0, 1000) == 7)
        {
            InstantiateRandom(gb, area, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<LoadSystem>().birthsCount++;
            iGaveBirth = !iGaveBirth;
        }
    }
*/
    private void Mitosis()
    {
        GameObject gb = (GameObject)Resources.Load("Entity");
        gb.GetComponent<EntityManager>().myGenome.photosynthetic = true;
        if (age > myGenome.Sexual_Maturity && energy > myGenome.Reproduction_Efficiency)
        {
            NewBirth(gb, transform.position, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<LoadSystem>().birthsCount++;
            OnDeath();
        }
    }

    private void NewBirth(GameObject gameObject, Vector3 position, Quaternion quaternion)
    {
        GameObject entityObject = (GameObject)(Resources.Load("Entity"));
        var BirthEntityManager = entityObject.GetComponent<EntityManager>();
        
        BirthEntityManager.age = 0;
        BirthEntityManager.bulk = UnityEngine.Random.Range(0.5f,3f);
        BirthEntityManager.energy = UnityEngine.Random.Range(50,300);
        BirthEntityManager.myGenome.photosynthetic = true;
        BirthEntityManager.myGenome.Photosynthesis_Efficiency = UnityEngine.Random.Range(1f,2f);;
        BirthEntityManager.myGenome.Sexual_Maturity = 4;//UnityEngine.Random.Range();

        Instantiate(gameObject, position, quaternion);

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