using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Move))]

public class StoneUnit : MonoBehaviour
{
    [SerializeField] private Move move;

    private void Awake()
    {
        move = GetComponent<Move>();
    }


    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Forward");
            move.OnMove(Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Back");
            move.OnMove(Vector3.back);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Right");
            move.OnMove(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("left");
            move.OnMove(Vector3.left);
        }
    }
}
