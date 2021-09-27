using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<gravity>().gravityTarget = GameObject.FindGameObjectWithTag("Earth").transform;
    }

    
    void Update()
    {
        
    }


    #region Movement

    //Public methods for moving
    public void MoveForward()
    {

    }

    public void MoveBackward()
    {

    }

    public void MoveLeft()
    {

    }

    public void MoveRight()
    {

    }

    #endregion
}
