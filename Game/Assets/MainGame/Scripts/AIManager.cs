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
        while (count>=0) // 게임이 계속 진행되는 동안 반복
        {
            count--;
            // 동물의 이동
            AnimalMove();

            // 동물의 이동이 끝나기를 대기
            yield return new WaitForSeconds(1f); // 필요에 따라 시간 조정

            // Hunter의 이동
            hunter.Move();

            // Hunter의 이동이 끝나기를 대기
            yield return new WaitForSeconds(1f); // 필요에 따라 시간 조정

            AnimalAttack();

            // Hunter의 이동이 끝나기를 대기
            yield return new WaitForSeconds(1f); // 필요에 따라 시간 조정


            // 다음 턴을 위해 잠시 대기 (원하는 경우)
            yield return null;
        }
    }



    // Update is called once per frame
    void Update()
    {
     
    }
}
