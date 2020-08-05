using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room1Manager : MonoBehaviour
{
    public static int rangeLength = 16;
    public int maxCube = 8;
    public int[] ranArr = Enumerable.Range(0, rangeLength).ToArray();
    public GameObject cube;
    public CubeController cubeController;

    void Start()
    {
        for (int i = 0; i < rangeLength; ++i)
        {
            int ranIdx = Random.Range(i, rangeLength);

            int temp = ranArr[ranIdx];
            ranArr[ranIdx] = ranArr[i];
            ranArr[i] = temp;
        }
        for (int i = 0; i < maxCube; ++i)
        {
            //Debug.Log(ranArr[i]+"애용");
            setCube(i + 1, ranArr[i] + 1);
        }
    }



    public void setCube(int number, int materialId)
    {
        cube = GameObject.Find(number.ToString());
        cubeController = cube.GetComponent<CubeController>();
        cube.GetComponent<MeshRenderer>().material = Resources.Load("Room1/"+materialId, typeof(Material)) as Material;
        cubeController.materialNumber = materialId;
    }

}
