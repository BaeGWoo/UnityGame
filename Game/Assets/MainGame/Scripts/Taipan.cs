using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taipan : Animal
{
    private Vector3[] movePoint = new Vector3[5];
    private Vector3[] moveDirection = new Vector3[4];
    private Animator animator;
    [SerializeField] GameObject AttackBox;

    private void Awake()
    {
        moveDirection[0] = new Vector3(4f, 0, 4);
        moveDirection[1] = new Vector3(-4, 0, 4);
        moveDirection[2] = new Vector3(-4, 0, -4);
        moveDirection[3] = new Vector3(4, 0, -4);
       

        animator = GetComponent<Animator>();
    }

    public override void Move()
    {
        movePoint[0] = transform.position;

        for (int i = 1; i < movePoint.Length; i++)
        {
            movePoint[i] = new Vector3(moveDirection[i - 1].x + transform.position.x, 0, moveDirection[i - 1].z + transform.position.z);
        }
        base.Move(transform.position, transform.rotation, movePoint, moveDirection);
    }
    public override void JumpAnimaition(){animator.SetTrigger("Jump");}

    


    public override void Attack()
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
