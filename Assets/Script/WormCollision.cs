using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WormCollision : MonoBehaviour
{
    public static List<List<Transform>> worms = new();
    public static List<float> wormRadiuses = new();

    public static void AddWorms(ref List<Transform> _worm, ref float _wormRadius)
    { 
        worms.Add(_worm);
        wormRadiuses.Add(_wormRadius);
    }

    // Update is called once per frame
    void Update()
    {
        for(int j = 0; j < worms.Count; j++)
        {
            if (GetComponent<WormTail>() != null)
            {
                if (worms[j].SequenceEqual(GetComponent<WormTail>().wormTail))
                {
                    continue;
                }
            }
            List<Transform> _worm = worms[j];
            for (int i = 1; i < _worm.Count; i++)
            {
                Vector3 start = _worm[i - 1].position;
                Vector3 end = _worm[i].position;

                Vector3 lineDirection = end - start;
                Vector3 circleDirection = transform.position - start;

                Debug.DrawLine(start, end, Color.red);


                float lineLength = lineDirection.magnitude;

                lineDirection.Normalize();

                float dotProduct = Vector3.Dot(circleDirection, lineDirection);

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

                float distance = Vector3.Distance(point, transform.position);

                if (distance <= wormRadiuses[j] + 0.5f)
                {
                    GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                }
                else
                {
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
        
    }
}
