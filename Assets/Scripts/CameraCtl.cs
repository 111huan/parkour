using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtl : MonoBehaviour
{
    float dx, dy, dz;
    public Transform ball;
    // Start is called before the first frame update
    void Start()
    {
        dx = ball.position.x - transform.position.x;
        dy = ball.position.y - transform.position.y;
        dz = ball.position.z - transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(ball.position.x - dx, ball.position.y - dy, ball.position.z - dz);
    }
}