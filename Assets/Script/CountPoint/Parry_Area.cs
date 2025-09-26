using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry_Area : MonoBehaviour
{
    public int Point = 1;

    public PointManager pointManager;
    void Start()
    {
        
            pointManager = FindObjectOfType<PointManager>();
        
       

    }

    void Update()
    {
        Close();
    }

    public void Close()
    {
        StartCoroutine(DelayClose());
        IEnumerator DelayClose()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Parry"))
        {
            Debug.Log("Parry");
            Destroy(other.gameObject);          
            pointManager.getpoint(Point);       
            gameObject.SetActive(false);    
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Parry"))
        {
            Debug.Log("Parry");

            Destroy(other.gameObject);
            pointManager.getpoint(Point);
            gameObject.SetActive(false);
        }
    }
}
