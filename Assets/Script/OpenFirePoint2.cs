using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenFirePoint2 : MonoBehaviour
{
    public Button ButtonToggle;
    public Sprite FirePoint2;
    public Sprite FirePoint2_OpenFirePoint2;
    private int counter = 0;

    // Use this for initialization
    void Start()
    {
        ButtonToggle = GetComponent<Button>();
    }

    public void changeButton()
    {
        counter++;
        if (counter % 2 == 0)
        {
            ButtonToggle.image.overrideSprite = FirePoint2;
        }
        else
        {
            ButtonToggle.image.overrideSprite = FirePoint2_OpenFirePoint2;
        }
    }
}
