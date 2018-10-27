using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class songContent : MonoBehaviour
{

    public static bool newStart = false;
    public GameObject SongObject;
    public GameObject LeftController;
    public static List<Info> songTwelveNoteArray;
    public static Info selectedTwelveNoteChart;
    public static Info songTwelveNoteInfo;
    SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
    Vector2 touchpad;
    //float count = 0;
    private float sensitivityX = 1.5F;
    
    void Start()
    {
        StartCoroutine(loadSongs());       
    }

    IEnumerator loadSongs()
    {
        controller = LeftController.GetComponent<SteamVR_TrackedObject>();
        
        songTwelveNoteArray = new List<Info>();
        string dir = Application.dataPath + "/Songs/";
        uint TwlevelNoteIndex = 0;
        //int count = 0;
        foreach (string s in Directory.GetDirectories(dir))
        {            
            if (File.Exists(s + "/info.json"))
            {
                string newDir = @"" + s;
                songTwelveNoteInfo = new Info();
                songTwelveNoteInfo = LoadsongData(newDir + "/info.json");
                var newFileLoc = "file:///" + Uri.EscapeUriString(s);

                WWW audioLoader = new WWW(newFileLoc + "/" + songTwelveNoteInfo.difficultyLevels[0].audioPath);
                while (!audioLoader.isDone)
                {

                }

                songTwelveNoteInfo.path = newDir;
                songTwelveNoteInfo.audioClip = audioLoader.GetAudioClip();
                GameObject Song = Instantiate(SongObject);
                Song.transform.SetParent(gameObject.transform);
                Song.transform.localScale = new Vector3(1, 1, 1);
                Song.transform.localPosition = new Vector3(Song.transform.localPosition.x, Song.transform.localPosition.y, 0);
                Song.name = TwlevelNoteIndex.ToString();
                Song.GetComponent<SelectSong>().noteArrayCount = "12";
                Song.GetComponent<SelectSong>().arrayID = (int)TwlevelNoteIndex;
                songTwelveNoteArray.Add(songTwelveNoteInfo);
                Transform[] ts = Song.transform.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in ts)
                {
                    if (t.gameObject.name == "Artist")
                    {
                        t.gameObject.GetComponent<Text>().text = songTwelveNoteInfo.authorName;
                    }
                    if (t.gameObject.name == "SongName")
                    {
                        t.gameObject.GetComponent<Text>().text = songTwelveNoteInfo.songName;
                    }
                    if (t.gameObject.name == "noteCount")
                    {
                        t.gameObject.GetComponent<Text>().text = "12";
                    }
                    if (t.gameObject.name == "Cover")
                    {
                        StartCoroutine(GetImage(t.gameObject, newFileLoc + "/" + songTwelveNoteInfo.coverImagePath));
                    }
                }
                TwlevelNoteIndex++;
            }
        }
        loaded = true;
        yield return null;
    }

    Info LoadsongData(string loc)
    {
        var info = File.ReadAllText(loc);
        return JsonConvert.DeserializeObject<Info>(info);
    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {

        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        return newAudio;

    }

    public static void setSong()
    {
        var regSprite = Resources.Load<Sprite>("Graphics/Trans");

        Transform[] ts = GameObject.Find("songContent").transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts)
        {
            Transform[] ts1 = t.transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform t1 in ts1)
            {
                if (t1.gameObject.name == "selected")
                {
                    Image realmSelect = t1.gameObject.GetComponent<Image>();
                    realmSelect.sprite = regSprite;
                }
            }
        }
    }

    IEnumerator GetImage(GameObject coverImg, string location)
    {
        Image tmp = coverImg.GetComponent<Image>();
        WWW www = new WWW(location);
        yield return www;
        tmp.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
    public bool loaded = false;
    void Update()
    {
        if (loaded)
        {
            if (controller.isValid)
            {
                device = SteamVR_Controller.Input((int)controller.index);
                if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

                    if (touchpad.y < -0.2f)
                    {
                        gameObject.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition -= 0.005f;
                    }

                    if (touchpad.y > 0.2f)
                    {
                        gameObject.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition += 0.005f;
                    }
                }
            }
        }
    }
}
