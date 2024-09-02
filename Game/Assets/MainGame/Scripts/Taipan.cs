using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taipan : MonoBehaviour, AnimalInterface
{
    private Vector3[] movePoint = new Vector3[4];
    private Vector3[] moveDirection = new Vector3[4];
    private Animator animator;

    public AIManager aimanager;
    public float jumpHeight = 2f; // 점프 높이
    public float jumpDuration = 1f; // 점프 애니메이션의 지속 시간
    private void Awake()
    {
        moveDirection[0] = new Vector3(5.2f, 0, 6f);
        moveDirection[1] = new Vector3(-5.2f, 0, 6f);
        moveDirection[2] = new Vector3(-5.2f, 0, -6f);
        moveDirection[3] = new Vector3(5.2f, 0, -6f);

        animator = GetComponent<Animator>();
    }

    public void Move()
    {
        AIManager.TileMap[(int)(transform.position.x / 2.6f), (int)(transform.position.z / 3)] = 0;
        Vector3 target = Hunter.HunterPosition;
        Vector3 curPosition = new Vector3(transform.position.x, 0, transform.position.z);

        float distance = 100;
        int minDirection = 0;

        // 현재 위치에서 이동할 수 있는 위치 설정하기
        for (int i = 0; i < movePoint.Length; i++)
        {
            movePoint[i] = new Vector3(moveDirection[i].x + transform.position.x, 0, moveDirection[i].z + transform.position.z);
            Debug.Log(movePoint[i]);
        }


        // 이동할 위치 중 target과 가장 인접한 위치 찾기
        for (int i = 0; i < movePoint.Length; i++)
        {
            float temp;
            temp = Mathf.Abs((movePoint[i].x - target.x) + (movePoint[i].z - target.z));

            if (movePoint[i].x >= -0.01f && movePoint[i].x <= 18.2 && movePoint[i].z >= -0.01f && movePoint[i].z <= 21)
            {
                if (AIManager.TileMap[(int)(movePoint[i].x / 2.6f), (int)(movePoint[i].z / 3)] != 1)
                {
                    if (temp < distance)
                    {
                        distance = temp;
                        minDirection = i;
                    }
                }
            }
        }
        AIManager.TileMap[(int)(movePoint[minDirection].x / 2.6f), (int)(movePoint[minDirection].z) / 3] = 1;
        StartCoroutine(JumpToPosition(new Vector3(movePoint[minDirection].x, 0, movePoint[minDirection].z), jumpDuration, jumpHeight));
    }

    public IEnumerator JumpToPosition(Vector3 targetPosition, float jumpDuration, float jumpHeight)
    {
        animator.SetTrigger("Jump");
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            float t = elapsedTime / jumpDuration;
            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight; // 점프 곡선

            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t);
            transform.position = new Vector3(currentPosition.x, startPosition.y + height, currentPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        transform.position = new Vector3(targetPosition.x, startPosition.y, targetPosition.z);
    }


    public void Attack()
    {

    }
}
