using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{   
    // ���� ����
    public Transform followerPrefab; // �������� ������
    public int initialFollowerCount = 10; // �ʱ� ������ ��

    // ���� ����
    public Transform followerTf; // ����ٴ� �������� Transform
    public float diameter; // ������ ������ ����(����)

    private List<Transform> followers = new List<Transform>(); // ����ٴϴ� �����̵��� Transform�� ���� ����Ʈ
    private List<Vector2> followerPos = new List<Vector2>(); // ����ٴϴ� �����̵��� ��ġ�� ���� ����Ʈ
    float h; // ���� �̵���
    float v; // ���� �̵���
    public float speed = 2f; // �÷��̾� �̵� �ӵ�

    void Start(){
        followerPos.Add(followerTf.position); // �ʱ� ����ٴϴ� ������ ��ġ ����
    }

    void Update()
    {
       v = Input.GetAxisRaw("Vertical"); // ���Ʒ����� �Է°�(-1, 0, 1)
       h = Input.GetAxisRaw("Horizontal"); // �¿���� �Է°�(-1, 0, 1)
       
       // �÷��̾� �̵�
       transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;
    }
    
    void LastUpdate(){
        MakeCivilianFollow(); // ����ٴϴ� ������ ��ġ ������Ʈ
    }

    void MakeCivilianFollow(){
        // ����ٴϴ� �����̵� ������ �Ÿ� ���
        float distance = ((Vector2)followerTf.position - followerPos[0]).magnitude;
        
        if (distance > diameter) { // ���� ������ ������ �Ÿ��� ������ ���溸�� ũ��
            Vector2 direction = ((Vector2)followerTf.position - followerPos[0]).normalized; // �̵� ���� ���
            
            // ���� ��ġ ����Ͽ� ����Ʈ�� �߰�
            followerPos.Insert(0, followerPos[0] + direction * diameter);
            // ������ ��ġ ���� �����Ͽ� �ڷ� �������� ���� ����
            followerPos.RemoveAt(followerPos.Count - 1);
            
            distance -= diameter; // �Ÿ� �ʱ�ȭ
        }
        
        // ����ٴϴ� �����̵��� ��ġ ������Ʈ
        for (int i=0; i<followers.Count; i++){
            followers[i].position = Vector2.Lerp(followerPos[i+1], followerPos[i], distance/diameter);
            // Lerp �Լ��� ����Ͽ� ���� ��ġ�� ���� ��ġ ���̸� �ε巴�� �̵���Ŵ
        }    
    }

    public void AddFollower(){
        // ���ο� ������ ���� �� ��ġ ����
        Transform follower = Instantiate(followerTf, followerPos[followerPos.Count-1], Quaternion.identity, transform);
        followers.Add(follower); // ������ �����̸� ����Ʈ�� �߰�
        followerPos.Add(follower.position); // ������ �������� ��ġ�� ����Ʈ�� �߰�
    }
}
