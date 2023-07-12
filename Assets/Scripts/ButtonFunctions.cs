using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    Button btStart, btToMenu, btSkip, btRetry,btExit,btContinue,btToMenu2,btPause,btCtn;
    Transform newGameTransform, ball;
    Text text,theme;
    public static bool retryContinued = false;
    int totalTime = 4;
    GameObject pausing;
    // Start is called before the first frame update
    void Start()
    {
        theme = GameObject.Find("Theme").GetComponent<Text>();
        text = GameObject.Find("Text").GetComponent<Text>();
        btStart = GameObject.Find("Start").GetComponent<Button>();
        btToMenu = GameObject.Find("ToMenu").GetComponent<Button>();
        btToMenu2 = GameObject.Find("ToMenu2").GetComponent<Button>();
        btExit = GameObject.Find("Exit").GetComponent<Button>();
        btSkip = GameObject.Find("Skip").GetComponent<Button>();
        btContinue = GameObject.Find("Continue").GetComponent<Button>();
        btRetry = GameObject.Find("Retry").GetComponent<Button>();
        btPause = GameObject.Find("Pause").GetComponent<Button>();
        btCtn = GameObject.Find("Continue").GetComponent<Button>();
        ball = GameObject.Find("AllBall").transform;
        newGameTransform = GameObject.Find("newGame").transform;
        pausing = GameObject.Find("Pausing");

        theme.transform.localScale = new Vector3(2, 2, 2);
        btSkip.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //print("stop:" + Move.stop + " stop1:" + Move.stop1);
        /*if (!Move.newGame)
        {
            btSkip.transform.localScale = new Vector3(0, 0, 0);
        }*/
        /*else
        {
            btSkip.transform.localScale = new Vector3(1, 1, 0);
        }*/
    }
    public void gameStart()
    {
        Move.state = "solid";
        Move.stop = false;
        Move.stop1 = false;
        Move.zSpeed = 10;
        theme.transform.localScale = new Vector3(0, 0, 0);
        btPause.transform.localScale = new Vector3(1, 1, 1);
        btStart.transform.localScale = new Vector3(0, 0, 0);
        btToMenu.transform.localScale = new Vector3(0, 0, 0);
        //btToMenu2.transform.localScale = new Vector3(0, 0, 0);
        //btContinue.transform.localScale = new Vector3(0, 0, 0);
        btExit.transform.localScale = new Vector3(0, 0, 0);
        if (Move.newGame)
        {
            btSkip.transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            btSkip.transform.localScale = new Vector3(0, 0, 0);
        }
        //btPause.transform.localScale = new Vector3(1, 1, 1);
    }

    public void gameExit()
    {
        Application.Quit();
    }

    public void toMenu()
    {
        Move.stop = false;
        Move.stop1 = false;
        Move.newGame = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void skipTutorial()
    {
        /* Debug.Log(ball);
         Debug.Log(newGameTransform);*/
        GenerateRoad.startGenerate = true;
        ball.position = new Vector3(ball.position.x, ball.position.y, newGameTransform.position.z + 10f);
        text.text = " ";
        btSkip.transform.localScale = new Vector3(0, 0, 0);
        Move.newGame = false;
        gameStart();
    }

    public void retry()
    {
        StartCoroutine(retry2());
    }
    IEnumerator retry2()
    {
        btRetry.transform.localScale = new Vector3(0, 0, 0);
        //ButtonFunctions.retryContinued = false;
        while (totalTime >= 0)
        {
            if (totalTime == 4)
            {
                text.text = "READY?";
            }
            else if (totalTime == 0)
            {
                text.text = "GO!";
            }
            else
            {
                text.text = totalTime.ToString();
            }
            Debug.Log(totalTime + " text:" + text.text);
            yield return new WaitForSeconds(1);
            totalTime--;
        }
        text.text = " ";
        Move.stop = false;
        Move.stop1 = false;
        totalTime = 4;
    }

    public void gameCtn()
    {
        btContinue.transform.localScale = new Vector3(0, 0, 0);
        Move.stop1 = false;
        Move.stop1 = false;
        text.text = " ";
    }

    public void pause()
    {
        btPause.transform.localScale = new Vector3(0, 0, 0);
        pausing.transform.localScale = new Vector3(10, 6, 0);
        Time.timeScale = 0;
    }

    public void ctn()
    {
        btPause.transform.localScale = new Vector3(1, 1, 1);
        pausing.transform.localScale = new Vector3(0, 0, 0);
        Time.timeScale = 1;
    }

}
