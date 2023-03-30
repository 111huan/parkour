using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static string state = "solid";
    Rigidbody rb;
    float gasSpeed = 5;
    float zSpeed = 8;
    bool unfixed = true;
    public static bool fail = false,success = false, stop = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, zSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        stateCtl();
        speedCtl();
    }

    void speedCtl()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, zSpeed);
        if (state == "gas")
        {
            gasMove();
        }
        if (stop)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
    void stateCtl()
    {
        
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("solid");
            state = "solid";
            rb.useGravity = true;
            if (!Retry.ballFixed)
            {
                stop = false;
            }
        }
        else if (Input.GetKeyDown("2"))
        {
            Debug.Log("liquid");
            state = "liquid";
            rb.useGravity = true;
            if (!Retry.ballFixed)
            {
                stop = false;
            }
        }
        else if (Input.GetKeyDown("3"))
        {
            Debug.Log("gas");
            state = "gas";
            rb.useGravity = false;
            if (!Retry.ballFixed)
            {
                stop = false;
            }
        }

        
    }

    void gasMove()
    {
        if (transform.position.y<8)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + gasSpeed * Time.deltaTime, transform.position.z);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, zSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wind" && state == "gas")
        {
            Debug.Log("------FAIL------");
            fail = true;
        }
        if (other.gameObject.tag == "thorn" && state == "solid")
        {
            Debug.Log("------FAIL------");
            fail = true;
        }
        if (other.gameObject.tag == "DrainFloor" && state == "liquid")
        {
            Debug.Log("------FAIL------");
            fail = true;
        }
        if (other.gameObject.tag == "success")
        {
            Debug.Log("------SUCCESS------");
            success = true;
        }
        if(other.gameObject.tag == "drain" && state != "liquid")
        {
            stop = true;
        }
        if (other.gameObject.tag == "door" && state != "solid")
        {
            stop = true;
        }
    }
}
