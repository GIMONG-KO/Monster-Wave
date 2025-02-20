using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image imageUI;

    void Start()
    {
        ExampleTimer.onGameStart += OnUnFade;
        ExampleTimer.onGameOver += OnFade;
    }
    
    public void OnFade()
    {
        Debug.Log("화면이 어두워지는 기능");
    }

    public void OnUnFade()
    {
        Debug.Log("화면이 밝아지는 기능");
    }
}