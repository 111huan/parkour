using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public static string state = "solid";
    public static bool newGame = true;
    Rigidbody rb;
    Text text;
    Slider slider;
    float gasSpeed = 5;
    float zSpeed = 10;
    bool unfixed = true;
    public static bool fail = false, success = false, stop = true, stop1 = true;
    [SerializeField] Transform solidObj, liquidObj, gasObj;
    Vector3 solidOn, liquidOn, gasOn;
    int totalTime = 4;
    Button  bt2;
    void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        text = GameObject.Find("Text").GetComponent<Text>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, zSpeed);
        solidOn = solidObj.localScale;
        liquidOn = liquidObj.localScale;
        gasOn = gasObj.localScale;
        //bt1 = GameObject.Find("Retry").GetComponent<Button>();
        bt2 = GameObject.Find("ToMenu").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        stateCtl();
        speedCtl();
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
        if (slider.value<=0.3)
        {
            state = "solid";        }
        else if (slider.value>=0.7)
        {
            state = "gas";
        }
        else
        {
            state = "liquid";
        }
        if (state == "solid")
        {
            solidObj.localScale = solidOn;
            liquidObj.localScale = new Vector3(0, 0, 0);
            gasObj.localScale = new Vector3(0, 0, 0);
            rb.useGravity = true;
            if (transform.position.y > 5.4)
            {
                transform.position = new Vector3(transform.position.x, 5.4f, transform.position.z);
            }
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
        stop = true;
        stop1 = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 8);
        text.fontSize = 18;
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
            Debug.Log(totalTime +" text:" + text.text);
            yield return new WaitForSeconds(1);
            totalTime--;
        }
        text.text = " ";
        stop = false;
        stop1 = false;
        totalTime = 4;
    }

    void gameFail()
    {
        stop = true;
        stop1 = true;
        text.text = "YOU FAIL...";
        //bt1.transform.localScale = new Vector3(1, 1, 1);
        bt2.transform.localScale = new Vector3(1, 1, 1);
    }
    

    private void OnTriggerEnter(Collider other)
    {
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
            zSpeed += 5;
        }
    }
}
