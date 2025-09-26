using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public float _jump;
    public GameObject[] Lane;

    void Start()
    {

    }

    void Update()
    {
        
            Jump();
        
    }

    public void Jump()
    {
        //ตำแหน่งที่กระโดดให้ไปที่Lane นั้นทีนที
        //ใช้ปุ้ม D จะโจมตีทีเลน 1 F ปุ่ม จะ parry ที่เลน 1
        //ใช้ปุ้ม J จะโจมตีทีเลน 2 K ปุ่ม จะ parry ที่เลน 2

        // ตรวจจับการกดปุ่มและย้ายไปเลนนั้นทันที
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = Lane[0].transform.position; // ไป Lane 1
            animator.SetTrigger("Attack"); // โจมตี
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position = Lane[0].transform.position; // ไป Lane 1
            animator.SetTrigger("Parry"); // ป้องกัน
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            transform.position = Lane[1].transform.position; // ไป Lane 2
            animator.SetTrigger("Attack"); // โจมตี
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            transform.position = Lane[1].transform.position; // ไป Lane 2
            animator.SetTrigger("Parry"); // ป้องกัน
        }
    }
}