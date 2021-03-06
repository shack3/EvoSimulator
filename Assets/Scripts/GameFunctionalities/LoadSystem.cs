using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    public int deathsCount;
    public int birthsCount;
    public void Awake()
    {
        InitiateSimulation(1000);
    }


    public void InitiateSimulation(int numberOfEntities)
    {
        //Cube initiation method
        var origin = GameObject.Find("Earth").transform.position;
        var radius = 20;
        GameObject entityObject = (GameObject)(Resources.Load("Entity"));
        var entityManager = entityObject.GetComponent<EntityManager>();
        
        for (int i = 0; i < numberOfEntities; i++)
        {
            Vector3 area = origin + new Vector3(UnityEngine.Random.Range(-radius / 2, radius / 2),
                UnityEngine.Random.Range(-radius / 2, radius / 2), UnityEngine.Random.Range(-radius / 2, radius / 2));
            
            
            //Entity size in comparation with the bulk
            Vector3 scale = entityObject.transform.localScale;
            entityObject.transform.localScale = new Vector3(entityManager.bulk/25,entityManager.bulk/25 ,entityManager.bulk/25 );
            
            
            Instantiate(entityObject, area, Quaternion.identity);
        }
        
    }
}
