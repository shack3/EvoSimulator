using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;


public class EntityManager : MonoBehaviour
{
    private Rigidbody rb;
    public float cicleTime = 30;
    public GameObject baby;
    public GenomeManager MyGenomeManager;
    
    //All variables related to entities base characteristics
    public float energy, bulk,age;
    public float NeuralResult;
    public float movementCost;
    public float movementSpeed;
    public bool imOnSun;
    public float[][] NeuralNetworkBias = new float[100][];

    public float[] ActivityNeurons = new float[6];

    public int ActivitySelected;
    /*
     * 0 - MoveReset(StandStill) (Dont do anything)
     * 1 - MoveForward
     * 2 - MoveBackward
     * 3 - MoveLeft
     * 4 - MoveRight
     * 5 - ReproductionActivity
     */
    //Unity Functions
    public delegate void OnDeathAux();
    public delegate Boolean DeathByOldAux(float dTime, int random);

    private void Awake()
    {
        gameObject.GetComponent<gravity>().gravityTarget = GameObject.FindGameObjectWithTag("Earth").transform;
        baby = GameObject.Find("Entity");
        rb = GetComponent<Rigidbody>();
        MyGenomeManager = new GenomeManager();
        InitNeuralBias();
    }

    private void Update()
    {
        GetOlder(OnDeath,DeadByOld,Time.deltaTime,UnityEngine.Random.Range(0, 1000));
        Mitosis();
        Eat(Time.deltaTime);
    }
    
    public void GetOlder(OnDeathAux ODA,DeathByOldAux DBOA,float dTime,int random)// 1 test
    {   //cycle times 30s
        //if (DBOA(Time.deltaTime,UnityEngine.Random.Range(0, 1000)))
        if (DBOA(dTime,random))
        {
            ODA();
        }
        else
        {
            EnergyConsumption(Time.deltaTime);
            if (energy > 0)
            {
                age += MyGenomeManager.Aging;
            }
            else
            {
                ODA();
            }
        }
    }

    public float EnergyConsumption(float dTime)// 1 test
    {
        //energy -= Time.deltaTime * bulk;
        energy -= dTime * bulk;
        return energy;
    }
    
    
    public Boolean DeadByOld(float dTime,int random)// 1 test
    {
        cicleTime -= dTime;
        if (cicleTime <= 0)
        {   
            //int dead_or_not = UnityEngine.Random.Range(0, 1000);
            if (random < age)
            {
                return true;
            }
        }
        return false;
    }
    
    #region Movement

    float horizontal;
    float vertical;

    //Public methods for moving axis
    // W1 D1 A-1 S-1
    public void MoveForward()// 1 test
    {
        horizontal += 0;
        vertical += 1;
    }

    public void MoveBackward()// 1 test
    {
        horizontal += 0;
        vertical += -1;
    }

    public void MoveLeft()// 1 test
    {
        horizontal += -1;
        vertical += 0;
    }

    public void MoveRight()// 1 test
    {
        horizontal += 1;
        vertical += 0;
    }

    public float MoveStatus()// 1 test
    {
        return horizontal * 10 + vertical;
    }

    public void MoveReset()// 1 test
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

    public void Eat(float dTime)// 1 test
    {
        if (imOnSun) //Direct Proportion c=d*a/b
            energy += dTime * MyGenomeManager.PhotosynthesisEfficiency * bulk;
        //Time.deltaTime * MyGenomeManager.PhotosynthesisEfficiency * bulk;
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
        if (age > MyGenomeManager.SexualMaturity && energy > MyGenomeManager.ReproductionEfficiency)
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
        BirthEntityManager.MyGenomeManager.Genome = MyGenomeManager.GenerateNewGenome(MyGenomeManager.Genome,MyGenomeManager.Genome);

        Instantiate(gameObject, position, quaternion);

    }
    
    
    #endregion

    
    private void OnDeath()
    {
        //Camera parent have to be null if his target is this gameobject.
        GameObject camera = GameObject.Find("Main Camera");
        GameObject.Find("GameManager").GetComponent<LoadSystem>().deathsCount++;
        
        if (camera.transform.parent == gameObject.transform)
        {
            camera.GetComponent<CameraFollow>().findNewTarget=true;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    #region Neural
    public void NewNeuralNetworkResult()
    {
        /*
         * Getting the inputs in the first layer
         */
        float[] NeuralResults = new float[100];
        float[] NewNeuralResults = new float[100];
        
        int ActivityToBeDone = 0;
        float result;

        float InputParameters = 0;
        for (int pos = 0; pos < MyGenomeManager.NeuralNetwork.Length; pos++)
        {
            InputParameters = Convert.ToSingle(imOnSun ? 1 : 0);
            InputParameters *= NeuralNetworkBias[0][pos];
            InputParameters *= energy;
            InputParameters *= bulk;
            NeuralResults[pos] += Math.Abs((float)Math.Sin(InputParameters* NeuralNetworkBias[0][pos]));
        }
        
        //Getting the results of the next 
        for (int pos = 1; pos < MyGenomeManager.NeuralNetwork.Length; pos++)
        {
            for (int pos2 = 0; pos2 < MyGenomeManager.NeuralNetwork.Length; pos2++)
            {
                NeuralResult = 0f;
                for (int pos3 = 0; pos3 < MyGenomeManager.NeuralNetwork.Length; pos3++)
                {
                    if (MyGenomeManager.NeuralNetwork[pos][pos2][pos3] >  0f)
                    {
                        //NeuralResults[pos2] = NeuralResults[pos3] * NeuralNetworkBias[pos][pos2];
                        NeuralResult += NeuralResults[pos3];
                    }
                }

                NewNeuralResults[pos2] +=  Math.Abs((float) Math.Sin(NeuralResult * NeuralNetworkBias[pos][pos2]));
            }

            NeuralResults = NewNeuralResults;
        }

        //Getting the results of the last layer
        NeuralResult = 0;
        for (int pos = 0; pos < NeuralResults.Length; pos++)
        {
            NeuralResult += NeuralResults[pos];
        }

        result = 0;
        for (int pos = 0; pos < ActivityNeurons.Length; pos++)
        {
            if (NeuralResult * ActivityNeurons[pos] > result)
            {
                result = NeuralResult * ActivityNeurons[pos];
                ActivitySelected = pos;
            }
        }
    }
    
    public void InitNeuralBias()// 1 test
    {
        for (int pos = 0; pos < NeuralNetworkBias.Length; pos++)
        {
            NeuralNetworkBias[pos] = new float[NeuralNetworkBias.Length];
            for (int pos2 = 0; pos2 < NeuralNetworkBias[pos].Length; pos2++)
                NeuralNetworkBias[pos][pos2] = UnityEngine.Random.Range(-1f,1f);
        }

        for (int pos = 0; pos < ActivityNeurons.Length; pos++)
        {
            ActivityNeurons[pos] = UnityEngine.Random.Range(-1f,1f);
        }
    }
    
    #endregion
}