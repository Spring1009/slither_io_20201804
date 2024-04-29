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

	private int sortingOrder = 0; // ���̾� ������ ��Ÿ���� ����

	private void Start()
	{
		positions.Add(WormHeadGfx.position);

		// ������ ���� ��, �ڽ��� ������ �ִ� ���� ����Ʈ�� �� �������� ���۷����� �浹 ��ũ��Ʈ�� static �Լ��� ����
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

		// ���ο� tail�� Sorting Order�� �����Ͽ� �ռ� ������ gfx�麸�� �ڿ� ��Ÿ������ ��
        tail.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder--;
	}
}
