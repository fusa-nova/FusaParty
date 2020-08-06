using System.Linq;
using UnityEngine;

public class Game1Manager : MonoBehaviour
{

    #region Public Fields

    public GameObject cube;
    public GameObject quad;
    public int answerCube;
    public int answerQuad;
    public int playerAnswerCube;
    public int playerAnswerQuad;
    public CubeController cubeController;

    #endregion

    #region Private Fields
    [SerializeField]
    private static int rangeLength = 16;
    [SerializeField]
    private int maxCube = 8;
    [SerializeField]
    private int maxQuad = 3;
    private int answerMaterialId;
    private int[] ranArr = Enumerable.Range(0, rangeLength).ToArray();

    #endregion

    [System.Obsolete]
    void Start()
    {
        playerAnswerCube = 0;
        playerAnswerQuad = 0;
        Random.seed = System.DateTime.Now.Millisecond;
        answerCube = Random.Range(1, 9);
        answerQuad = Random.Range(1, 4);

        for (int i = 0; i < rangeLength; ++i)
        {
            int ranIdx = Random.Range(i, rangeLength);

            int temp = ranArr[ranIdx];
            ranArr[ranIdx] = ranArr[i];
            ranArr[i] = temp;
        }

        for (int i = 0; i < maxCube; ++i)
        {
            setCube(i + 1, ranArr[i] + 1);
        }

        Debug.Log(answerCube);
        Debug.Log(answerQuad);

        for (int i = 0; i < maxQuad; ++i)
        {
            setQuad(i + 1, ranArr[maxCube + i] + 1);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("으악" + Input.mousePosition);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Camera.main.
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit))
            //{
            //    Debug.Log("???");
            //    if (Input.GetMouseButton(0))
            //    {
            //        Debug.Log("?12312?");
            //    }
            //}
            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position, 1000f);
            //Debug.Log(hit.transform);
            //if (hit)
            //{
            //    Debug.Log("맞음");
            //}
        }
    }



    public void setCube(int number, int materialId)
    {
        cube = GameObject.Find("Cube" + number.ToString());
        cubeController = cube.GetComponent<CubeController>();
        cube.GetComponent<MeshRenderer>().material = Resources.Load("Game1/" + materialId, typeof(Material)) as Material;
        //cubeController.materialNumber = materialId;
        if (number == answerCube)
        {
            answerMaterialId = materialId;
            //quad = GameObject.Find("Quad" + answerQuad.ToString());
            //quad.GetComponent<MeshRenderer>().material = Resources.Load("Game1/" + materialId, typeof(Material)) as Material;
        }
    }

    public void setQuad(int number, int materialId)
    {
        if (number == answerQuad)
        {
            materialId = answerMaterialId;
        }
        quad = GameObject.Find("Quad" + number.ToString());
        quad.GetComponent<MeshRenderer>().material = Resources.Load("Game1/" + materialId, typeof(Material)) as Material;
    }

}
