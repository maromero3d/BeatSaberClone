using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SelectSong : MonoBehaviour
{
    public Canvas TargetCanvas;
    public int arrayID;
    AudioSource Audio;
    AudioSource RhythmAudio;
    AudioSource GuitarAudio;
    GameObject start;
    public string noteArrayCount;
    public static bool isFiveNote = false;
    
    void Start()
    {
        start = GameObject.Find("start");
        Audio = GameObject.Find("GameModule").GetComponent<GameModule>().Audio;
        RhythmAudio = GameObject.Find("GameModule").GetComponent<GameModule>().RhythmAudio;
        GuitarAudio = GameObject.Find("GameModule").GetComponent<GameModule>().GuitarAudio;

        gameObject.GetComponent<Button>().onClick.AddListener(GoToTarget);
    }

    void GoToTarget()
    {
       if (start.GetComponent<Button>().interactable)
            start.GetComponent<Button>().interactable = false;

        if (noteArrayCount == "12")
        {
            Audio.clip = songContent.songTwelveNoteArray[arrayID].audioClip;
            if (!Audio.isPlaying)
            {
                setSong();
                songContent.selectedTwelveNoteChart = songContent.songTwelveNoteArray[arrayID];
                isFiveNote = false;
                Audio.time = songContent.songTwelveNoteArray[arrayID].previewStartTime;
                Audio.Play();
            }
            else if (Audio.name != songContent.songTwelveNoteArray[arrayID].songName)
            {
                return;
            }
        }
    }

    public void setSong()
    {
        songContent.setSong();
        var tarSprite = Resources.Load<Sprite>("Graphics/CharacterObjectTargeted");
        
        Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts)
        {
            if (t.gameObject.name == "selected")
            {
                Image realmSelect = t.gameObject.GetComponent<Image>();
                realmSelect.sprite = tarSprite;
            }
        }
        
    }

    public void OnMouseOver()
    {
        Debug.Log("Enter");
    }

    public void OnMouseExit()
    {
        Debug.Log("Exit");
    }
}
