using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllerMenu : MonoBehaviour {
    public Text levelDifficulty;
    public Text totalDrones;
    public Text hitDrones;
    public Text songTitle;
    public Button continueFunction;
    public Button restart;
    public Button _menu;
    GameModule game;
    // Use this for initialization
    void Start ()
    {
        game = GameObject.Find("GameModule").GetComponent<GameModule>();
        
        totalDrones.text = game.noteTotal.ToString();
        hitDrones.text = game.hitTotal.ToString();
        

        continueFunction.onClick.AddListener(continueSong);
        restart.onClick.AddListener(restartSong);
        _menu.onClick.AddListener(goToMenu);

    }

    void goToMenu()
    {
        if (SelectSong.isFiveNote)
        {
            //FiveNoteGrid.stopAllAudio();
            //FiveNoteGrid.endSong();
            gameObject.transform.parent.gameObject.SetActive(false);
        }            
    }

    void restartSong()
    {
        if (SelectSong.isFiveNote)
        {
            //FiveNoteGrid.stopAllAudio();
            //FiveNoteGrid.beginGame(FiveNoteGrid.file);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    void continueSong()
    {
        if (SelectSong.isFiveNote)
        {
            //FiveNoteGrid.unPaused();
        }
    }

    // Update is called once per frame
    void Update () {
        
    }
}
