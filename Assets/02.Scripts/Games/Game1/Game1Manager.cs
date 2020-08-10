using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class Game1Manager : MonoBehaviourPunCallbacks
{

    #region Public Fields

    public GameObject cube;
    public GameObject quad;
    public int answerCube;
    public int answerQuad;
    public GameObject playerAnswerCube;
    public GameObject playerAnswerQuad;
    public CubeController cubeController;
    public bool startGame;

    #endregion

    #region Private Fields
    [SerializeField]
    private static int rangeLength = 16;
    [SerializeField]
    private int maxCube = 8;
    [SerializeField]
    private int maxQuad = 3;
    public int quadStart = -3;
    private int answerMaterialId;
    private int[] ranArr = Enumerable.Range(0, rangeLength).ToArray();

    private string objectName;
    private int objectNumber;

    public int[] rotateCubeX = new int[4];
    public int[] rotateCubeY = new int[2];
    public int[,] rotateMatrix = new int[4,2];
    public int[] quadMatrix = new int[3];


    #endregion

    #region Photon Callbacks


    void Start()
    {
        startGame = false;
        LoadGames();
        Debug.Log("Game1Manager");
        rotateCubeX[0] = -3;
        rotateCubeX[1] = -1;
        rotateCubeX[2] = 1;
        rotateCubeX[3] = 3;
        rotateCubeY[0] = 0;
        rotateCubeY[1] = -2;
        for(int i = 0; i < rotateCubeY.Length; i++)
        {
            for (int j = 0; j < rotateCubeX.Length; j++)
            {
                rotateMatrix[j,i] = 0;
            }
        }
        for(int i = 0; i < maxQuad; ++i)
        {
            quadMatrix[i] = 0;
        }
    }

    void Update()
    {
        if (startGame == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    objectName = hit.collider.gameObject.name.Substring(0, 4);
                    //objectNumber = hit.collider.gameObject.GetComponent<CubeController>();
                    objectNumber = 10;
                    switch (objectName)
                    {
                        case "Cube":
                            if (playerAnswerCube == null)
                            {
                                playerAnswerCube = hit.collider.gameObject;
                                playerAnswerCube.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
                            }
                            else
                            {
                                if (playerAnswerCube == hit.collider.gameObject)
                                {
                                    playerAnswerCube.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
                                    playerAnswerCube = null;
                                }
                                else
                                {
                                    playerAnswerCube.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
                                    playerAnswerCube = hit.collider.gameObject;
                                    playerAnswerCube.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
                                }
                            }
                            break;
                        case "Quad":
                            if (playerAnswerQuad == null)
                            {
                                playerAnswerQuad = hit.collider.gameObject;
                                hit.collider.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
                            }
                            else
                            {
                                if (playerAnswerQuad == hit.collider.gameObject)
                                {
                                    playerAnswerQuad.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
                                    playerAnswerQuad = null;
                                }
                                else
                                {
                                    playerAnswerQuad.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
                                    playerAnswerQuad = hit.collider.gameObject;
                                    playerAnswerQuad.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    if (playerAnswerCube != null && playerAnswerQuad != null)
                    {

                        if (playerAnswerCube.GetComponent<CubeController>().answer && playerAnswerQuad.GetComponent<QuadController>().answer)
                        {
                            Debug.Log("정답입니다");
                        }
                        else
                        {
                            Debug.Log("오답입니다");
                        }
                    }
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

            LoadGames();
        }
    }

    #endregion

    #region Game Logic
    [System.Obsolete]
    public void SetttingGames()
    {
        if (PhotonNetwork.IsMasterClient)
        {
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
                SetCube(i + 1, ranArr[i] + 1);
            }
            Debug.Log(answerCube + "정답 큐브");
            Debug.Log(answerQuad + "정답 쿼드");
            for (int i = 0; i < maxQuad; ++i)
            {
                SetQuad(i + 1, ranArr[maxCube + i] + 1);
            }
        }
    }

    public void SetCube(int number, int materialId)
    {
        cube = MakeCube();
        if (cube != null)
        {
            //photonView.RPC("ChangeCubeMaterial", RpcTarget.All, materialId);
            cube.GetComponent<MeshRenderer>().material = Resources.Load("Game1/" + materialId, typeof(Material)) as Material;
            if (number == answerCube)
            {
                cube.GetComponent<CubeController>().answer = true;
            }
        }
        cube = null;
    }

    public GameObject MakeCube()
    {
        for (int i = 0; i < rotateCubeY.Length; i++)
        {
            for (int j = 0; j < rotateCubeX.Length; j++)
            {
                if (rotateMatrix[j, i] == 0)
                {
                    rotateMatrix[j, i] = 1;
                    cube = PhotonNetwork.Instantiate("Game1/Cube", new Vector3(rotateCubeX[j], rotateCubeY[i], 2f), Quaternion.identity, 0);
                    return cube;
                }

            }
        }
        return null;
    }

    public void ChangeCubeMaterial(int materialId)
    {
        quad.GetComponent<MeshRenderer>().material = Resources.Load("Game1/" + materialId, typeof(Material)) as Material;
    }

    public void SetQuad(int number, int materialId)
    {
        quad = MakeQuad();
        if (quad != null)
        {
            quad.GetComponent<MeshRenderer>().material = Resources.Load("Game1/" + materialId, typeof(Material)) as Material;

            if (number == answerQuad)
            {
                quad.GetComponent<QuadController>().answer = true;
            }
        }
        quad = null;
    }

    public GameObject MakeQuad()
    {
        for (int i = 0; i < maxQuad; ++i)
        {
            if (quadMatrix[i] == 0)
            {
                quadMatrix[i] = 1;
                quad = PhotonNetwork.Instantiate("Game1/Quad", new Vector3(quadStart, 3f, 2f), Quaternion.identity, 0);
                quadStart += 3;
                return quad;
            }
        }
        return null;
    }



    #endregion

    #region Private Methods

    void LoadGames()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            //Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PlayerCount" + PhotonNetwork.CurrentRoom.PlayerCount);
        //PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        if(PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            Debug.Log("Game Start");
            // 나중에 플레이어가 시작을 눌렀을 때로 변경해야한다.
            StartGames();
        }
    }

    void StartGames()
    {
        StartCoroutine("StartGameCoroutine");
    }

    [System.Obsolete]
    IEnumerator StartGameCoroutine()
    {
        Debug.Log("3");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("2");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("1");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("start");
        startGame = true;
        SetttingGames();
    }

    #endregion
}
