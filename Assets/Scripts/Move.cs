using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class Move : MonoBehaviour
{
    public static string state = "solid";
    public static bool newGame = true;
    Rigidbody rb;
    Text text,score;
    Slider slider;
    float gasSpeed = 5;
    float zSpeed = 10;
    bool unfixed = true;
    public static bool fail = false, success = false, stop = true, stop1 = true;
    [SerializeField] Transform solidObj, liquidObj, gasObj;
    Vector3 solidOn, liquidOn, gasOn;
    int totalTime = 4, nowScore = 0,bestScore;
    Button  bt2,skip,ctn;
    /*TextAsset txt;
    string path;
    FileStream file;
    byte[] bts;*/
    void Start()
    {
        
        bestScore = PlayerPrefs.GetInt("bestScore");
        print(bestScore);
        //slider = GameObject.Find("Slider").GetComponent<Slider>();
        text = GameObject.Find("Text").GetComponent<Text>();
        score = GameObject.Find("ScoreBoard").GetComponent<Text>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, zSpeed);
        solidOn = solidObj.localScale;
        liquidOn = liquidObj.localScale;
        gasOn = gasObj.localScale;
        //bt1 = GameObject.Find("Retry").GetComponent<Button>();
        bt2 = GameObject.Find("ToMenu").GetComponent<Button>();
        skip = GameObject.Find("Skip").GetComponent<Button>();
        ctn = GameObject.Find("Retry").GetComponent<Button>();
        /*txt = Resources.Load("Record") as TextAsset;*/
        /*path = Application.dataPath + "/Resources/Record.txt";
        file = new FileStream(path, FileMode.Create);*/
    }

    // Update is called once per frame
    void Update()
    {
        stateCtl();
        speedCtl();
        scoreBoard();
        //print("stop:" + stop + " stop1:" + stop1 + " rb.v:" + rb.velocity);
    }

    private void OnDisable()
    {
        /*if (file != null)
        {
            //清空缓存
            file.Flush();
            // 关闭流
            file.Close();
            //销毁资源
            file.Dispose();
        }*/
    }
    void scoreBoard()
    {
        if (nowScore/2 >= bestScore)
        {
            bestScore = nowScore/2;
        }
        score.text = "current score：" + nowScore/2 + " obstacles\nbest record："+bestScore+" obstacles";
        /*bts = System.Text.Encoding.UTF8.GetBytes(bestScore.ToString());
        file.Write(bts, 0, bts.Length);*/
        PlayerPrefs.SetInt("bestScore", bestScore);
    }
    void speedCtl()
    {
        //Debug.Log(rb.velocity);
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        if (!newGame)
        {
            zSpeed += Time.deltaTime * 0.5f;
        }
        if (stop)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, zSpeed);
            if (state != "gas" && transform.position.y <= 5.2)
            {
                rb.velocity = new Vector3(0, 0, zSpeed);
            }

            if (state == "gas")
            {
                gasMove();
            }
            if (transform.position.y >= 8.5)
            {
                rb.velocity = new Vector3(0, 0, zSpeed);
            }
        }
    }
    void stateCtl()
    {
        if (Input.GetKeyDown("1"))
        {
            state = "solid";        }
        else if (Input.GetKeyDown("3"))
        {
            state = "gas";
        }
        else if(Input.GetKeyDown("2"))
        {
            state = "liquid";
        }
        if (state == "solid")
        {
            solidObj.localScale = solidOn;
            liquidObj.localScale = new Vector3(0, 0, 0);
            gasObj.localScale = new Vector3(0, 0, 0);
            rb.useGravity = true;
            /*if (transform.position.y > 5.4)
            {
                transform.position = new Vector3(transform.position.x, 5.4f, transform.position.z);
            }*/
        }
        if (state == "liquid")
        {
            solidObj.localScale = new Vector3(0, 0, 0);
            liquidObj.localScale = liquidOn;
            gasObj.localScale = new Vector3(0, 0, 0);
            rb.useGravity = true;
        }
        if(state == "gas")
        {
            solidObj.localScale = new Vector3(0, 0, 0);
            liquidObj.localScale = new Vector3(0, 0, 0);
            gasObj.localScale = gasOn;
            rb.useGravity = false;
        }
    }

    void gasMove()
    {
        if (!stop)
        {
            if (transform.position.y < 8)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + gasSpeed * Time.deltaTime, transform.position.z);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, zSpeed);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "drain" && state != "liquid")
        {
            stop = true;
        }
        else if (other.gameObject.tag == "door" && state != "solid")
        {
            stop = true;
        }
        else if(stop1)
        {
            stop = true;
        }
        else
        {
            stop = false;
        }
    }

    IEnumerator retry()
    {
        print("move.retry()");
        stop = true;
        stop1 = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 8);
        text.fontSize = 18;
        if(state == "solid")
        {
            text.text = "solid cannot pass this obstacle!";
        }
        else if(state == "liquid")
        {
            text.text = "liquid cannot pass this obstacle!";
        }
        else if (state == "gas")
        {
            text.text = "gas cannot pass this obstacle!";
        }
        ctn.transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }

    
    void gameFail()
    {
        stop = true;
        stop1 = true;
        text.text = "YOU FAIL...";
        //bt1.transform.localScale = new Vector3(1, 1, 1);
        bt2.transform.localScale = new Vector3(1, 1, 1);
    }

    void promptCtl(string promptName)
    {
        if(promptName == "Prompt1")
        {
            text.text = "click the buttons to control the ball's state";
            Invoke("delay",3);
        }
        else if(promptName == "Prompt2")
        {
            text.text = "change to fluid to pass";
            Invoke("delay", 3);
        }
        else if (promptName == "Prompt3")
        {
            text.text = "do not try to pass by fluid";
            Invoke("delay", 3);
        }
        else if (promptName == "Prompt4")
        {
            text.text = "change to gas to pass";
            Invoke("delay", 3);
        }
        else if (promptName == "Prompt5")
        {
            text.text = "do not try to pass by gas";
            Invoke("delay", 3);
        }
        else if (promptName == "Prompt6")
        {
            text.text = "change to solid to pass";
            Invoke("delay", 3);
        }
        else if (promptName == "Prompt7")
        {
            text.text = "do not try to pass by solid";
            Invoke("delay", 3);
        }
    }
    
    void delay()
    {
        text.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="out" && !newGame)
        {
            nowScore++;
        }
        if (other.gameObject.tag == "wind" && state == "gas")
        {
            if (newGame)
            {
                StartCoroutine(retry());
            }
            else
            {
                //fail = true;
                gameFail();
            }
        }
        if (other.gameObject.tag == "thorn" && state == "solid")
        {
            if (newGame)
            {
                StartCoroutine(retry());
            }
            else
            {
                //fail = true;
                gameFail();
            }
        }
        if (other.gameObject.tag == "DrainFloor" && state == "liquid")
        {
            if (newGame)
            {
                StartCoroutine(retry());
            }
            else
            {
                //fail = true;
                gameFail();
            }
        }
        if (other.gameObject.name == "newGame")
        {
            newGame = false;
            //zSpeed += 5;
        }
        if (other.gameObject.tag == "prompt")
        {
            promptCtl(other.name);
        }
    }
}
