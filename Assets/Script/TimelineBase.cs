using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public enum NoteType { Attack, Parry }

    public float time;       // ���ҷ���鵵�ͧ�֧������ (�Թҷ�)
    public int laneIndex;    // �Ź spawn
    public NoteType noteType;
    public float speed;      // �������Ǣͧ�� (�ٹԵ/�Թҷ�)
}

[CreateAssetMenu(fileName = "Timeline", menuName = "MusicGame/Timeline")]
public class TimelineBase : ScriptableObject
{
    public List<NoteData> notes = new List<NoteData>();
}