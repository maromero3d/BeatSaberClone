using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startButton : MonoBehaviour {
    public GameObject GameModule;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(startTrack);
    }

    void startTrack()
    {
        GameObject newNote = Instantiate(Resources.Load("Prefabs/TwelveNoteGrid") as GameObject);
       // newNote.GetComponent<TwelveNoteGame>().beginGame(difficultyContent.diffcultyLevel);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
