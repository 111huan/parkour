using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Retry : MonoBehaviour
{
    public Transform ball;
    public Text text;
    public int TotalTime = 4;
    public static bool ballFixed = false;

    void Update()
    {
        if (Move.fail)
        {
            retry();
        }
        if (TotalTime < 0)
        {
            Move.stop = false;
            text.text = " ";
        }
    }

    void retry()
    {
        ballFixed = true;
        Move.stop = true;
        //²¥·ÅËÀÍö¶¯»­
        ball.position = new Vector3(ball.position.x, ball.position.y, ball.position.z - 10);
        Move.fail = false;
        StartCoroutine(Time());
    }
    IEnumerator Time()
    {
        text.fontSize = 20;
        TotalTime = 4;
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
