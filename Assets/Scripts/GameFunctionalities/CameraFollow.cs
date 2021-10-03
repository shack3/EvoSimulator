using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Transform entity, planet;
    public Material selected;
    public Volume volume;
    
    public int posicion, size;
    public GameObject[] targets;

    private Text inspectorText, countText;
    
    private float effectTime = 1f;
    public float interpolationRatio= 0.123f;

    public bool findNewTarget = true;
    private bool activateChR = false;

    private void Awake()
    {
        GUIStyle style = new GUIStyle ();
        style.richText = true;
    }

    private void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Earth").transform;
        inspectorText = GameObject.Find("InspectorText").GetComponent<Text>();
        countText = GameObject.Find("CountText").GetComponent<Text>();
        posicion = 0;
    }

    private void Update()
    {
        FindOtherTarget();
        
        
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


        inspectorText.text = "Age = " + entity.GetComponent<EntityManager>().age + "/" +entity.GetComponent<EntityManager>().maxAge 
                             + "\nEnergy = "         + entity.GetComponent<EntityManager>().energy
                             + "\nBulk = "           + entity.GetComponent<EntityManager>().bulk
                             + "\nPhotosynthetic = " + entity.GetComponent<EntityManager>().photosynthetic;

        countText.text = "<color=red>Deaths = " + GameObject.Find("GameManager").GetComponent<LoadSystem>().deathsCount + "</color>"
                        + "\n<color=green>Births = " + GameObject.Find("GameManager").GetComponent<LoadSystem>().birthsCount + "</color>";
        
        if (activateChR)
            ChromaticEffect();
        
        targets = GameObject.FindGameObjectsWithTag("Entity");

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
        
        size = targets.Length;
        if (findNewTarget == true)
        {
            activateChR = true;
            if (posicion + 1 >= size)
            {
                entity = targets[0].transform;
                posicion = 0;
            }
            else
            {
                posicion += 1;
                entity = targets[posicion].transform;
            }
            
            findNewTarget = false;
        }
    }

}
