using Photon.Pun;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    #region Public Fields
    [Tooltip("스피어 회전 속도 조정을 위한 필드 값")]
    public float rotateSpeed;
    public bool answer;
    #endregion

    #region Private Fields
    [SerializeField]
    GameObject quizCube;
    Game1Manager game1Manager;
    private MeshRenderer meshRenderer;
    PhotonView photonView;
    #endregion

    #region MonoBehaviour Callbacks
    [System.Obsolete]
    void Start()
    {

        if (quizCube == null)
        {
            quizCube = this.gameObject;
        }
        //rotateSpeed = 50f;
        rotateSpeed = 0.2f;
        game1Manager = GameObject.Find("Game1Manager").GetComponent<Game1Manager>();
    }

    [System.Obsolete]
    void Update()
    {
        if (game1Manager.startGame == true)
        {
            if (rotateSpeed > 0)
            {
                //if (rotateSpeed >= 20f)
                //{
                //    rotateSpeed -= 0.05f;
                //}
                //else if (rotateSpeed >= 15f)
                //{
                //    rotateSpeed -= 0.02f;
                //}
                //else if (rotateSpeed >= 10f)
                //{
                //    rotateSpeed -= 0.005f;
                //}
                //else if (rotateSpeed >= 7f)
                //{
                //    rotateSpeed -= 0.003f;
                //}
                //else if (rotateSpeed >= 3f)
                //{
                //    rotateSpeed -= 0.002f;
                //}
                //else
                //{
                //    rotateSpeed -= 0.001f;
                //}

                Random.InitState(this.gameObject.GetInstanceID() + 1 + (int)Time.time);
                quizCube.transform.Rotate(Vector3.up * rotateSpeed * Random.value);
                Random.InitState(this.gameObject.GetInstanceID() + 2 + (int)Time.time);
                quizCube.transform.Rotate(Vector3.left * rotateSpeed * Random.value);
                Random.InitState(this.gameObject.GetInstanceID() + 3 + (int)Time.time);
                quizCube.transform.Rotate(Vector3.forward * rotateSpeed * Random.value);
            }
            else
            {
                rotateSpeed = 0f;
                ChangeSpeedRPC(rotateSpeed);
                quizCube.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }

    #endregion


    public void MaterialRPC(int materialId, int number)
    {
        photonView = PhotonView.Get(this);
        photonView.RPC("ChangeCubeMaterial", RpcTarget.All, materialId, number);
    }
    [PunRPC]
    public void ChangeCubeMaterial(int materialId, int number)
    {
        GetComponent<MeshRenderer>().material = Resources.Load("Game1/" + materialId, typeof(Material)) as Material;
        if(number > 0)
        {
            answer = true;
        }
    }

    public void ChangeSpeedRPC(float speed)
    {
        photonView = PhotonView.Get(this);
        photonView.RPC("ChangeSpeed", RpcTarget.All, speed);
    }

    [PunRPC]
    public void ChangeSpeed(float speed)
    {
        rotateSpeed = speed;
    }
}

