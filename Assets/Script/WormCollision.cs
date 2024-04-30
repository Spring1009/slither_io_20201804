using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WormCollision : MonoBehaviour
{
    public static List<List<Transform>> worms = new();
    public static List<float> wormRadiuses = new();

    // worms ����Ʈ�� �� �����̵��� ���� ����Ʈ���� ����, �������� �Բ� �����Կ� ���� ���� �ε����� ���� �����̿� ���� ����
    public static void AddWorms(List<Transform> _worm, ref float _wormRadius)
    { 
        worms.Add(_worm);
        wormRadiuses.Add(_wormRadius);
    }

    
    void Update()
    {
        // �� �����̺� �ݺ���
        for(int j = 0; j < worms.Count; j++)
        {
            // �� ��ũ��Ʈ�� �پ��ִ� ������Ʈ�� �������̰�, ���� worms ����Ʈ�� j �ε����� �ڽŰ� ���� �������ϰ�� �������� �н�
            if (GetComponent<WormTail>() != null)
            {
                if (worms[j].SequenceEqual(GetComponent<WormTail>().wormTail))
                {
                    continue;
                }
            }

            //���� worms ����Ʈ�� j �ε����� �ִ� �� ������� ���� �浹 ����
            List<Transform> _worm = worms[j];

            for (int i = 1; i < _worm.Count; i++)
            {
                Debug.Log(_worm.Count);
                // �ڽ� �� �ε����� �ڽ� �ε��� ���� ��ġ�� ���� �浹 ����
                Vector3 start = _worm[i - 1].position;
                Vector3 end = _worm[i].position;

                Vector3 lineDirection = end - start;
                Vector3 circleDirection = transform.position - start;

                Debug.DrawLine(start, end, Color.red);


                float lineLength = lineDirection.magnitude;

                lineDirection.Normalize();

                float dotProduct = Vector3.Dot(circleDirection, lineDirection);

                // ���� ������ �������� ���� ���� ���� ����� ���� ����
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

                // ���� ��ġ�� ������ ���� point�� �������� �浹 ����
                float distance = Vector3.Distance(point, transform.position);

                if (distance <= wormRadiuses[j] + 0.5f)     //0.5f�� �ڽ��� wormTail���� circleDiameter �� ������ ���� ����
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
