using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class BeatSaberNoteInfo
{ 
    public string _version { get; set; }
    public long _beatsPerMinute { get; set; }
    public long _beatsPerBar { get; set; }
    public uint _noteJumpSpeed { get; set; }
    public uint _shuffle { get; set; }
    public double _shufflePeriod { get; set; }
    public List<BeatSaberEvents> _events { get; set; }
    public List<NoteData> _notes { get; set; }
    public List<ObstacleData> _obstacles { get; set; }
}

public class NoteData
{
    public double _time { get; set; }
    public uint _lineIndex { get; set; }
    public uint _lineLayer { get; set; }
    public Hand _type { get; set; }
    public _cutType _cutDirection { get; set; }
}

public class ObstacleData
{
    public float _time { get; set; }
    public uint _lineIndex { get; set; }
    public uint _type { get; set; }
    public float _duration { get; set; }
    public uint _width { get; set; }
}

public class Info
{
    public string path { get; set; }
    public AudioClip audioClip { get; set; }
    public string songName { get; set; }
    public string songSubName { get; set; }
    public string authorName { get; set; }
    public long beatsPerMinute { get; set; }
    public long previewStartTime { get; set; }
    public long previewDuration { get; set; }
    public string coverImagePath { get; set; }
    public string environmentName { get; set; }
    public List<DifficultyLevels> difficultyLevels { get; set; }
}

public class BeatSaberEvents
{
    public float Time { get; set; }
    public BeatSaberEventType Type { get; set; }
    public int Value { get; set; }
}

public class DifficultyLevels
{
    public string difficulty { get; set; }
    public uint difficultyRank { get; set; }
    public string audioPath { get; set; }
    public string jsonPath { get; set; }
    public long offset { get; set; }
    public long oldOffset { get; set; }
}