using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CubeManager : MonoBehaviour
{
    [SerializeField] GameObject[] cube;
    [SerializeField] GameObject text;
    [SerializeField] GameObject gameoverText;
    [SerializeField] GameObject gameoverPlane;
    [SerializeField] GameObject SuccesPlane;
    [SerializeField] GameObject SuccesText;
    [SerializeField] string cur;
    public static string curCube;
    public static GameObject SeletedCube;
    [SerializeField] float time = 60;
    public static int count;

    void Awake()
    {
        int[] position=new int[cube.Length];
        int[] pairposition=new int[cube.Length];
        curCube = "";
        count = cube.Length;
        cur = curCube;
      
        for (int i = 0; i < cube.Length; i++)
        {
            int cubePosition = Random.Range(0, cube.Length);
            int pairPosition = Random.Range(0, cube.Length);

            while (position[cubePosition] == -1)
            {
                cubePosition = Random.Range(0, cube.Length);
              
            }

            position[cubePosition] = -1;

            while (pairposition[pairPosition] == -1)
            {
                pairPosition = Random.Range(0, cube.Length);
            }

            pairposition[pairPosition] = -1;
            cube[i] = Instantiate(cube[i]);
            cube[i].transform.position = new Vector3(cubePosition * 2, 0, 0);

            cube[i] = Instantiate(cube[i]);
            cube[i].transform.position = new Vector3(pairPosition * 2, 4, 0);
        }


    }

    // Update is called once per frame
   public string GetCubeName()
    {
        return curCube;
    }

    public void Update()
    {
        text.GetComponent<Text>().text = "" + (int)time;
        //time -= Time.deltaTime;
        cur = curCube;

        if (time < 0)
        {
            gameoverText.SetActive(true);
            gameoverPlane.SetActive(true);
            //Application.Quit();
        }
        else
        {
            if (count <= 0)
            {
                SuccesPlane.SetActive(true);
                SuccesText.SetActive(true);
            }

            else
                time -= Time.deltaTime;
        }
    }
}
