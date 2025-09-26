using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public class NoteSpawner : MonoBehaviour
{
    [Header("References")]
    public AudioSource musicSource; // ถ้ามี ให้ใช้เป็นตัวกำหนดเวลา
    public Transform[] laneTransforms; // ตำแหน่ง spawn ต่อ lane
    public GameObject tapNotePrefab;
    public GameObject holdNotePrefab;
    public GameObject slideNotePrefab;
    public GameObject minePrefab;

    [Header("Spawn Settings")]
    public float spawnAheadTime = 1.5f; // เวลาที่จะ spawn ล่วงหน้า (เพื่อให้โน้ตเดินมา)
    public bool useAudioTime = true;

    [Header("Timeline")]
    public List<NoteEvent> timeline = new List<NoteEvent>();

    // internal
    private float playbackStartRealTime = 0f;
    private float offset = 0f; // สำหรับ seek
    private bool isPlaying = false;
    private int nextIndex = 0;

    void Start()
    {
        SortTimeline();
    }

    void Update()
    {
        if (!isPlaying) return;

        float currentTime = GetCurrentTime();

        // spawn all events whose time is within (currentTime + spawnAheadTime)
        while (nextIndex < timeline.Count && timeline[nextIndex].timeSeconds <= currentTime + spawnAheadTime)
        {
            SpawnNote(timeline[nextIndex]);
            nextIndex++;
        }
    }

    float GetCurrentTime()
    {
        if (useAudioTime && musicSource != null)
        {
            return musicSource.time + offset;
        }
        else
        {
            return Time.realtimeSinceStartup - playbackStartRealTime + offset;
        }
    }

    void SpawnNote(NoteEvent e)
    {
        if (e.laneIndex < 0 || e.laneIndex >= laneTransforms.Length)
        {
            Debug.LogWarning($"NoteEvent laneIndex out of range: {e.laneIndex}");
            return;
        }

        Transform lane = laneTransforms[e.laneIndex];
        GameObject prefab = GetPrefabForType(e.noteType);
        if (prefab == null)
        {
            Debug.LogWarning($"No prefab for note type {e.noteType}");
            return;
        }

        // instantiate at lane position (customize as needed)
        GameObject go = Instantiate(prefab, lane.position, Quaternion.identity, transform);

        // Optional: pass event data to note script if you have one
        var noteComp = go.GetComponent<Note>();
        if (noteComp != null)
        {
            noteComp.Initialize(e);
        }
    }

    GameObject GetPrefabForType(NoteEvent.NoteType t)
    {
        switch (t)
        {
            case NoteEvent.NoteType.Tap: return tapNotePrefab;
            case NoteEvent.NoteType.Hold: return holdNotePrefab;
            case NoteEvent.NoteType.Slide: return slideNotePrefab;
            case NoteEvent.NoteType.Mine: return minePrefab;
            default: return tapNotePrefab;
        }
    }

    #region Timeline Management API (Runtime control)

    public void SortTimeline()
    {
        timeline = timeline.OrderBy(n => n.timeSeconds).ToList();
    }

    public void Play()
    {
        if (isPlaying) return;
        isPlaying = true;
        if (useAudioTime && musicSource != null)
        {
            musicSource.Play();
        }
        else
        {
            playbackStartRealTime = Time.realtimeSinceStartup;
        }
        // find nextIndex based on current time
        float t = GetCurrentTime();
        nextIndex = timeline.FindIndex(n => n.timeSeconds > t);
        if (nextIndex == -1) nextIndex = timeline.Count;
    }

    public void Pause()
    {
        if (!isPlaying) return;
        isPlaying = false;
        if (useAudioTime && musicSource != null)
        {
            musicSource.Pause();
        }
        else
        {
            // offset already accounts for elapsed time
            offset = GetCurrentTime();
        }
    }

    public void StopAndReset()
    {
        isPlaying = false;
        offset = 0f;
        playbackStartRealTime = Time.realtimeSinceStartup;
        if (useAudioTime && musicSource != null)
        {
            musicSource.Stop();
            musicSource.time = 0f;
        }
        nextIndex = 0;
    }

    /// <summary>
    /// Seek to absolute time (seconds)
    /// </summary>
    public void Seek(float seconds)
    {
        offset = seconds;
        if (useAudioTime && musicSource != null)
        {
            musicSource.time = seconds;
        }
        else
        {
            playbackStartRealTime = Time.realtimeSinceStartup - seconds;
        }
        // recalc nextIndex
        nextIndex = timeline.FindIndex(n => n.timeSeconds > seconds);
        if (nextIndex == -1) nextIndex = timeline.Count;
    }

    /// <summary>
    /// Convenience: add note by minute:second
    /// </summary>
    public void AddNoteByMinute(int minute, float second, NoteEvent.NoteType type, int lane, float holdDuration = 0f, string meta = "")
    {
        float t = minute * 60f + second;
        AddNoteAtTime(t, type, lane, holdDuration, meta);
    }

    public void AddNoteAtTime(float seconds, NoteEvent.NoteType type, int lane, float holdDuration = 0f, string meta = "")
    {
        NoteEvent e = new NoteEvent()
        {
            timeSeconds = seconds,
            laneIndex = lane,
            noteType = type,
            holdDuration = holdDuration,
            meta = meta
        };
        timeline.Add(e);
        SortTimeline();
        // optional: if playback is active, recompute nextIndex
        float t = GetCurrentTime();
        nextIndex = timeline.FindIndex(n => n.timeSeconds > t);
        if (nextIndex == -1) nextIndex = timeline.Count;
    }

    /// <summary>
    /// Remove all notes in a specific minute (e.g., minute = 2 will remove notes with time in [120,180) )
    /// </summary>
    public void ClearMinute(int minute)
    {
        float start = minute * 60f;
        float end = start + 60f;
        timeline.RemoveAll(n => n.timeSeconds >= start && n.timeSeconds < end);
        SortTimeline();
        nextIndex = timeline.FindIndex(n => n.timeSeconds > GetCurrentTime());
        if (nextIndex == -1) nextIndex = timeline.Count;
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Parse time string like "1:30" or "0:15.5" -> seconds
    /// </summary>
    public static float ParseMinuteSecond(string mmss)
    {
        if (string.IsNullOrEmpty(mmss)) return 0f;
        var parts = mmss.Split(':');
        if (parts.Length == 1)
        {
            if (float.TryParse(parts[0], out float s)) return s;
            return 0f;
        }
        if (parts.Length >= 2)
        {
            if (int.TryParse(parts[0], out int m) && float.TryParse(parts[1], out float s))
            {
                return m * 60f + s;
            }
        }
        return 0f;
    }

    #endregion
}