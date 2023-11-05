using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{

    //static since multiple scripts will use this
    [SerializeField] private float timeValue = 0;
    [SerializeField] TextMeshProUGUI label;
    // Start is called before the first frame update
    void Start()
    {
        label.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        timeValue += Time.deltaTime;
        DisplayTime(timeValue);
    }

    void DisplayTime(float displayTime)
	{
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        label.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
