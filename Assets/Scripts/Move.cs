using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public static string state = "solid";
    public static bool newGame = true;
    Rigidbody rb;
    ParticleSystem gasParticle;
    Text text,score;
    Slider slider;
    Animation animSolid, animFluid;
    float gasSpeed = 5;
    public static float zSpeed = 10;
    public static bool fail = false, success = false, stop = true, stop1 = true;
    [SerializeField] Transform solidObj, liquidObj, gasObj;
    Vector3 solidOn, liquidOn, gasOn;
    int  nowScore = 0,bestScore;
    Button bt2, btRetry, ctn, sun, moon, cloud, skip;
    [SerializeField] Material sky,liquidMaterial;
    string lastState = null;
    Transform allBall;
    void Start()
    {
        PlayerPrefs.SetInt("bestScore", 5);
        stop = true;
        stop1 = true;
        state = "solid";
        allBall = GameObject.Find("AllBall").transform;
        allBall.localPosition = new Vector3(0, 5, -115);
        animSolid = solidObj.gameObject.GetComponent<Animation>();
        animFluid = liquidObj.gameObject.GetComponent<Animation>();
        bestScore = PlayerPrefs.GetInt("bestScore");
        //slider = GameObject.Find("Slider").GetComponent<Slider>();
        text = GameObject.Find("Text").GetComponent<Text>();
        score = GameObject.Find("ScoreBoard").GetComponent<Text>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, zSpeed);
        solidOn = solidObj.localScale;
        liquidOn = liquidObj.localScale;
        gasOn = gasObj.localScale;
        gasParticle = gasObj.gameObject.GetComponent<ParticleSystem>();
        bt2 = GameObject.Find("ToMenu").GetComponent<Button>();
        skip = GameObject.Find("Skip").GetComponent<Button>();
        btRetry = GameObject.Find("Retry").GetComponent<Button>();
        ctn = GameObject.Find("Continue").GetComponent<Button>();
        gasParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        stateCtl();
        speedCtl();
        scoreBoard();
    }

    void scoreBoard()
    {
        if (!newGame)
        {
            if (nowScore / 2 >= bestScore)
            {
                bestScore = nowScore / 2;
            }
            score.text = "current score：" + nowScore / 2 + "\nbest record：" + bestScore;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
        else
        {
            score.text = "";
        }
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

    public void change2Gas()
    {
        lastState = state;
        if (state == "liquid")
        {
            animSolid[animSolid.clip.name].time = animSolid[animSolid.clip.name].length;
            animSolid[animSolid.clip.name].speed = -1;
            animSolid.Play(animSolid.clip.name);
        }
        gasParticle.Play();
        state = "gas";
    }
    public void change2Solid()
    {
        lastState = state;
        if (state == "liquid")
        {
            solidObj.localRotation = new Quaternion(-0.7f, 0, 0, 0.7f);
            animSolid[animSolid.clip.name].time = animSolid[animSolid.clip.name].length;
            animSolid[animSolid.clip.name].speed = -1;
            animSolid.Play(animSolid.clip.name);
        }
        gasParticle.Stop();
        state = "solid";
    }

    public void change2Liquid()
    {
        lastState = state;
        if (state == "solid")
        {
            solidObj.localRotation = new Quaternion(-0.7f, 0, 0, 0.7f);
            animSolid[animSolid.clip.name].time = 0;
            animSolid[animSolid.clip.name].speed = 1;
            animSolid.Play(animSolid.clip.name);
            print(animSolid.isPlaying);
        }
        if (state == "gas")
        {
            liquidObj.localScale = Vector3.Lerp(liquidObj.localScale, new Vector3(0, 0, 0), Time.deltaTime * 10);
        }
        gasParticle.Stop();
        state = "liquid";
    }

    void stateCtl()
    {
        if (state == "solid")
        {
            solidObj.Rotate(new Vector3(50 * Time.deltaTime, 0, 0));
            sky.SetColor("_Color1", Color.Lerp(sky.GetColor("_Color1"), new Color(0, 0, 0.3f), Time.deltaTime));
            sky.SetColor("_Color2", Color.Lerp(sky.GetColor("_Color2"), new Color(0.7f, 0.9f, 1), Time.deltaTime));
            rb.useGravity = true;
            if (solidObj.localScale.x < 30 && !animSolid.isPlaying)//固体渐渐出现
            {
                float dScale = solidObj.localScale.x;
                solidObj.localScale = new Vector3(dScale + 25f * Time.deltaTime, dScale + 25f * Time.deltaTime, dScale + 25f * Time.deltaTime);
            }
            else
            {
                solidObj.localScale = solidOn;
            }
            liquidObj.localScale = new Vector3(0, 0, 0);
            gasObj.localScale = new Vector3(0, 0, 0);
        }
        if (state == "liquid")
        {
            rb.useGravity = true ;
            sky.SetColor("_Color1", Color.Lerp(sky.GetColor("_Color1"), new Color(0, 0.3f, 0.3f), Time.deltaTime)) ;
            sky.SetColor("_Color2", Color.Lerp(sky.GetColor("_Color2"), new Color(0.8f, 1, 0.9f), Time.deltaTime));
            waterMaterialChange();
            if (lastState == "gas" && liquidObj.localScale.x < liquidOn.x)
            {
                liquidObj.localScale = Vector3.Lerp(liquidObj.localScale, liquidOn, 1.5f * Time.deltaTime);
            }
            else if (!animSolid.isPlaying)
            {
                liquidObj.localScale = liquidOn;
                solidObj.localScale = new Vector3(0, 0, 0);
                gasObj.localScale = new Vector3(0, 0, 0);
            }
        }
        if(state == "gas")
        {
            sky.SetColor("_Color1", Color.Lerp(sky.GetColor("_Color1"), new Color(0.7f, 0, 0), Time.deltaTime));
            sky.SetColor("_Color2", Color.Lerp(sky.GetColor("_Color2"), new Color(1, 0.85f, 0.65f), Time.deltaTime));
            rb.useGravity = false;
            if (solidObj.localScale.x > 0)//原本的固体/液体模型渐渐消失
            {
                if (solidObj.localScale.x > 0)
                {
                    float dScale = solidObj.localScale.x;
                    solidObj.localScale = new Vector3(dScale - 25f * Time.deltaTime, dScale - 25f * Time.deltaTime, dScale - 25f * Time.deltaTime);
                }
            }
            else if (liquidObj.localScale.x > 0)
            {
                float dScale = liquidObj.localScale.x;
                liquidObj.localScale = new Vector3(dScale - 25f * Time.deltaTime, dScale - 25f * Time.deltaTime, dScale - 25f * Time.deltaTime);
            }
            else if (!animSolid.isPlaying)//若animSolid已经播放完，切换成气态模型
            {
                solidObj.localScale = new Vector3(0, 0, 0);
                liquidObj.localScale = new Vector3(0, 0, 0);
                gasObj.localScale = gasOn;
            }
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
        btRetry.transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }

    
    void gameFail()
    {
        stop = true;
        stop1 = true;
        if (nowScore / 2 <= bestScore)
        {
            text.text = " congrats， you got " + nowScore / 2 + " points!";
        }
        else
        {
            text.text = " congrats， you got " + nowScore / 2 + " points,"+"\nit's a new record!";
        }
        //bt1.transform.localScale = new Vector3(1, 1, 1);
        bt2.transform.localScale = new Vector3(1, 1, 1);
    }

    void promptCtl(string promptName)
    {
        if(promptName == "Prompt1")
        {
            text.text = "click the buttons to control the ball's state";
        }
        else if(promptName == "Prompt2")
        {
            text.text = "change to fluid to pass";
            //Invoke("delay", 3);
        }
        else if (promptName == "Prompt3")
        {
            text.text = "do not try to pass by fluid";
            //Invoke("delay", 3);
        }
        else if (promptName == "Prompt4")
        {
            text.text = "change to gas to pass";
            //Invoke("delay", 3);
        }
        else if (promptName == "Prompt5")
        {
            text.text = "do not try to pass by gas";
            //Invoke("delay", 3);
        }
        else if (promptName == "Prompt6")
        {
            text.text = "change to solid to pass";
            //Invoke("delay", 3);
        }
        else if (promptName == "Prompt7")
        {
            text.text = "do not try to pass by solid";
            //Invoke("delay", 3);
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
            text.text = "official begin";
            Invoke("delay", 3);
            //zSpeed += 5;
        }
        if (other.gameObject.tag == "prompt"&&!stop)
        {
            print("prompt start");
            promptCtl(other.name);
            stop = true;
            stop1 = true;
            ctn.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
    }

    void waterMaterialChange()
    {
        float tX = 1.5f, tY = 1.5f, oX = 0.5f , oY = 0.5f;
        Vector2 nowTiling = liquidMaterial.GetTextureScale("_MainTex");
        Vector2 nowOffset = liquidMaterial.GetTextureOffset("_MainTex");
        float paceX1 = 0.5f, paceY1 = 0.5f, paceX2 = 0.5f, paceY2=0.5f;
        float rate = 0.05f;
        if (nowTiling.x > 2)
        {
            paceX1 = -rate;
            nowTiling.x = 2;
        }
        else if (nowTiling.x < 1)
        {
            paceX1 = rate;
            nowTiling.x = 1;
        }
        if (nowTiling.y > 2f)
        {
            paceY1 = -rate;
            nowTiling.y = 2f;
        }
        else if (nowTiling.y < 1)
        {
            paceY1 = rate;
            nowTiling.y = 1;
        }
        // nowOffset
        if (nowTiling.x > 0.5)
        {
            paceX2 = -rate;
            nowTiling.x = 0.5f;
        }
        else if (nowTiling.x < -0.5)
        {
            paceX2 = rate;
            nowTiling.x = 1;
        }
        if (nowTiling.y > 0.5f)
        {
            paceY2 = -rate;
            nowTiling.y = 0.5f;
        }
        else if (nowTiling.x < -0.5)
        {
            paceY2 = rate;
            nowTiling.x = -0.5f;
        }

        liquidMaterial.SetTextureScale("_MainTex", new Vector2(nowTiling.x + paceX1 * Time.deltaTime,
            nowTiling.y +  paceY1 * Time.deltaTime));
        liquidMaterial.SetTextureOffset("_MainTex", new Vector2(nowOffset.x + paceX2 * Time.deltaTime,
            nowOffset.y +paceY2 * Time.deltaTime));
    }
}