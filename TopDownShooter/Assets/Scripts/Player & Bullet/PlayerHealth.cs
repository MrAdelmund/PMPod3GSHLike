using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingVatality = 100;
    [SerializeField] float iFrameTimeGrantedWhenHit = 0.5f;
    [SerializeField] TextMeshProUGUI textToUpdate;
    static int currentLives = 0;
    float iFrames = 0;
    void Start()
    {
        //retrives Vitality from PlayerPrefs
        currentLives = PlayerPrefs.GetInt("Vitality");
        //sets vitality when starting new game/player starts a level and is low on healh
        if (currentLives <= startingVatality)
        {
            PlayerPrefs.SetInt("Vitality", startingVatality);
            currentLives = startingVatality;
        }
        textToUpdate.text = currentLives.ToString();
    }
    void Update()
    {
        iFrames -= Time.deltaTime;
    }
    //gets called by enemy hitbox/enemy bullet object
    public void TakeDamage(int amountOfDammage)
    {
        //dammages player if they do not have Iframes
        if (iFrames <= 0)
        {
            currentLives -= amountOfDammage;
            iFrames = iFrameTimeGrantedWhenHit;
            if (currentLives <= 0)
                GameOver();
        }
        textToUpdate.text = currentLives.ToString();
        PlayerPrefs.SetInt("LevelPlayerWasIn",SceneManager.GetActiveScene().buildIndex);
    }
    //gets called by health containers when picked up
    public void GainHealth(int amountOfHealthToGain)
    {
        currentLives += amountOfHealthToGain;
        textToUpdate.text = currentLives.ToString();
    }
    //gets called by menu when level is complete
    public static void UpdateSavedPlayerHealth()
    {
        PlayerPrefs.SetInt("Vitality", currentLives);
    }
    //tirggers game over screen
    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
