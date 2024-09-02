using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public static Vector3 HunterPosition;
    [SerializeField] AIManager aiManager;
    void Awake()
    {
        
        HunterPosition = new Vector3(0, 0, 0);
    }

    public void Move()
    {
        Debug.Log("Move");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
