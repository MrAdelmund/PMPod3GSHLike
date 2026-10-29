using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPickup : MonoBehaviour
{
    public bool randomWeapon = true;
    [Header("Only used if not set to random")]
    [Tooltip ("0 is normal, 1 is lightning, 2 is fire, 3 is chaser")]
    public int gunIndex;
    [SerializeField] Sprite[] gunTypeSprites;
    void Start()
    {
        var gun1 = GetComponent<SpriteRenderer>();
        if (randomWeapon)
        {
            int randomNum = Random.Range(0, gunTypeSprites.Length);
            gun1.sprite = gunTypeSprites[randomNum];
            gunIndex = randomNum;
        }
        else
        {
            //sets the rendered sprited of object based off of assinged gun index
            if (gunIndex > gunTypeSprites.Length)
                Debug.LogWarning("Gun index is set to number outside bounds of array. Please fix, this will cause issues");
            else
                gun1.sprite = gunTypeSprites[gunIndex];
        }
        
    }
}
