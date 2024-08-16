using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{

    [SerializeField] GameObject SelectImage;
    public void OnEnter()
    {
       SelectImage.SetActive(true);
        
    }

    public void OnExit()
    {
        SelectImage.SetActive(false);
    }

}
