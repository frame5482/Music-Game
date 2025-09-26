using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private NoteEvent evt;

    public void Initialize(NoteEvent e)
    {
        evt = e;
        // ทำอะไรก็ได้ตามชนิด เช่น ถ้าเป็น Hold ให้ขยายความยาวของ sprite
        // หรือกำหนดความเร็วการเคลื่อนที่ตาม spawnAheadTime
    }

    void Update()
    {
        // ตัวอย่าง: โน้ตเคลื่อนที่ลงไปตามแนว Y (ปรับตามระบบของคุณ)
        // transform.position += Vector3.down * (speed * Time.deltaTime);
    }

    // เมื่อตีโดน / พลาด ให้ทำลายหรือเล่นอนิเมชัน
    public void Hit()
    {
        Destroy(gameObject);
    }
}