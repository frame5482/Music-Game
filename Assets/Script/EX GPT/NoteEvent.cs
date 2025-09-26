using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteEvent
{
    public enum NoteType { Tap, Hold, Slide, Mine } // ขยายได้ตามต้องการ

    [Tooltip("เวลาเป็นวินาที (เช่น 90 = 1:30), หรือใช้ helper ParseMinuteSecond")]
    public float timeSeconds;

    [Tooltip("ลำดับเลน (0..n-1) ให้ตรงกับ laneTransforms ใน NoteSpawner")]
    public int laneIndex;

    public NoteType noteType;

    // optional: hold length (วินาที) สำหรับ Hold
    public float holdDuration;

    public string meta; // ข้อมูลเสริม (ชื่อโน้ต, id ฯลฯ)
}
