using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{

    public float timeValue;
    [SerializeField] TextMeshProUGUI label;

    // Start is called before the first frame update
    void Start()
    {
        timeValue = 0;
        label.text = "00:00";
    }

	// Update is called once per frame
	void Update()
    {
        timeValue += Time.deltaTime;
        DisplayTime(timeValue);
    }

    //display time on text in minutes & seconds
    void DisplayTime(float displayTime)
	{
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        label.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
