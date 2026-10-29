using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectToggler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textToToggle;
    public void CheckIfYouShouldBeEnabled()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
            textToToggle.enabled = true;
        else
            textToToggle.enabled = false;
    }
}
