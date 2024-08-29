using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : MonoBehaviour, Click
{
    [SerializeField] CubeManager cubeManager;
    [SerializeField] bool clicked = false;

    private void Awake()
    {
        cubeManager = GetComponent<CubeManager>();
    }
    public void Interact()
    {

        string temp = CubeManager.curCube;
        clicked = clicked == true ? false : true;
        if (temp == "")
        {
            CubeManager.curCube = "Green";
            CubeManager.SeletedCube = gameObject;
        }

        else
        {
            if (temp == "Green")
            {
                if (clicked)
                {
                    Destroy(gameObject);
                    Destroy(CubeManager.SeletedCube);
                    CubeManager.count--;
                    Debug.Log("Destroy");
                }
                CubeManager.curCube = "";
                
                CubeManager.SeletedCube = null;
               
            }

            else
            {
                CubeManager.curCube = "";
               
                CubeManager.SeletedCube = null;
                
            }
        }
        Debug.Log(temp);
    }

}
