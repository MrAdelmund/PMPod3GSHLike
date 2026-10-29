using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuControler : MonoBehaviour
{
    [SerializeField] Animator animatorForAnimation;
    static bool pauseMenuCanOpen = true;
    bool toggeledOn = false;
    static CanvasGroup canvasGroup;
    void Start()
    {
        //component retrieval
        canvasGroup = GetComponent<CanvasGroup>();
        animatorForAnimation.enabled = false;
        //makes canvas not interactable
        DisableCanvasInteraction();
    }
    void Pause()
    {
        Time.timeScale = 0;
        //toggles on menu animation
        animatorForAnimation.enabled = true;
        //makes canvas interactable
        EnableCanvasInteraction();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        //toggles off menu animation
        animatorForAnimation.enabled = false;
        //resets bool to keep stuff synced
        toggeledOn = false;
        //makes canvas not interactable
        DisableCanvasInteraction();
    }
    //disables interaction and makes canvas transparent
    void DisableCanvasInteraction()
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    //enables interaction and makes canvas visable
    void EnableCanvasInteraction()
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    //These are functions that can be called from anywhere without an object
    //reference to disable/enable the player's ability to open the pause menu.
    public static void DisablePauseMenuOpening()
    {
        pauseMenuCanOpen = false;
    }
    public static void EnablePauseMenuOpening()
    {
        pauseMenuCanOpen = true;
    }
    //updater for input
    public void OnPauseMenuToggle(InputValue Value)
    {
        //gets called when player presses pause button, and flips the current state of the menu
        if (pauseMenuCanOpen)
        {
            if (Value.isPressed)
                toggeledOn = !toggeledOn;
            if (toggeledOn)
                Pause();
            else
                Resume();
        }
    }
}
