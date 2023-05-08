using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenerateRoad : MonoBehaviour
{
    int count = 0;
    int randomNum = 0;
    Vector3 generateStartPos;
    [SerializeField] GameObject[] ob;


    // Start is called before the first frame update
    void Start()
    {
        generateStartPos = new Vector3(0, 4.14f, 234);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generate()
    {
        Debug.Log("generate");
        if (count == 0)
        {
            for(int i = 0; i < 3; i++)
            {
                randomNum = Random.Range(0, 6);
                Instantiate(ob[randomNum],generateStartPos,new Quaternion(0,0,0,0));
                generateStartPos = new Vector3(generateStartPos.x, generateStartPos.y, generateStartPos.z + 50);
                count++;
            }
        }
        else
        {
            randomNum = Random.Range(0, 6);
            Instantiate(ob[randomNum], generateStartPos, new Quaternion(0, 0, 0, 0));
            generateStartPos = new Vector3(generateStartPos.x, generateStartPos.y, generateStartPos.z + 50);
            count++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enter")
        {
            generate();
        }
        if(other.gameObject.tag == "out")
        {
            Destroy(other.transform.parent.gameObject,10f);
        }
    }
}
