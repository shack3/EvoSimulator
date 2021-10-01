using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Random = System.Random;

public class CameraFollow : MonoBehaviour
{
    public Transform entity, planet;
    public Material selected;
    public Volume volume;
    
    public int posicion, size;
    public GameObject[] targets; 

    private float effectTime = 1f;
    public float interpolationRatio= 0.123f;


    private bool activateChR = false;

    private void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Earth").transform;
        posicion = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Entity")
                {
                    entity = hit.transform;
                    activateChR = true;
                    
                }
            }
        }
        transform.SetParent(entity);
        
        if (transform.parent == entity)
            entity.GetComponent<MeshRenderer>().material = selected;
        

        transform.position = Vector3.Lerp(transform.position, -(planet.position - entity.position) * (2-entity.GetComponent<Transform>().localScale.x ), interpolationRatio);
        transform.LookAt(planet);


        
        if (activateChR)
            ChromaticEffect();

    }

    private void ChromaticEffect()
    {
        ChromaticAberration chrA;
        
        if (volume.profile.TryGet<ChromaticAberration>(out chrA))
        {
            if(effectTime>0)
            {
                //Debug.Log(effectTime);
                effectTime -= Time.deltaTime;
                chrA.intensity.value = effectTime/2;
            }
            else
            {
                chrA.intensity.value = 0f;
                effectTime = 1f;
                activateChR = false;
            }
        }
    }

    public void FindOtherTarget()
    {
        targets = GameObject.FindGameObjectsWithTag("Entity");
        size = targets.Length;
        if (posicion < size)
        {
            entity = targets[posicion++].transform;
            posicion++;
        }
        else
        {
            entity = targets[0].transform;
            posicion = 0;
        }
            
    }

}
