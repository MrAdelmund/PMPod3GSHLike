using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] int sceneToLoad = 2;
    [Header("References")]
    [SerializeField] CanvasGroup startCanvasGroup;
    [SerializeField] CanvasGroup weaponSelectCanvasGroup;
    [SerializeField] GameObject defaultSelectedButton;
    [SerializeField] Sprite[] gunSprites;
    [SerializeField] Image SelectGunUIImage;
    GameObject lastSelectedObject;
    void Start()
    {
        DisableCanvasInteraction(weaponSelectCanvasGroup);
        EnableCanvasInteraction(startCanvasGroup);
        lastSelectedObject = defaultSelectedButton;
        //makes sure that canvases are enabled
        startCanvasGroup.gameObject.GetComponent<Canvas>().enabled = true;
        weaponSelectCanvasGroup.gameObject.GetComponent<Canvas>().enabled = true;
        PlayerPrefs.SetInt("StartGunIndex", -1);
    }
    private void FixedUpdate()
    {
        if (EventSystem.current.currentSelectedGameObject != lastSelectedObject)
        {
            lastSelectedObject = EventSystem.current.currentSelectedGameObject;
            BroadcastMessage("CheckIfYouShouldBeEnabled");
        }
    }
    public void LoadScene()
    {
        if (PlayerPrefs.GetInt("StartGunIndex") != -1)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    public void SetSelectedWeapon(int weaponToSelect)
    {
        PlayerPrefs.SetInt("StartGunIndex", weaponToSelect);
        SelectGunUIImage.sprite = gunSprites[weaponToSelect];
    }
    //disables interaction and makes canvas transparent
    void DisableCanvasInteraction(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    //enables interaction and makes canvas visable
    void EnableCanvasInteraction(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public void OpenWeaponSelectMenu()
    {
        EnableCanvasInteraction(weaponSelectCanvasGroup);
        DisableCanvasInteraction(startCanvasGroup);
        EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
    }
}
