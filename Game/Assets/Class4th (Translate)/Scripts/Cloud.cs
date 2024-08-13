using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float speed = 5.0f;
  
   
    void Start()
    {
        
    }

    private void Update()
    {
        //transform.position = transform.position + direction * speed * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }



}
