using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MouseEffect : MonoBehaviour
{
    public Material redMaterial; // ������ ����
    private Material originalMaterial; // ���� ����
    private Renderer objectRenderer;
    public Vector3 curPostiion;
    public bool moveableArea = false;
    public float xSpacing = 2; // x �� ����
    public float zSpacing = 2;  // z �� ����
    public static GameObject targetblock;
    public float padding = 0;

    private float speed = 3.0f;
    private enum MoveStage { MovingX, MovingZ, Done }
    private MoveStage currentStage = MoveStage.MovingX;


    [SerializeField] float curdistance;
    //public float distance;
    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        currentStage = MoveStage.Done;

    }

    private void Start()
    {
        curPostiion = transform.position;
        if (CompareTag("BlackTile"))
        {
            padding = 1.8f;
        }
        Debug.Log(tag);
    }

    void Update()
    {
        if (Hunter.Moveable && !Hunter.Running)
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
        float distance;
        distance = Math.Abs(Hunter.HunterPosition.x - (curPostiion.x - padding)) + Math.Abs(Hunter.HunterPosition.z - curPostiion.z);
        curdistance = distance;
        if (distance <= 4)
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
                if (clickedObject.CompareTag("map") || clickedObject.CompareTag("BlackTile"))
                {
                    if (!clickedObject.GetComponent<MouseEffect>().moveableArea || clickedObject == targetblock)
                    {
                        Debug.Log("not Moveable or targetblock");
                        return;
                    }

                }
                else
                {
                    Debug.Log(clickedObject.tag);
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
                float newDistance = Math.Abs(newPosition.x - curPostiion.x - padding) + Math.Abs(newPosition.z - curPostiion.z);
                // ������Ʈ ��ġ �̵�

                StartCoroutine(ReturnArea());
                StartCoroutine(MoveHunter(newPosition));
                //Hunter.HunterPosition = newPosition;
                //Hunter.Moveable = false;
                targetblock = clickedObject;

            }
        }
    }

    private IEnumerator MoveHunter(Vector3 newPosition)
    {
        // X ��ǥ�� �̵�
        yield return StartCoroutine(MoveToX(newPosition.x));

        // Z ��ǥ�� �̵�
        yield return StartCoroutine(MoveToZ(newPosition.z));

        // ��� �̵��� ���� �� ������ ���� �ൿ
        OnReachedDestination();
    }

    private IEnumerator MoveToX(float targetX)
    {
        Hunter.Running = true;
        Vector3 startPosition = Hunter.HunterPosition;
        currentStage = MoveStage.MovingX;
        Vector3 targetPosition = new Vector3(targetX, Hunter.HunterPosition.y, Hunter.HunterPosition.z);
        Hunter.HunterRotation.LookAt(targetPosition);
        Vector3 eulerAngles = Hunter.HunterRotation.eulerAngles;

        // ���� ���� 0~360 ������ ��ȯ�մϴ�.
        float yRotation = Mathf.Round(eulerAngles.y / 90) * 90;

        // ȸ������ �����մϴ�.
        Hunter.HunterRotation.rotation = Quaternion.Euler(eulerAngles.x, yRotation, eulerAngles.z);

        while (Mathf.Abs(Hunter.HunterPosition.x - targetX) > Mathf.Epsilon)
        {
            float step = speed * Time.deltaTime;
            float newX = Mathf.MoveTowards(Hunter.HunterPosition.x, targetX, step);
            Hunter.HunterPosition = new Vector3(newX, Hunter.HunterPosition.y, Hunter.HunterPosition.z);
            yield return null; // ���� �����ӱ��� ���
        }
    }

    private IEnumerator MoveToZ(float targetZ)
    {
        Vector3 startPosition = Hunter.HunterPosition;
        currentStage = MoveStage.MovingZ;
        Vector3 targetPosition = new Vector3(Hunter.HunterPosition.x, Hunter.HunterPosition.y, targetZ);
        Hunter.HunterRotation.LookAt(targetPosition);
        Vector3 eulerAngles = Hunter.HunterRotation.eulerAngles;

        // ���� ���� 0~360 ������ ��ȯ�մϴ�.
        float yRotation = Mathf.Round(eulerAngles.y / 90) * 90;

        // ȸ������ �����մϴ�.
        Hunter.HunterRotation.rotation = Quaternion.Euler(eulerAngles.x, yRotation, eulerAngles.z);

        while (Mathf.Abs(Hunter.HunterPosition.z - targetZ) > Mathf.Epsilon)
        {
            float step = speed * Time.deltaTime;
            float newZ = Mathf.MoveTowards(Hunter.HunterPosition.z, targetZ, step);
            Hunter.HunterPosition = new Vector3(Hunter.HunterPosition.x, Hunter.HunterPosition.y, newZ);
            yield return null; // ���� �����ӱ��� ���
        }
    }

    private void OnReachedDestination()
    {
        currentStage = MoveStage.Done;
        Debug.Log("��ǥ ��ġ�� �����߽��ϴ�!");
        // ���� �ൿ�� ���⿡ �����ϼ���.
        Hunter.Moveable = false;
        Hunter.Running = false;
    }



}
