using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public LevelState levelState = LevelState.Start;
    public GameObject finishScreen, startButton;
    public Text text;
    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.F11))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void StartButton()
    {
        levelState = LevelState.Play;
        startButton.SetActive(false);
    }
    public void Finish(string name)
    {
        finishScreen.SetActive(true);
        levelState = LevelState.Finish;
        text.text= name + "Kazandý";
    }
    public enum LevelState{
        Start,
        Play,
        Finish
    }
}
