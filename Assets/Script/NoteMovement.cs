using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    private Vector3 target;
    private float speed;

    public void Initialize(Vector3 targetPos, float moveSpeed)
    {
        target = targetPos;
        speed = moveSpeed;
    }
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // พลาดเมื่อผู้เล่นไม่กด
            Destroy(gameObject);
        }
    }
}
