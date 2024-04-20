using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WormMove : MonoBehaviour
{
	public float speed = 3f;
	public float rotationSpeed = 200f;
	public float skillDuration = 3f; // 스킬 지속 시간 설정
    public TextMeshProUGUI skillW; // 스킬 UI의 TextMeshPro

	float velX = 0f;
	bool isUsingSkill = false; // 스킬 사용 여부를 나타내는 변수

    private void Update()
    {
        velX = Input.GetAxisRaw("Horizontal");
        // W 키 입력 감지
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
        // 스킬 사용 중일 때는 이동하지 않도록 처리
        if (!isUsingSkill)
        {
            //움직임
            transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        }
        //회전
        transform.Rotate(Vector3.forward * -velX * rotationSpeed * Time.fixedDeltaTime);
    }

    
    IEnumerator UseSkill()
    {
        // 스킬 UI의 색상을 변경
        skillW.color = Color.red;
        isUsingSkill = true;
        // 스킬 지속 시간만큼 기다림
        yield return new WaitForSeconds(skillDuration);
        isUsingSkill = false;
        // 스킬 UI의 색상을 원래대로 복구
        skillW.color = Color.white;
    }
}
