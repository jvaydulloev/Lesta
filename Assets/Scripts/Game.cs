using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]private GameObject _parentButtons;
    [SerializeField]private GameObject _parentBalls;
    [SerializeField]private GameObject _UI; 
    [SerializeField]private AudioSource _sound;
    Button [] _buttons;
    Image[] _balls;
    
    private Lines _lines;
    // Start is called before the first frame update
    void Start() 
    {
        _lines = new Lines(ShowBox,PlayCut,LevelComplete);
        InitButtons();
        InitImages();   
    }
    public void StartGame()
    {
        _UI.SetActive(false);
        _lines.Start();
    }
    public  void ShowBox(Vector2Int number,int ball)
    {
        _buttons[number.x*Lines.SIZE+number.y].transform.GetChild(0).GetComponent<Image>().sprite = _balls[ball].GetComponent<Image>().sprite;
    }
    public  void PlayCut()
    {
        _sound.Play();
    }
    public void LevelComplete()
    {
        _UI.SetActive(true);
        Time.timeScale=0;
        Debug.Log("Level Complete");
    }
    private void InitButtons()
    {
        _buttons =new Button[Lines.SIZE*Lines.SIZE];
        _buttons =_parentButtons.GetComponentsInChildren<Button>();
    }
    private void InitImages()
    {
        _balls =new Image[Lines.BALLS];
        _balls = _parentBalls.GetComponentsInChildren<Image>();
    }

}
