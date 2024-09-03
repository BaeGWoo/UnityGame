using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Pudu : MonoBehaviour, AnimalInterface
{
    private Vector3[] movePoint = new Vector3[2];
    private Animator animator;
    [SerializeField] GameObject AttackBox;
    public AIManager aimanager;
    public float jumpHeight = 2f; // ���� ����
    public float jumpDuration = 1f; // ���� �ִϸ��̼��� ���� �ð�
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move()
    {
        Vector3 curPosition = new Vector3(transform.position.x, 0, transform.position.z);
        AIManager.TileMap[(int)(curPosition.x / 2.6f), (int)(curPosition.z / 3)] = 0;
        Vector3 target = Hunter.HunterPosition;

        float distance = 100;
        int minDirection = 0;



        // ���� ��ġ���� �̵��� �� �ִ� ��ġ �����ϱ�
        movePoint[0] = new Vector3(target.x, 0, curPosition.z);
        movePoint[1] = new Vector3(curPosition.x,0,target.z);


        // �̵��� ��ġ �� target�� ���� ������ ��ġ ã��
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
            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight; // ���� �

            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t);
            transform.position = new Vector3(currentPosition.x, startPosition.y + height, currentPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }

        transform.position = new Vector3(targetPosition.x, startPosition.y, targetPosition.z);

        transform.LookAt(Hunter.HunterPosition);
        Vector3 eulerAngles = transform.eulerAngles;

        // ���� ���� 0~360 ������ ��ȯ�մϴ�.
        float yRotation = Mathf.Round(eulerAngles.y / 90) * 90;

        // ȸ������ �����մϴ�.
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

        // GameObject�� Ȱ��ȭ�մϴ�.
        AttackBox.SetActive(true);

        // ������ �ð� ���� ����մϴ�.
        yield return new WaitForSeconds(1.0f);

        // GameObject�� ��Ȱ��ȭ�մϴ�.
        AttackBox.SetActive(false);
    }
}
