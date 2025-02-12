using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public TMP_Text scoreText;
    private int score = 0;

    public static InputManager instance;
    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) CheckHit('W');
        if (Input.GetKeyDown(KeyCode.A)) CheckHit('A');
        if (Input.GetKeyDown(KeyCode.S)) CheckHit('S');
        if (Input.GetKeyDown(KeyCode.D)) CheckHit('D');

        scoreText.text = "Score: " + score;
    }

    void CheckHit(char key)
    {
        if (rhythmManager.activeNotes.Count > 0)
        {
            GameObject note = rhythmManager.activeNotes[0];
            float distance = Mathf.Abs(note.GetComponent<RectTransform>().anchoredPosition.y - rhythmManager.hitZone.anchoredPosition.y);

           
            if (rhythmManager.CheckNextKey(key,note))
            {
                
               


            }
        }
    }
    public void DestroyNote(GameObject n)
    {
        score += 10;
        rhythmManager.activeNotes.RemoveAt(0);
        Destroy(n);
    }
}
