using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseEffect : MonoBehaviour
{
    public  Material redMaterial; // 빨간색 재질
    private Material originalMaterial; // 원래 재질
    private  Renderer objectRenderer;
    public  Vector3 curPostiion;
    public bool moveableArea = false;
    public float xSpacing = 2.6f; // x 축 간격
    public float zSpacing = 3f;  // z 축 간격
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
        //            objectRenderer.material = redMaterial; // 마우스가 오브젝트 위에 있을 때 빨간색 재질 적용
        //    }
        //    else
        //    {
        //        objectRenderer.material = originalMaterial; // 원래 재질로 복원
        //    }
        //}
        //else
        //{
        //    objectRenderer.material = originalMaterial; // 원래 재질로 복원
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
        yield return null; // 다음 프레임까지 대기
    }

    public IEnumerator ReturnArea()
    {
        objectRenderer.material = originalMaterial;
        moveableArea = false;
        yield return null; // 다음 프레임까지 대기
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
                // 클릭한 위치의 x와 z 좌표 추출
                Vector3 clickPosition = hit.point;
                float x = clickPosition.x;
                float z = clickPosition.z;

                // 클릭한 위치에 가장 가까운 칸의 인덱스 계산
                int xIndex = Mathf.RoundToInt(x / xSpacing);
                int zIndex = Mathf.RoundToInt(z / zSpacing);

                // 새 위치 계산
                Vector3 newPosition = new Vector3(xIndex * xSpacing, clickedObject.transform.position.y, zIndex * zSpacing);
                float newDistance = Math.Abs(newPosition.x - curPostiion.x) + Math.Abs(newPosition.z - curPostiion.z);
                // 오브젝트 위치 이동

                Hunter.HunterPosition = newPosition;
                Hunter.Moveable = false;
                targetblock = clickedObject;

            }
        }
    }

    
}
