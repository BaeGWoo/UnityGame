using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseEffect : MonoBehaviour
{
    public  Material redMaterial; // ������ ����
    private Material originalMaterial; // ���� ����
    private  Renderer objectRenderer;
    public  Vector3 curPostiion;
    public bool moveableArea = false;
    public float xSpacing = 2.6f; // x �� ����
    public float zSpacing = 3f;  // z �� ����
    public static GameObject targetblock;
    //public float distance;
    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        curPostiion = transform.position;
    }

    void Update()
    {
        if (Hunter.Moveable)
        {
            StartCoroutine(MoveableArea());
        }

        else
        {
            StartCoroutine(ReturnArea());
        }
        //if (Physics.Raycast(ray, out hit))
        //{
        //    if (hit.transform == transform)
        //    {
        //       
        //            objectRenderer.material = redMaterial; // ���콺�� ������Ʈ ���� ���� �� ������ ���� ����
        //    }
        //    else
        //    {
        //        objectRenderer.material = originalMaterial; // ���� ������ ����
        //    }
        //}
        //else
        //{
        //    objectRenderer.material = originalMaterial; // ���� ������ ����
        //}

    }

    public IEnumerator MoveableArea()
    {
        Debug.Log("Moveable");
        float distance;
        distance = Math.Abs(Hunter.HunterPosition.x - curPostiion.x) + Math.Abs(Hunter.HunterPosition.z - curPostiion.z);

        if (distance <= 4.3f)
        {
            if (this.gameObject != targetblock)
            {
                objectRenderer.material = redMaterial;
                moveableArea = true;

                moveHunterPosition();
            }

        }
        yield return null; // ���� �����ӱ��� ���
    }

    public IEnumerator ReturnArea()
    {
        objectRenderer.material = originalMaterial;
        moveableArea = false;
        yield return null; // ���� �����ӱ��� ���
    }

    public void moveHunterPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.transform.gameObject;
                Debug.Log(clickedObject);
                if (clickedObject.CompareTag("map"))
                {
                    if (!clickedObject.GetComponent<MouseEffect>().moveableArea||clickedObject==targetblock)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                // Ŭ���� ��ġ�� x�� z ��ǥ ����
                Vector3 clickPosition = hit.point;
                float x = clickPosition.x;
                float z = clickPosition.z;

                // Ŭ���� ��ġ�� ���� ����� ĭ�� �ε��� ���
                int xIndex = Mathf.RoundToInt(x / xSpacing);
                int zIndex = Mathf.RoundToInt(z / zSpacing);

                // �� ��ġ ���
                Vector3 newPosition = new Vector3(xIndex * xSpacing, clickedObject.transform.position.y, zIndex * zSpacing);
                float newDistance = Math.Abs(newPosition.x - curPostiion.x) + Math.Abs(newPosition.z - curPostiion.z);
                // ������Ʈ ��ġ �̵�

                Hunter.HunterPosition = newPosition;
                Hunter.Moveable = false;
                targetblock = clickedObject;

            }
        }
    }

    
}
