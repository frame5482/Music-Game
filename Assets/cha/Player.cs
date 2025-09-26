using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public float _jump;
    public GameObject[] Lane;

    public GameObject[] AttackArea;
    public GameObject[] ParryArea;

    void Start()
    {

    }

    void Update()
    {
        
            Jump();
        
    }

    public void Jump()
    {
      
        if (Input.GetKeyDown(KeyCode.D))
        {

            transform.position = Lane[0].transform.position;
            AttackArea[0].SetActive(true);
            animator.SetTrigger("Attack"); 
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position = Lane[0].transform.position;
            ParryArea[0].SetActive(true);
            animator.SetTrigger("Parry");

        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            transform.position = Lane[1].transform.position;
            AttackArea[1].SetActive(true);
            animator.SetTrigger("Attack"); 
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            transform.position = Lane[1].transform.position;
            ParryArea[1].SetActive(true);
            animator.SetTrigger("Parry"); 
        }
    }
}