using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WormCollision : MonoBehaviour
{
    public static List<List<Transform>> worms = new();
    public static List<float> wormRadiuses = new();

    // worms 리스트에 각 지렁이들의 몸통 리스트들을 저장, 반지름도 함께 저장함에 따라 같은 인덱스로 같은 지렁이에 접근 가능
    public static void AddWorms(List<Transform> _worm, ref float _wormRadius)
    { 
        worms.Add(_worm);
        wormRadiuses.Add(_wormRadius);
    }

    
    void Update()
    {
        // 각 지렁이별 반복문
        for(int j = 0; j < worms.Count; j++)
        {
            // 이 스크립트가 붙어있는 오브젝트가 지렁이이고, 현재 worms 리스트의 j 인덱스가 자신과 같은 지렁이일경우 다음으로 패스
            if (GetComponent<WormTail>() != null)
            {
                if (worms[j].SequenceEqual(GetComponent<WormTail>().wormTail))
                {
                    continue;
                }
            }

            //현재 worms 리스트의 j 인덱스에 있는 각 몸통들을 통한 충돌 연산
            List<Transform> _worm = worms[j];

            for (int i = 1; i < _worm.Count; i++)
            {
                Debug.Log(_worm.Count);
                // 자신 앞 인덱스와 자신 인덱스 간의 위치를 통해 충돌 감지
                Vector3 start = _worm[i - 1].position;
                Vector3 end = _worm[i].position;

                Vector3 lineDirection = end - start;
                Vector3 circleDirection = transform.position - start;

                Debug.DrawLine(start, end, Color.red);


                float lineLength = lineDirection.magnitude;

                lineDirection.Normalize();

                float dotProduct = Vector3.Dot(circleDirection, lineDirection);

                // 구한 내적을 바탕으로 선분 위의 가장 가까운 점을 구함
                Vector3 point;
                if (dotProduct <= 0)
                {
                    point = start;
                }
                else if (dotProduct >= lineLength)
                {
                    point = end;
                }
                else
                {
                    point = start + lineDirection * dotProduct;
                }

                // 현재 위치와 위에서 구한 point를 바탕으로 충돌 감지
                float distance = Vector3.Distance(point, transform.position);

                if (distance <= wormRadiuses[j] + 0.5f)     //0.5f는 자신의 wormTail에서 circleDiameter 를 가져와 더할 예정
                {
                    GetComponent<SpriteRenderer>().color = Color.red;
                    return;
                }
                else
                {
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
        
    }
}
