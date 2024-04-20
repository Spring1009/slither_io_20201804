using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WormMove : MonoBehaviour
{
	public float speed = 3f;
	public float rotationSpeed = 200f;
	public float skillDuration = 3f; // ��ų ���� �ð� ����
    public TextMeshProUGUI skillW; // ��ų UI�� TextMeshPro

	float velX = 0f;
	bool isUsingSkill = false; // ��ų ��� ���θ� ��Ÿ���� ����

    private void Update()
    {
        velX = Input.GetAxisRaw("Horizontal");
        // W Ű �Է� ����
        if (Input.GetKey(KeyCode.W))
        {
            if (!isUsingSkill)
            {
                StartCoroutine(UseSkill());
            }
        }
    }

    private void FixedUpdate()
    {
        // ��ų ��� ���� ���� �̵����� �ʵ��� ó��
        if (!isUsingSkill)
        {
            //������
            transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        }
        //ȸ��
        transform.Rotate(Vector3.forward * -velX * rotationSpeed * Time.fixedDeltaTime);
    }

    
    IEnumerator UseSkill()
    {
        // ��ų UI�� ������ ����
        skillW.color = Color.red;
        isUsingSkill = true;
        // ��ų ���� �ð���ŭ ��ٸ�
        yield return new WaitForSeconds(skillDuration);
        isUsingSkill = false;
        // ��ų UI�� ������ ������� ����
        skillW.color = Color.white;
    }
}
