using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Retry : MonoBehaviour
{
    public Transform ball;
    public TextMesh text;
    public int TotalTime = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        Move.stop = true;
        //������������
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
