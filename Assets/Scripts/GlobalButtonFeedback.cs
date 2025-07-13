using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GlobalButtonFeedback : MonoBehaviour
{
    [System.Obsolete]
    void Start()
    {
        Button[] buttons = FindObjectsOfType<Button>(true); // true = يشوف حتى لو الزر مش مفعل

        foreach (Button button in buttons)
        {
            if (button.gameObject.GetComponent<ButtonFeedback>() == null)
            {
                button.gameObject.AddComponent<ButtonFeedback>();
            }
        }
    }
}
