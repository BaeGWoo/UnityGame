using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public static Vector3 HunterPosition;
    public static bool Moveable = false;
    [SerializeField] AIManager aiManager;
    void Awake()
    {
        
        HunterPosition = new Vector3(0, 0, 0);
    }

    public void Move()
    {
        Moveable = true;
       
    }

    public void Attack()
    {
        Moveable = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = HunterPosition;
    }
}
