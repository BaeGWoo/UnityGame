using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{

    [SerializeField] Transform[] spawners;
    [SerializeField] GameObject cloud;

    void Start()
    {
       InvokeRepeating("ResetPosition", 5, 5);
    }


    public void ResetPosition()
    {
      
        int random = Random.Range(0, spawners.Length);
       
        cloud.transform.position = spawners[random].position;
        
    }
}
