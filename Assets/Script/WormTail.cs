using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormTail : MonoBehaviour
{
	public Transform WormTailGfx;
	public Transform WormHeadGfx;
	public float circleDiameter;

	public List<Transform> wormTail= new List<Transform>();
	private List<Vector2> positions = new List<Vector2>();

	private int sortingOrder = 0; // 레이어 순서를 나타내는 변수

	private void Start()
	{
		positions.Add(WormHeadGfx.position);

		// 지렁이 생성 시, 자신이 가지고 있는 몸통 리스트와 원 반지름의 레퍼런스를 충돌 스크립트의 static 함수로 전달
		WormCollision.AddWorms(ref wormTail, ref circleDiameter);
	}

	private void Update()
	{
		float distance = ((Vector2)WormHeadGfx.position - positions[0]).magnitude;

		if(distance > circleDiameter)
		{
			Vector2 direction = ((Vector2)WormHeadGfx.position - positions[0]).normalized;

			positions.Insert(0, positions[0] + direction * circleDiameter);
			positions.RemoveAt(positions.Count - 1);

			distance -= circleDiameter;
		}

		for (int i = 0; i < wormTail.Count; i++)
		{
			wormTail[i].position = Vector2.Lerp(positions[i+1], positions[i], distance / circleDiameter);
		}
	}

	public void AddTail()
	{
		Transform tail = Instantiate(WormTailGfx, positions[positions.Count - 1], Quaternion.identity, transform);
		wormTail.Add(tail);
		positions.Add(tail.position);

		// 새로운 tail에 Sorting Order를 설정하여 앞서 생성된 gfx들보다 뒤에 나타나도록 함
        tail.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder--;
	}
}
