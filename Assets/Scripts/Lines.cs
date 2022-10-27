using System;
using UnityEngine;
public enum Side
{
    Up,
    Down,
    Right,
    Left

};

public delegate void ShowBox(Vector2Int number,int ball);
public delegate void PlayCut();

public delegate void LevelComplete();

public class Lines
{
    public const int SIZE = 5;
    public const int BALLS = 3;
    private ShowBox _showBox;
    private PlayCut _playCut;
    private LevelComplete _levelComplete;
    private Map _map;

    public Lines (ShowBox showBox,PlayCut playCut,LevelComplete levelComplete)
    {
        _showBox= showBox;
        _playCut= playCut;
        _levelComplete = levelComplete;
        TouchControl.OnSwipe += Swipe;
        _map= new Map(_showBox);
    }
    ~Lines()
    {
       TouchControl.OnSwipe -= Swipe; 
    }
    public void Start()
    {   
        _map.ClearMap();
        _map.AddBalls();
    }

    public void Swipe(Vector2Int number,Side side)
    {   
        if(_map.TryMoving(number.x,number.y,side))
        {
        _playCut?.Invoke();
        _map.CheckingCompletion(_levelComplete);
        }
    }
}
