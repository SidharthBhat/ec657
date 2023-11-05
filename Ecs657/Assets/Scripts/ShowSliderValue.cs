using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowSliderValue : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    public string textName;

    // Start is called before the first frame update
    void Start()
    {
        text.text = textName + ": " + Mathf.Round(slider.value * 100f) / 100f;
    }

    //changes text value if slider ever changes valuea
    public void updateText()
	{
        //rounds to 2 sig fig
        text.text = textName + ": " + Mathf.Round(slider.value * 100f) / 100f;
    }
}
