using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class TouchControl : MonoBehaviour,IBeginDragHandler,IDragHandler
{
    public static UnityAction<Vector2Int,Side> OnSwipe;
    public void OnDrag(PointerEventData eventData)
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2Int number =GetButtonNumber();
        string name = GetButtonName();
        if(Mathf.Abs(eventData.delta.x)>Mathf.Abs(eventData.delta.y))
        {
            if(eventData.delta.x>0)
            {
                //Debug.Log($"{name}  {number}  Swipe right");
                OnSwipe?.Invoke(number,Side.Right);
            }
            else
            {
                //Debug.Log($"{name}  {number}  Swipe left");
                OnSwipe?.Invoke(number,Side.Left);
            }
                

        }
        else if(Mathf.Abs(eventData.delta.x)<Mathf.Abs(eventData.delta.y))
        {
            if(eventData.delta.y>0)
            {
                //Debug.Log($"{name}  {number}  Swipe up");
                OnSwipe?.Invoke(number,Side.Up);

            }
            else
            {
                //Debug.Log($"{name}  {number}  Swipe down");
                OnSwipe?.Invoke(number,Side.Down);
            }
        }
    }

    private Vector2Int GetButtonNumber()
    {
        Regex reg = new Regex("\\((\\d+)\\)");
        Match match = reg.Match(GetButtonName());
        if(!match.Success)
            throw new Exception("Unrecognized object name");
            Group group =match.Groups[1];
        
        string number = group.Value;
        int value = Convert.ToInt32(number);
        Vector2Int current =new Vector2Int();
        current.y = value % Lines.SIZE;
        current.x = value / Lines.SIZE;
        return current;
    }
    private string GetButtonName()
    {
        return EventSystem.current.currentSelectedGameObject.name;
    }

}
