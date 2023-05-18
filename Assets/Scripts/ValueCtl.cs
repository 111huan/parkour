using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueCtl : MonoBehaviour
{
    Slider slider, slider1;
    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        scrollCtl();
        keyBoardCtl();
        slider1Ctl();
    }

    void scrollCtl()
    {
        slider.value += Input.GetAxis("Mouse ScrollWheel");
        slider1.value += Input.GetAxis("Mouse ScrollWheel");
    }

    void keyBoardCtl()
    {
        if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            slider.value += 0.006f;
            slider1.value = slider.value;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            slider.value -= 0.006f;
            slider1.value = slider.value;
        }
    }

    void slider1Ctl()
    {
        slider.value = slider1.value;
    }
}
