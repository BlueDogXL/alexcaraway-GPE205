using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToCredits : MonoBehaviour
{
    public void ChangeToCreditsMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateCreditsScreen();
        }
    }
}
