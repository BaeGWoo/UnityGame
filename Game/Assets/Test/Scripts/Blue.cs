using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : MonoBehaviour, Click
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
            CubeManager.curCube = "Blue";
            CubeManager.SeletedCube = gameObject;
            //clicked = true;
        }

        else
        {
            if (temp == "Blue")
            {
                if (clicked)
                {
                    Destroy(gameObject);
                    Destroy(CubeManager.SeletedCube);
                    CubeManager.count--;
                    Debug.Log("Destroy");
                }
                CubeManager.curCube = "";
                
                clicked = false;
                CubeManager.SeletedCube = null;
               
            }

            else
            {
                clicked = false;
                CubeManager.curCube = "";
              
                CubeManager.SeletedCube = null;

            }
        }
        Debug.Log(temp);
    }

}
