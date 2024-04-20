using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{   
    // 변수 선언
    public Transform followerPrefab; // 지렁이의 프리팹
    public int initialFollowerCount = 10; // 초기 지렁이 수

    // 변수 선언
    public Transform followerTf; // 따라다닐 지렁이의 Transform
    public float diameter; // 지렁이 사이의 간격(직경)

    private List<Transform> followers = new List<Transform>(); // 따라다니는 지렁이들의 Transform을 담을 리스트
    private List<Vector2> followerPos = new List<Vector2>(); // 따라다니는 지렁이들의 위치를 담을 리스트
    float h; // 수평 이동값
    float v; // 수직 이동값
    public float speed = 2f; // 플레이어 이동 속도

    void Start(){
        followerPos.Add(followerTf.position); // 초기 따라다니는 지렁이 위치 설정
    }

    void Update()
    {
       v = Input.GetAxisRaw("Vertical"); // 위아래로의 입력값(-1, 0, 1)
       h = Input.GetAxisRaw("Horizontal"); // 좌우로의 입력값(-1, 0, 1)
       
       // 플레이어 이동
       transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;
    }
    
    void LastUpdate(){
        MakeCivilianFollow(); // 따라다니는 지렁이 위치 업데이트
    }

    void MakeCivilianFollow(){
        // 따라다니는 지렁이들 사이의 거리 계산
        float distance = ((Vector2)followerTf.position - followerPos[0]).magnitude;
        
        if (distance > diameter) { // 만약 지렁이 사이의 거리가 설정된 직경보다 크면
            Vector2 direction = ((Vector2)followerTf.position - followerPos[0]).normalized; // 이동 방향 계산
            
            // 다음 위치 계산하여 리스트에 추가
            followerPos.Insert(0, followerPos[0] + direction * diameter);
            // 마지막 위치 정보 제거하여 뒤로 물러나는 것을 방지
            followerPos.RemoveAt(followerPos.Count - 1);
            
            distance -= diameter; // 거리 초기화
        }
        
        // 따라다니는 지렁이들의 위치 업데이트
        for (int i=0; i<followers.Count; i++){
            followers[i].position = Vector2.Lerp(followerPos[i+1], followerPos[i], distance/diameter);
            // Lerp 함수를 사용하여 현재 위치와 이전 위치 사이를 부드럽게 이동시킴
        }    
    }

    public void AddFollower(){
        // 새로운 지렁이 생성 및 위치 설정
        Transform follower = Instantiate(followerTf, followerPos[followerPos.Count-1], Quaternion.identity, transform);
        followers.Add(follower); // 생성된 지렁이를 리스트에 추가
        followerPos.Add(follower.position); // 생성된 지렁이의 위치를 리스트에 추가
    }
}
