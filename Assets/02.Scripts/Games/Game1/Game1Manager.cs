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
    public int playerAnswerCube;
    public int playerAnswerQuad;
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
    private int answerMaterialId;
    private int[] ranArr = Enumerable.Range(0, rangeLength).ToArray();

    private Game1Manager game1Manager;
    private string objectName;
    private int objectNumber;

    #endregion

    #region Photon Callbacks


    void Start()
    {
        startGame = false;
        LoadGames();
        Debug.Log("Game1Manager");

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
                    objectNumber = int.Parse(hit.collider.gameObject.name.Substring(4, 1));
                    switch (objectName)
                    {
                        case "Cube":
                            if (game1Manager.playerAnswerCube == 0)
                            {
                                game1Manager.playerAnswerCube = objectNumber;
                                hit.collider.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
                            }
                            else
                            {
                                game1Manager.playerAnswerCube = 0;
                                hit.collider.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
                            }
                            break;
                        case "Quad":
                            if (game1Manager.playerAnswerQuad == 0)
                            {
                                game1Manager.playerAnswerQuad = objectNumber;
                                hit.collider.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
                            }
                            else
                            {
                                game1Manager.playerAnswerQuad = 0;
                                hit.collider.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
                            }
                            break;
                        default:
                            break;
                    }
                    if (game1Manager.playerAnswerCube != 0 && game1Manager.playerAnswerQuad != 0)
                    {
                        if (game1Manager.answerCube == game1Manager.playerAnswerCube && game1Manager.answerQuad == game1Manager.playerAnswerQuad)
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
    public void setttingGames()
    {
        playerAnswerCube = 0;
        playerAnswerQuad = 0;
        Random.seed = System.DateTime.Now.Millisecond;
        answerCube = Random.Range(1, 9);
        answerQuad = Random.Range(1, 4);
        game1Manager = GameObject.Find("Game1Manager").GetComponent<Game1Manager>();

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

    public void setCube(int number, int materialId)
    {
        cube = GameObject.Find("Cube" + number.ToString());
        cube.GetComponent<MeshRenderer>().material = Resources.Load("Game1/" + materialId, typeof(Material)) as Material;
        if (number == answerCube)
        {
            answerMaterialId = materialId;
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
        setttingGames();
    }

    #endregion
}
