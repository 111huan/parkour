using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Retry : MonoBehaviour
{
    public static GameObject ball;
    public Text text;
    public int TotalTime = 4;
    public static bool ballFixed = false;
    public Transform bt1, bt2;

    private void Start()
    {
        ball = GameObject.Find("AllBall");
        bt1 = GameObject.Find("Retry").transform;
        bt2 = GameObject.Find("ToMenu").transform;
        bt1.localScale = new Vector3(0, 0, 0);
        bt2.localScale = new Vector3(0, 0, 0);
    }
    void Update()
    {
        if (Move.fail&&Move.newGame)
        {
            retry();
           
        }
        if (TotalTime < 0.1)
        {
            Move.stop = false;
            text.text = " ";
        }
        /*if(Move.fail && !Move.newGame)
        {
            TotalTime = -1;
            gameFail();
        }*/
        
    }

    void retry()
    {
        ballFixed = true;
        Move.stop = true;
        //²¥·ÅËÀÍö¶¯»­
        ball.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z - 2);
        Move.fail = false;
        StartCoroutine(Time());
    }

    public void gameFail()
    {
        ballFixed = true;
        Move.stop = true;
        //Move.stop1 = true;
        text.text = "YOU LOSE...";
        bt1.localScale = new Vector3(0, 0, 0);
        bt2.localScale = new Vector3(0, 0, 0);
    }

    IEnumerator Time()
    {
        text.fontSize = 20;
        //TotalTime = 4;
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
