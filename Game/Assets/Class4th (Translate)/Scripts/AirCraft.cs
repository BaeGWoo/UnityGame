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
        //밀림현상 방지를 위해 GetAxis보다 GetAxisRaw를 사용해 줍니다.
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        //     P    =    P0    +   V  *   T
        // 다음 위치 = 현재위치 + 방향 * 시간(time.delta)

        // 벡터의 정규화 ->대각이동 때 빨라지는 것을 방지하기 위해 단위벡터화 해줍니다.
        direction.Normalize();

        // Time.deltaTime : 마지막 프레임이 완료된 후 경과한 시간을 초 단위로 반환하는 값입니다.
        transform.position=transform.position + direction*speed*Time.deltaTime;

        

    }
}
