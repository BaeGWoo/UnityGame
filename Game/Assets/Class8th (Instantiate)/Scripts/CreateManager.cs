using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    [SerializeField] GameObject cloneObject;

    [SerializeField] GameObject[] inventory;
    [SerializeField] int index;
    [SerializeField] int count;

    void Start()
    {
        index = 0;
        StartCoroutine(Create());
    }

    IEnumerator Create()
    {
        while (true)
        {
            if (index < inventory.Length)
            {
                if (cloneObject == null)
                    cloneObject = Instantiate(inventory[index++]);
                else
                    Debug.Log("object exist");
            }
            else
                Debug.Log("count Overflow");
           
            yield return new WaitForSeconds(2.0f);
            
        }
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(cloneObject);  
        }

        
    }




}
