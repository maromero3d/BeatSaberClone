using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwelveNoteCube : MonoBehaviour {
    
    public int noteIndex = 0;
    public NoteData note;
    Vector3 hardScale;
    Vector3 StartPos;
    Vector3 SecondStart;
    Vector3 newPos;

    bool canStep = true;
    bool canStepTwo = false;
    bool canStepFinal = true;

    GameModule game;

    // Use this for initialization
    void Start () {
        game = GameObject.Find("GameModule").GetComponent<GameModule>();
        StartPos = transform.position;

        newPos = GameObject.Find(note._lineLayer.ToString() + note._lineIndex.ToString()).transform.position;

        Transform[] ts = transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts)
        {
            if (t.gameObject.name == "Box")
            {
                hardScale = t.gameObject.transform.localScale / 2;
                StartCoroutine(RotateMe(Vector3.forward * GetNoteRotation(note), 2f, t.gameObject));
            }
        }
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime, GameObject obj)
    {
        //Box = obj;
        var fromAngle = obj.transform.rotation;
        var toAngle = Quaternion.Euler(obj.transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            obj.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    float GetNoteRotation(NoteData note)
    {
        float tmp = 0;
        switch (note._cutDirection)
        {
            case _cutType._bottomLeft:
                tmp = 135f;
                break;
            case _cutType._bottomRight:
                tmp = 225;
                break;
            case _cutType._down:
                tmp = 0f;
                break;
            case _cutType._left:
                tmp = 90f;
                break;
            case _cutType._right:
                tmp = 280f;
                break;
            case _cutType._topLeft:
                tmp = 45f;
                break;
            case _cutType._topRight:
                tmp = 315f;
                break;
            case _cutType._up:
                tmp = 180f;
                break;

        }
        return tmp;
    }

    // Update is called once per frame
    void Update () {
        if (canStep)
            transform.position = Vector3.Lerp(StartPos, newPos, ((1 * TwelveNoteGame.BeatPerSec) - ((float)note._time - ((TwelveNoteGame.songPosInBeats - difficultyContent.selectedTwelveNoteChart.difficultyLevels[0].offset) + (3 * TwelveNoteGame.BeatPerSec)))) / (1 * TwelveNoteGame.BeatPerSec));

        if (canStepTwo)
            transform.position = Vector3.Lerp(newPos, new Vector3(newPos.x, newPos.y, game.wall.transform.position.z), ((4 * TwelveNoteGame.BeatPerSec) - ((float)note._time - ((TwelveNoteGame.songPosInBeats - difficultyContent.selectedTwelveNoteChart.difficultyLevels[0].offset)) + (1 * TwelveNoteGame.BeatPerSec))) / (4 * TwelveNoteGame.BeatPerSec));

        if (transform.position.z <= newPos.z && canStepFinal)
        {
            addFlame();
            canStep = false;
            canStepTwo = true;
            canStepFinal = false;
        }
        
        transform.LookAt(new Vector3(game.wall.transform.position.x, transform.position.y, game.wall.transform.position.z));
    }

    void addFlame()
    {
        string type = "RedFire";
        /*switch(note._type)
        {
            case Hand.blue:
                type = "BlueFire";
                break;
            case Hand.red:
                type = "RedFire";
                break;
            case Hand.Bomb:
                type = "RedFire";
                break;
        }
        GameObject _explosion = Instantiate(Resources.Load("Prefabs/" + type) as GameObject);
        _explosion.transform.localScale = new Vector3(2f, 7f, 2f);
        _explosion.transform.position = newPos;
        _explosion.name = type;
        _explosion.AddComponent<Trash>();*/
    }
}
