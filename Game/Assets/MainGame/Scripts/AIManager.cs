using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class AIManager : MonoBehaviour
{
    [SerializeField] GameObject[] Animals;
    private List<Animal> animals = new List<Animal>();
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
            Animals[i].GetComponent<Animal>().Move();  
        }
    }

    public void AnimalAttack()
    {
        for (int i = 0; i < Animals.Length; i++)
        {
            Animals[i].GetComponent<Animal>().Attack();
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
            while (Hunter.Moveable)
            {
                yield return null; // �� ������ ���
            }
            Hunter.Moveable = false;
            AnimalAttack();

            // Hunter�� �̵��� �����⸦ ���
            yield return new WaitForSeconds(1f); // �ʿ信 ���� �ð� ����


            // ���� ���� ���� ��� ��� (���ϴ� ���)
            yield return null;
        }
    }



    
}
