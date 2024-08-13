using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCraft : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float speed=1.0f;
 

    void Start()
    {
        
    }

    
    void Update()
    {
        //�и����� ������ ���� GetAxis���� GetAxisRaw�� ����� �ݴϴ�.
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        //     P    =    P0    +   V  *   T
        // ���� ��ġ = ������ġ + ���� * �ð�(time.delta)

        // ������ ����ȭ ->�밢�̵� �� �������� ���� �����ϱ� ���� ��������ȭ ���ݴϴ�.
        direction.Normalize();

        // Time.deltaTime : ������ �������� �Ϸ�� �� ����� �ð��� �� ������ ��ȯ�ϴ� ���Դϴ�.
        transform.position=transform.position + direction*speed*Time.deltaTime;

        

    }
}
