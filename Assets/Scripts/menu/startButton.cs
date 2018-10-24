using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startButton : MonoBehaviour {
    public GameModule GameModule;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(startTrack);
    }

    void startTrack()
    {
        GameObject newNote = Instantiate(Resources.Load("Prefabs/grid") as GameObject);
        newNote.transform.position = new Vector3(0f, 2.6f, 32f);
        newNote.GetComponent<TwelveNoteGame>().beginGame(difficultyContent.diffcultyLevel);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
