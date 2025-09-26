using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public int Point;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void getpoint(int getPoint)
    {
        Point += getPoint;
    }
}
