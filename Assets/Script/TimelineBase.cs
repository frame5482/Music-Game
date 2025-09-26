using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public enum NoteType { Attack, Parry }

    public float time;      
    public int laneIndex;    
    public NoteType noteType;
    public float speed;     
}

[CreateAssetMenu(fileName = "Timeline", menuName = "MusicGame/Timeline")]
public class TimelineBase : ScriptableObject
{
    public List<NoteData> notes = new List<NoteData>();
}