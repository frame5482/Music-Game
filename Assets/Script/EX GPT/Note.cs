using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private NoteEvent evt;

    public void Initialize(NoteEvent e)
    {
        evt = e;
        // �����á�������Դ �� ����� Hold �����¤�����Ǣͧ sprite
        // ���͡�˹��������ǡ������͹����� spawnAheadTime
    }

    void Update()
    {
        // ������ҧ: ������͹���ŧ仵���� Y (��Ѻ����к��ͧ�س)
        // transform.position += Vector3.down * (speed * Time.deltaTime);
    }

    // ����͵�ⴹ / ��Ҵ ��������������͹����ѹ
    public void Hit()
    {
        Destroy(gameObject);
    }
}