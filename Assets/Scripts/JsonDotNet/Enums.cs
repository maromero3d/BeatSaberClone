public enum BeatSaberEventType
{
    BackTopLazer = 0,
    TrackRingLazer = 1,
    LeftLazer = 2,
    RightLazer = 3,
    BackBottom = 4,

    TunnelRotation = 8,
    TunnelZooming = 9,

    LeftLazerSpeed = 12,
    RightLazerSpeed = 13

}

public enum BeatSaberEventColorType
{
    LightEffect0 = 0,
    LightEffect1 = 1,
    LightEffect2 = 2,
    Bluefade = 3,
    LightEffect4 = 4,

    RedFade = 7,
    TunnelZooming = 9,

    MoveLight2 = 12,
    MoveLight3 = 13

}

public enum _cutType
{
    _up = 0,
    _down = 1,
    _left = 2,
    _right = 3,
    _bottomLeft = 4,
    _bottomRight = 5,
    _topLeft = 6,
    _topRight = 7,
    _any = 8
}

public enum Hand
{
    red = 0,
    blue = 1,
    Bomb = 3
}

public enum HorizontalPosition
{
    Left = 0,
    CenterLeft = 1,
    CenterRight = 2,
    Right = 3
}

public enum VerticalPosition
{
    Bottom = 0,
    Middle = 1,
    Top = 2
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Expert
}

public enum DifficultyForFiveNote
{
    EasyGuitar,
    MediumGuitar,
    HardGuitar,
    ExpertGuitar,
    EasyDrums,
    MediumDrums,
    HardDrums,
    ExpertDrums
}

public enum EnvironmentType
{
    DefaultEnvironment = 0,
    TriangleEnvironment = 1,
    BigMirrorEnvironment = 2,
    NiceEnvironment = 3,
}