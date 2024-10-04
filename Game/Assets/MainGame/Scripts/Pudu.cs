using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Pudu : Animal
{
    private Vector3[] movePoint = new Vector3[3];
    private Vector3[] moveDirection = new Vector3[2];
    private Animator animator;
    [SerializeField] GameObject AttackBox;
    public AIManager aimanager;
    public float jumpHeight = 2f; // 점프 높이
    public float jumpDuration = 1f; // 점프 애니메이션의 지속 시간
    private void Awake()
    {
        animator = GetComponent<Animator>();

        Vector3 target = Hunter.HunterPosition;

        moveDirection[0] = new Vector3(transform.position.x, 0, target.z);
        moveDirection[1] = new Vector3(target.x, 0, transform.position.z);
    }

    public override void Move(Vector3[] movePoints, Vector3[] moveDirections, Animator animator)
    {
        movePoints = movePoint;
        moveDirections = moveDirection;
        animator = this.animator;
        // 개 특유의 이동 방식 구현
        base.Move(movePoints, moveDirections, animator); // 기본 동작 호출
    }

    public void Move()
    {
        Vector3 curPosition = new Vector3(transform.position.x, 0.2f, transform.position.z);
        AIManager.TileMap[(int)(curPosition.x / 2), (int)(curPosition.z / 2)] = 0;
        Vector3 target = Hunter.HunterPosition;

        float distance = 100;
        int minDirection = 0;



        // 현재 위치에서 이동할 수 있는 위치 설정하기
        movePoint[0]= curPosition;
        movePoint[1] = new Vector3(target.x, 0, curPosition.z);
        movePoint[2] = new Vector3(curPosition.x,0,target.z);


        // 이동할 위치 중 target과 가장 인접한 위치 찾기
        for (int i = 0; i < movePoint.Length; i++)
        {
            float temp;
            temp = Mathf.Abs((movePoint[i].x - target.x) + (movePoint[i].z - target.z));

            if (movePoint[i].x >= -0.01f && movePoint[i].x <= 14 && movePoint[i].z >= -0.01f && movePoint[i].z <= 14)
            {
                if (AIManager.TileMap[(int)(movePoint[i].x / 2), (int)(movePoint[i].z / 2)] != 1)
                {
                    if (temp < distance)
                    {
                        distance = temp;
                        minDirection = i;
                    }
                }
            }
        }
        AIManager.TileMap[(int)(movePoint[minDirection].x / 2), (int)(movePoint[minDirection].z) / 2] = 1;
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

        transform.LookAt(Hunter.HunterPosition);
        Vector3 eulerAngles = transform.eulerAngles;

        // 각도 값을 0~360 범위로 변환합니다.
        float yRotation = Mathf.Round(eulerAngles.y / 90) * 90;

        // 회전값을 설정합니다.
        transform.rotation = Quaternion.Euler(eulerAngles.x, yRotation, eulerAngles.z);

        if (transform.position == Hunter.HunterPosition)
        {
            StartCoroutine(ActiveAttackBox());
        }
    }


    public void Attack()
    {
        
    }

    private IEnumerator ActiveAttackBox()
    {
        animator.SetTrigger("Jump");

        // GameObject를 활성화합니다.
        AttackBox.SetActive(true);

        // 지정한 시간 동안 대기합니다.
        yield return new WaitForSeconds(1.0f);

        // GameObject를 비활성화합니다.
        AttackBox.SetActive(false);
    }
}
