using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeDelate : MonoBehaviour
{
    public Text text;
    public int TotalTime = 60;

    void Start()
    {
       
    }

    void update()
    {
        if (Move.fail)
        {
            //Move.fail = false;
            Move.stop = true;
            text.fontSize = 50;
            StartCoroutine(Time());
            text.fontSize = 0;
            TotalTime = 4;
            Move.stop = false;
        }
    }

    IEnumerator Time()
    {
        while (TotalTime >= 0)
        {
            if (TotalTime == 4)
            {
                text.text = "READY?";
            }
            else if (TotalTime == 0)
            {
                text.text = "GO!";
            }
            else
            {
                text.text = TotalTime.ToString();
            }
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
    }

}
