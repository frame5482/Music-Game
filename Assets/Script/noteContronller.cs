using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteController : MonoBehaviour
{
    public TimelineBase timeline;      // ScriptableObject ที่เก็บ timeline โน้ต
    public GameObject[] notePrefabs;   // index 0 = Attack, 1 = Parry
    public Transform[] laneTransforms; // ตำแหน่ง spawn ของแต่ละเลน
    public Transform[] playerTransform;  // ตำแหน่งผู้เล่น
    public bool isPlaying = false;

    void Start()
    {
        // เริ่มเกมอัตโนมัติ
        summon();
    }

    public void summon()
    {
        isPlaying = true;
        StartCoroutine(SpawnNotes());
    }

    IEnumerator SpawnNotes()
    {
        float startTime = Time.time;

        foreach (var noteData in timeline.notes)
        {
            Transform lane = laneTransforms[noteData.laneIndex];

            // คำนวณระยะทางจาก spawn → player
            float distance = Vector3.Distance(lane.position, playerTransform[noteData.laneIndex].position);

            // คำนวณเวลาที่ต้อง spawn ล่วงหน้า
            // spawnTime = เวลาที่โน้ตต้องถึง player - (distance / speed)
            float travelTime = distance / noteData.speed;
            float spawnTime = noteData.time - travelTime;

            // รอจนถึงเวลาที่ spawn
            float waitTime = spawnTime - (Time.time - startTime);
            if (waitTime > 0)
                yield return new WaitForSeconds(waitTime);


            // Spawn โน้ต
            GameObject prefab = notePrefabs[(int)noteData.noteType];
            GameObject go = Instantiate(prefab, lane.position, Quaternion.identity);

            // กำหนดให้โน้ตเคลื่อนที่ไปยังผู้เล่น
            NoteMovement move = go.GetComponent<NoteMovement>();
            if (move != null)
            {
                move.Initialize(playerTransform[noteData.laneIndex].position, noteData.speed);
            }
        }
    }

    public void End()
    {
        isPlaying = false;
        StopAllCoroutines();
        Debug.Log("Game Ended");
        // ทำระบบจบเกม / คะแนน / animation ฯลฯ
    }
}
