using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animtest : MonoBehaviour
{
    float currentTime, clipTime;
    //Animation anim;
    Animator ac;
    Animation  myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = gameObject.GetComponent<Animation>();
        
        //print(ac.GetCurrentAnimatorClipInfo(0).Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim[myAnim.clip.name].time = myAnim[myAnim.clip.name].length;//´Ó×îºóµ¹²¥
            myAnim[myAnim.clip.name].speed = -0.2f;
            myAnim.Play(myAnim.clip.name);
        }
    }
}
