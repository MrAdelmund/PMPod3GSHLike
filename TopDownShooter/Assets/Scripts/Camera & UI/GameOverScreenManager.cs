using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField] GameObject selectedButtonOnStart;
    int levelToLoad;
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(selectedButtonOnStart);
        levelToLoad = PlayerPrefs.GetInt("LevelPlayerWasIn");
    }
    //loads the level the player came from when they died
    public void Yes()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    //loads main menu
    public void No()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
