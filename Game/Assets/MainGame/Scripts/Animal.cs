using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    public float jumpHeight = 2f; // ���� ����
    public float jumpDuration = 1f; // ���� �ִϸ��̼��� ���� �ð�
    public virtual void Move(Vector3[] movePoint, Vector3[] moveDirection, Animator animator)
    {
        AIManager.TileMap[(int)(transform.position.x / 2), (int)(transform.position.z / 2)] = 0;
        Vector3 target = Hunter.HunterPosition;
        //Vector3 curPosition = new Vector3(transform.position.x, 0, transform.position.z);

        float distance = 100;
        int minDirection = 0;

      

        // �̵��� ��ġ �� target�� ���� ������ ��ġ ã��
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
        //StartCoroutine(JumpToPosition(new Vector3(movePoint[minDirection].x, 0, movePoint[minDirection].z), jumpDuration, jumpHeight));
        JumpToPosition(new Vector3(movePoint[minDirection].x, 0, movePoint[minDirection].z), jumpDuration, jumpHeight,animator);
    }

    public virtual void JumpToPosition(Vector3 targetPosition, float jumpDuration, float jumpHeight,Animator animator)
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
            //yield return null; // ���� �����ӱ��� ���
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
            //Attack();
        }
    }
}
