using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmManager : MonoBehaviour
{
    public GameObject[] notePrefabs; // Prefab ของ UI Note (Image)
    public RectTransform spawnPoint;
    public RectTransform hitZone;
    public Transform noteParent; // Parent เป็น UI (Canvas)
    public float spawnRate = 0.5f;

    public List<string> sequences = new List<string>() { "WADS", "DWAS", "ASWD" }; // 🆕 ลำดับของ Set
    public int currentSequenceIndex = 0;
    public int currentNoteIndex = 0;

    public List<GameObject> activeNotes = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        string sequence = sequences[currentSequenceIndex];

        foreach (char key in sequence)
        {
            GameObject note = Instantiate(GetNotePrefab(key), noteParent); // 🆕 Spawn เป็น UI
            RectTransform rectTransform = note.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = spawnPoint.anchoredPosition; // 🆕 ใช้ตำแหน่ง UI
            Debug.Log(key);
            activeNotes.Add(note);
            yield return new WaitForSeconds(spawnRate); // Spawn ทีละตัวในชุด
        }
    }

    public bool CheckNextKey(char key,GameObject obj)
    {
        string sequence = sequences[currentSequenceIndex];

        if (key == sequence[currentNoteIndex])
        {
            currentNoteIndex++;
            InputManager.instance.DestroyNote(obj);
            if (currentNoteIndex >= sequence.Length) // ถ้ากดครบเซ็ต
            {
                NextSequence();
            }
            return true;
        }
        return false;
    }

    void NextSequence()
    {
        // ลบโน้ตเก่าทั้งหมด
        foreach (GameObject note in activeNotes)
        {
            Destroy(note);
        }
        activeNotes.Clear();

        // รีเซ็ตลำดับใหม่
        currentNoteIndex = 0;
        currentSequenceIndex = (currentSequenceIndex + 1) % sequences.Count;

        StartCoroutine(SpawnSequence());
    }

    GameObject GetNotePrefab(char key)
    {
        switch (key)
        {
            case 'W': return notePrefabs[0];
            case 'A': return notePrefabs[1];
            case 'S': return notePrefabs[2];
            case 'D': return notePrefabs[3];
            default: return null;
        }
    }
}
