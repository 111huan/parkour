using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaa : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    Mesh cubeMesh, sphereMesh;
    Vector3[] cubeVirtices,sphereVirtices;
    // Start is called before the first frame update
    void Start()
    {
        cubeMesh = cube.GetComponent<MeshFilter>().mesh;
        sphereMesh = sphere.GetComponent<MeshFilter>().mesh;
        cubeVirtices = cubeMesh.vertices;
        sphereVirtices = sphereMesh.vertices;
        Debug.Log("cube:" + cubeVirtices.Length + "\nsphere:" + sphereVirtices.Length);
    }

    void Update()
    {
        for(int i = 0; i < sphereVirtices.Length; i++)
        {
        }
    }
}
