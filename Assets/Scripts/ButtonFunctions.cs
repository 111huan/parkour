using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    Button btStart, btToMenu, btPause, btContinue,btExit;
    // Start is called before the first frame update
    void Start()
    {
        btStart = GameObject.Find("Start").GetComponent<Button>();
        btToMenu = GameObject.Find("ToMenu").GetComponent<Button>();
        //btPause = GameObject.Find("Pause").GetComponent<Button>();
        //btContinue = GameObject.Find("Continue").GetComponent<Button>();
        btExit = GameObject.Find("Exit").GetComponent<Button>();
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
}
