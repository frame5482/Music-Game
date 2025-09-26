using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteEvent
{
    public enum NoteType { Tap, Hold, Slide, Mine } // ����������ͧ���

    [Tooltip("�������Թҷ� (�� 90 = 1:30), ������ helper ParseMinuteSecond")]
    public float timeSeconds;

    [Tooltip("�ӴѺ�Ź (0..n-1) ���ç�Ѻ laneTransforms � NoteSpawner")]
    public int laneIndex;

    public NoteType noteType;

    // optional: hold length (�Թҷ�) ����Ѻ Hold
    public float holdDuration;

    public string meta; // ����������� (������, id ���)
}
