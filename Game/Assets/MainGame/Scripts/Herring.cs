                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herring : Animal
{
    private Vector3[] movePoint = new Vector3[9];
    private Vector3[] moveDirection = new Vector3[8];
    private Animator animator;
    [SerializeField] GameObject AttackBox;
    public AIManager aimanager;
    public float jumpHeight = 2f; // ���� ����
    public float jumpDuration = 1f; // ���� �ִϸ��̼��� ���� �ð�
    private void Awake()
    {
        moveDirection[0] = new Vector3(2, 0, 2);
        moveDirection[1] = new Vector3(-2, 0, 2);
        moveDirection[2] = new Vector3(-2, 0, -2);
        moveDirection[3] = new Vector3(2, 0, -2);
        moveDirection[4] = new Vector3(0, 0, -2);
        moveDirection[5] = new Vector3(0, 0, 2);
        moveDirection[6] = new Vector3(2, 0, 0);
        moveDirection[7] = new Vector3(-2, 0, 0);


        movePoint[0] = transform.position;
        // ���� ��ġ���� �̵��� �� �ִ� ��ġ �����ϱ�
        for (int i = 1; i < movePoint.Length; i++)
        {
            movePoint[i] = new Vector3(moveDirection[i - 1].x + transform.position.x, 0, moveDirection[i - 1].z + transform.position.z);

        }

        animator = GetComponent<Animator>();
    }

    public override void Move(Vector3[] movePoints, Vector3[] moveDirections, Animator animator)
    {
        movePoints = movePoint;
        moveDirections = moveDirection;
        animator = this.animator;
        // �� Ư���� �̵� ��� ����
        base.Move(movePoints, moveDirections, animator); // �⺻ ���� ȣ��
    }

    //public void Move()
    //{
    //    AIManager.TileMap[(int)(transform.position.x / 2), (int)(transform.position.z / 2)] = 0;
    //    Vector3 target = Hunter.HunterPosition;
    //    Vector3 curPosition = new Vector3(transform.position.x, 0, transform.position.z);
    //
    //    float distance = 100;
    //    int minDirection = 0;
    //
    //    movePoint[0] = transform.position;
    //    // ���� ��ġ���� �̵��� �� �ִ� ��ġ �����ϱ�
    //    for (int i = 1; i < movePoint.Length; i++)
    //    {
    //        movePoint[i] = new Vector3(moveDirection[i - 1].x + transform.position.x, 0, moveDirection[i - 1].z + transform.position.z);
    //
    //    }
    //
    //    // �̵��� ��ġ �� target�� ���� ������ ��ġ ã��
    //    for (int i = 0; i < movePoint.Length; i++)
    //    {
    //        float temp;
    //        temp = Mathf.Abs((movePoint[i].x - target.x) + (movePoint[i].z - target.z));
    //
    //        if (movePoint[i].x >= -0.01f && movePoint[i].x <= 14 && movePoint[i].z >= -0.01f && movePoint[i].z <= 14)
    //        {
    //            if (AIManager.TileMap[(int)(movePoint[i].x / 2), (int)(movePoint[i].z / 2)] != 1)
    //            {
    //                if (temp < distance)
    //                {
    //                    distance = temp;
    //                    minDirection = i;
    //                }
    //            }
    //        }
    //    }
    //    AIManager.TileMap[(int)(movePoint[minDirection].x / 2), (int)(movePoint[minDirection].z) / 2] = 1;
    //    StartCoroutine(JumpToPosition(new Vector3(movePoint[minDirection].x, 0, movePoint[minDirection].z), jumpDuration, jumpHeight));
    //}
    //
    //public IEnumerator JumpToPosition(Vector3 targetPosition, float jumpDuration, float jumpHeight)
    //{
    //    animator.SetTrigger("Jump");
    //    Vector3 startPosition = transform.position;
    //    float elapsedTime = 0f;
    //
    //    while (elapsedTime < jumpDuration)
    //    {
    //        float t = elapsedTime / jumpDuration;
    //        float height = Mathf.Sin(t * Mathf.PI) * jumpHeight; // ���� �
    //
    //        Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t);
    //        transform.position = new Vector3(currentPosition.x, startPosition.y + height, currentPosition.z);
    //
    //        elapsedTime += Time.deltaTime;
    //        yield return null; // ���� �����ӱ��� ���
    //    }
    //
    //    transform.position = new Vector3(targetPosition.x, startPosition.y, targetPosition.z);
    //    transform.LookAt(Hunter.HunterPosition);
    //    Vector3 eulerAngles = transform.eulerAngles;
    //
    //    // ���� ���� 0~360 ������ ��ȯ�մϴ�.
    //    float yRotation = Mathf.Round(eulerAngles.y / 90) * 90;
    //
    //    // ȸ������ �����մϴ�.
    //    transform.rotation = Quaternion.Euler(eulerAngles.x, yRotation, eulerAngles.z);
    //    if (transform.position == Hunter.HunterPosition)
    //    {
    //        Attack();
    //    }
    //}


    public void Attack()
    {
        StartCoroutine(ActiveAttackBox());
    }

    private IEnumerator ActiveAttackBox()
    {
        animator.SetTrigger("Attack");

        // GameObject�� Ȱ��ȭ�մϴ�.
        AttackBox.SetActive(true);

        // ������ �ð� ���� ����մϴ�.
        yield return new WaitForSeconds(1.0f);

        // GameObject�� ��Ȱ��ȭ�մϴ�.
        AttackBox.SetActive(false);
    }
}
