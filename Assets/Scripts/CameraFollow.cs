using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class CameraFollow : MonoBehaviour
{
    public Transform entity, planet;
    public Material selected;
    public Volume volume;


    private float effectTime = 1f;
    public float interpolationRatio= 0.123f;


    public bool activateChR = false;

    private void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Earth").transform;
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
        

        transform.position = Vector3.Lerp(transform.position, -(planet.position - entity.position) * 2, interpolationRatio);
        transform.LookAt(planet);


        
        if (activateChR)
            chromaticEffect();

    }
    
    public void chromaticEffect()
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
   

}
