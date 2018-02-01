using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenFirePoint1 : MonoBehaviour {
    public Button FirePoint1Button;
    public Sprite FirePoint1;
    public Sprite FirePoint1_OpenFirePoint1;
    private int counter = 0;

	// Use this for initialization
	void Start () {
        FirePoint1Button = GetComponent<Button>();
	}
	
	public void changeButton()
    {
        counter++;
        if (counter % 2==0)
        {
            FirePoint1Button.image.overrideSprite = FirePoint1;
        }
        else
        {
            FirePoint1Button.image.overrideSprite = FirePoint1_OpenFirePoint1;
        }
    }
}
