using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class AIManager : MonoBehaviour
{
    [SerializeField] GameObject[] Animals;
    public static int[,] TileMap = new int[8, 8];
    [SerializeField] Hunter hunter;
    int count = 5;

    private void Start()
    {
        StartCoroutine(TurnManager());
    }

    public void AnimalMove()
    {
        
        for (int i=0;i< Animals.Length; i++)
        {
            AnimalInterface animal = Animals[i].GetComponent<AnimalInterface>();
            if(animal != null)
            {
                animal.Move();
            }
        }
    }

    public void AnimalAttack()
    {

        for (int i = 0; i < Animals.Length; i++)
        {
            AnimalInterface animal = Animals[i].GetComponent<AnimalInterface>();
            if (animal != null)
            {
                animal.Attack();
            }
        }
    }

    private IEnumerator TurnManager()
    {
        while (count>=0) // ������ ��� ����Ǵ� ���� �ݺ�
        {
            count--;
            // ������ �̵�
            AnimalMove();

            // ������ �̵��� �����⸦ ���
            yield return new WaitForSeconds(1f); // �ʿ信 ���� �ð� ����

            // Hunter�� �̵�
            hunter.Move();

            // Hunter�� �̵��� �����⸦ ���
            yield return new WaitForSeconds(1f); // �ʿ信 ���� �ð� ����

            AnimalAttack();

            // Hunter�� �̵��� �����⸦ ���
            yield return new WaitForSeconds(1f); // �ʿ信 ���� �ð� ����


            // ���� ���� ���� ��� ��� (���ϴ� ���)
            yield return null;
        }
    }



    // Update is called once per frame
    void Update()
    {
     
    }
}
