using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class note : MonoBehaviour
{
    public float speed = 5f;         // ความเร็วเคลื่อนที่
    public float duration = 0f;      // สำหรับ Hold/Attack type ถ้าต้องใช้
    private Rigidbody rb;

    public enum NoteType { Attack, Parry }
    public NoteType noteType;

    // Event callback (optional) เพื่อให้ NoteSpawner รู้ว่าโดนตี/แพรี่แล้ว
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

        // ทำ effect ที่ต้องการ (เช่น เล่นอนิเมชัน / เสียง)
       // ScoreManager.Instance.AddScore(1); // สมมติคุณมี ScoreManager
        Destroy(gameObject);
    }
    public void OnParry()
    {
        if (noteType != NoteType.Parry) return;

        // ทำ effect ที่ต้องการ
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