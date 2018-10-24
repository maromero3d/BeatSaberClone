using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TwelveNoteGame : MonoBehaviour {
    
    string file = "";
    bool paused = false;
    public static bool playtrack = false;
    public static bool makeHard = false;
    public static List<NoteData> notes;
    public static List<ObstacleData> obstacleDatas;
    public static List<BeatSaberEvents> beatSaberEvents;
    public static float secPerBeat;
    public static float BeatPerSec;
    public static float dsptimesong;
    public static float songPosition;
    public static float songPosInBeats;
    public static int nextIndex = 0;
    GameModule game;
    // Use this for initialization
    void Start () {
        game = GameObject.Find("GameModule").GetComponent<GameModule>();
    }

    public void beginGame(string filename)
    {
        game = GameObject.Find("GameModule").GetComponent<GameModule>();
        nextIndex = 0;
        file = filename;
        game.MenuObject.SetActive(false);
        SpawnObjects(difficultyContent.selectedTwelveNoteChart, file);
        game.Audio.GetComponent<AudioSource>().time = 0;
        playtrack = true;
        game.Audio.GetComponent<AudioSource>().Play();
    }

    void SpawnObjects(Info infoForNotes, string file)
    {
        notes = new List<NoteData>();
        obstacleDatas = new List<ObstacleData>();
        beatSaberEvents = new List<BeatSaberEvents>();

        var info = File.ReadAllText(infoForNotes.path + "/" + file);
        var te = JsonConvert.DeserializeObject<BeatSaberNoteInfo>(info);
        BeatPerSec = difficultyContent.selectedTwelveNoteChart.beatsPerMinute / 60f;
        secPerBeat = 60f / difficultyContent.selectedTwelveNoteChart.beatsPerMinute;
        dsptimesong = (float)AudioSettings.dspTime;

        foreach (NoteData note in te._notes)
        {
            SpawnNote(transform, note);
        }
        Comparison<NoteData> comparison = (x, y) => x._time.CompareTo(y._time);
        notes.Sort(comparison);
        
        foreach (BeatSaberEvents _event in te._events)
        {
            SpawnEvent(transform, _event);
        }
        
        foreach (ObstacleData _obstacle in te._obstacles)
        {
            SpawnObstacle(transform, _obstacle);
        }
    }

    void SpawnNote(Transform parent, NoteData note)
    {
        notes.Add(note);
    }

    void SpawnEvent(Transform parent, BeatSaberEvents note)
    {
        beatSaberEvents.Add(note);
    }

    void SpawnObstacle(Transform parent, ObstacleData note)
    {
        obstacleDatas.Add(note);
    }

    void FixedUpdate()
    {
        if (playtrack)
        {
            if (nextIndex < notes.Count && (notes[nextIndex]._time < (songPosInBeats - difficultyContent.selectedTwelveNoteChart.difficultyLevels[0].offset) + (4 * BeatPerSec)))
            {
                GameObject newNote;
                string tmp = "";

                if (notes[nextIndex]._type == Hand.blue)
                    tmp = "Blue";

                if (notes[nextIndex]._type == Hand.red)
                    tmp = "Red";

                if (notes[nextIndex]._type == Hand.Bomb)
                    tmp = "Bomb";

                if (notes[nextIndex]._type == Hand.blue && notes[nextIndex]._cutDirection == _cutType._any)
                    tmp = "AnyBlue";

                if (notes[nextIndex]._type == Hand.red && notes[nextIndex]._cutDirection == _cutType._any)
                    tmp = "AnyRed";

                newNote = Instantiate(Resources.Load("Prefabs/" + tmp) as GameObject);
                newNote.name = nextIndex.ToString();
                newNote.AddComponent<TwelveNoteCube>();
                newNote.GetComponent<TwelveNoteCube>().note = notes[nextIndex];
                newNote.GetComponent<TwelveNoteCube>().noteIndex = nextIndex;
                newNote.transform.SetParent(gameObject.transform);

                Vector3 position = new Vector3(game.boss.transform.position.x, game.boss.transform.position.y, game.boss.transform.position.z);

                newNote.transform.position = position;
                nextIndex++;
            }    

            /*if (controller11.GetComponent<SteamVR_TrackedController>().menuPressed)
            {
                playtrack = false;
                Audio.GetComponent<AudioSource>().Pause();
                controllerMenu.SetActive(true);
                resetSaber(true);
                Invoke("Paused", 0.5f);
                return;
            }*/

            if (!game.Audio.GetComponent<AudioSource>().isPlaying)
                endSong();


        }

        /*if (paused)
        {
            if (controller11.GetComponent<SteamVR_TrackedController>().menuPressed)
            {
                paused = false;
                Invoke("unPaused", 0.5f);

            }
        }*/
    }

    public void endSong()
    {
        playtrack = false;
        game.MenuObject.SetActive(true);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
        songPosition = (float)(AudioSettings.dspTime - dsptimesong);
        songPosInBeats = songPosition / secPerBeat;
    }

    public void stopAudio()
    {
        if (difficultyContent.selectedTwelveNoteChart.audioClip != null)
        {
            game.Audio.Stop();
        }
    }

    public void playAudioAt(float delay)
    {
        if (difficultyContent.selectedTwelveNoteChart.audioClip != null)
        {
            game.Audio.PlayScheduled(AudioSettings.dspTime + delay);
        }
    }
}
