using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public enum NoteType { Attack, Parry }

    public float time;       // เวลาที่โน้ตต้องถึงผู้เล่น (วินาที)
    public int laneIndex;    // เลน spawn
    public NoteType noteType;
    public float speed;      // ความเร็วของโน้ต (ยูนิต/วินาที)
}

[CreateAssetMenu(fileName = "Timeline", menuName = "MusicGame/Timeline")]
public class TimelineBase : ScriptableObject
{
    public List<NoteData> notes = new List<NoteData>();
}