using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    Button btStart, btToMenu, btSkip, btRetry,btExit;
    Transform newGameTransform, ball;
    Text text;
    public static bool retryContinued = false;
    int totalTime = 4;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
        btStart = GameObject.Find("Start").GetComponent<Button>();
        btToMenu = GameObject.Find("ToMenu").GetComponent<Button>();
        //btPause = GameObject.Find("Pause").GetComponent<Button>();
        //btContinue = GameObject.Find("Continue").GetComponent<Button>();
        btExit = GameObject.Find("Exit").GetComponent<Button>();
        btSkip = GameObject.Find("Skip").GetComponent<Button>();
        btRetry = GameObject.Find("Retry").GetComponent<Button>();
        ball = GameObject.Find("AllBall").transform;
        newGameTransform = GameObject.Find("newGame").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameStart()
    {
        Move.stop = false;
        Move.stop1 = false;
        btStart.transform.localScale = new Vector3(0, 0, 0);
        btToMenu.transform.localScale = new Vector3(0, 0, 0);
        //btContinue.transform.localScale = new Vector3(0, 0, 0);
        btExit.transform.localScale = new Vector3(0, 0, 0);
        btSkip.transform.localScale = new Vector3(0, 0, 0);
        //btPause.transform.localScale = new Vector3(1, 1, 1);
    }

    public void gameExit()
    {
        Application.Quit();
    }

    public void toMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void skipTutorial()
    {
        /* Debug.Log(ball);
         Debug.Log(newGameTransform);*/
        GenerateRoad.startGenerate = true;
        ball.position = new Vector3(ball.position.x, ball.position.y, newGameTransform.position.z - 10f);
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
}
