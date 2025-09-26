using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class note : MonoBehaviour
{
    public float speed = 5f;         // ������������͹���
    public float duration = 0f;      // ����Ѻ Hold/Attack type ��ҵ�ͧ��
    private Rigidbody rb;

    public enum NoteType { Attack, Parry }
    public NoteType noteType;

    // Event callback (optional) ������� NoteSpawner ������ⴹ��/��������
    public System.Action<note> onHit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false; 
            rb.isKinematic = true; 
        }
    }
    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void OnAttack()
    {
        if (noteType != NoteType.Attack) return;

        // �� effect ����ͧ��� (�� ���͹����ѹ / ���§)
       // ScoreManager.Instance.AddScore(1); // ����Ԥس�� ScoreManager
        Destroy(gameObject);
    }
    public void OnParry()
    {
        if (noteType != NoteType.Parry) return;

        // �� effect ����ͧ���
       // ScoreManager.Instance.AddScore(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack") && noteType == NoteType.Attack)
        {
            OnAttack();
        }
        else if (other.CompareTag("PlayerParry") && noteType == NoteType.Parry)
        {
            OnParry();
        }
    }
}