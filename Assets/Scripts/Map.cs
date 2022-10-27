using System;
using System.Linq;
using UnityEngine;
public class Map 
{
    private int[,] _map;
    private ShowBox _showBox;
    public Map(ShowBox showBox)
    {
        _map = new int[Lines.SIZE,Lines.SIZE];
        _showBox=showBox;
    }
   public void ClearMap()
   {
       for(int i=0;i<Lines.SIZE;i++)
        for(int j=0;j<Lines.SIZE;j++)
            SetMap(i,j,0);
   }

   public void AddBalls()
   {
       int [] balls ={1,1,1,1,1,2,2,2,2,2,3,3,3,3,3};
       int indexBalls=0;
       Shuffle(ref balls);
       for(int i=0;i<Lines.SIZE;i=i+2)
       {
            for(int j=0;j<Lines.SIZE;j++)
            {
                SetMap(j,i,balls[indexBalls]); 
                indexBalls++;   
            }
       }
       SetMap(1,1,4);
       SetMap(1,3,4);
       SetMap(3,1,4);
       SetMap(3,3,4);
   }

    public void SetMap(int x,int y,int ball)
   {
       _map[x,y]=ball;
       _showBox?.Invoke(new Vector2Int(x,y),ball);
       
   }

   public bool TryMoving(int x,int y,Side side)
   {    
        int x2=0,y2=0;
        switch(side)
        {
            case Side.Up:
                x2=x-1;
                y2=y;
                break;
            case Side.Down:
                x2=x+1;
                y2=y;
                break;
            case Side.Right:
                x2=x;
                y2=y+1;
                break;
            case Side.Left:
                x2=x;
                y2=y-1;
                break;
        }
        if(OnMap(x2,y2) && _map[x2,y2]==0)
        {
            SetMap(x2,y2,_map[x,y]);
            SetMap(x,y,0);
            return true;
        }
        return false;
   }

    private bool OnMap(int x, int y)
    {
        return x>=0 && x<Lines.SIZE 
                && y>=0 && y<Lines.SIZE;
    }

    public void CheckingCompletion(LevelComplete OnCompleted)
    {
        for(int i=0;i<Lines.SIZE;i++){
            if(_map[i,0]!=1)return;
            Debug.Log(_map[0,i] + ": "+0 +" "+ i);
        }
        for(int i=0;i<Lines.SIZE;i++){
            if(_map[i,2]!=2)return;
            Debug.Log(_map[0,i] + ": "+0 +" "+ i);
        }
        for(int i=0;i<Lines.SIZE;i++){
            if(_map[i,4]!=3)return;
            Debug.Log(_map[0,i] + ": "+0 +" "+ i);
        }

        OnCompleted?.Invoke();
    }
    private void Shuffle(ref int []arr)
    {
            System.Random random = new System.Random();
            arr = arr.OrderBy(x => random.Next()).ToArray();
    }


}
