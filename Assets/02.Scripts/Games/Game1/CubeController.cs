using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class CubeController : MonoBehaviour, IPunObservable
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
    #endregion

    #region MonoBehaviour Callbacks
    [System.Obsolete]
    void Start()
    {
        if (quizCube == null)
        {
            quizCube = this.gameObject;
        }
        rotateSpeed = 50f;
        game1Manager = GameObject.Find("Game1Manager").GetComponent<Game1Manager>();
    }

    [System.Obsolete]
    void Update()
    {
        if (game1Manager.startGame == true)
        {
            if (rotateSpeed > 0)
            {
                if (rotateSpeed >= 20f)
                {
                    rotateSpeed -= 0.05f;
                }
                else if (rotateSpeed >= 15f)
                {
                    rotateSpeed -= 0.02f;
                }
                else if (rotateSpeed >= 10f)
                {
                    rotateSpeed -= 0.005f;
                }
                else if (rotateSpeed >= 7f)
                {
                    rotateSpeed -= 0.003f;
                }
                else if (rotateSpeed >= 3f)
                {
                    rotateSpeed -= 0.002f;
                }
                else
                {
                    rotateSpeed -= 0.001f;
                }
                //Debug.Log(+"인스턴스아이디");
                Random.InitState(10 + 1 + (int)Time.time);
                Random.InitState(this.gameObject.GetInstanceID() + 1 + (int)Time.time);
                quizCube.transform.Rotate(Vector3.up * rotateSpeed * Random.value);
                Random.InitState(10 + 1 + (int)Time.time);
                Random.InitState(this.gameObject.GetInstanceID() + 2 + (int)Time.time);
                quizCube.transform.Rotate(Vector3.left * rotateSpeed * Random.value);
                Random.InitState(10 + 1 + (int)Time.time);
                Random.InitState(this.gameObject.GetInstanceID() + 3 + (int)Time.time);
                quizCube.transform.Rotate(Vector3.forward * rotateSpeed * Random.value);
            }
            else
            {
                rotateSpeed = 0f;
                quizCube.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left) 
        { 
            Debug.Log("Mouse Click Button : Left"); 
        } 
        else if (pointerEventData.button == PointerEventData.InputButton.Middle) 
        { 
            Debug.Log("Mouse Click Button : Middle"); 
        } 
        else if (pointerEventData.button == PointerEventData.InputButton.Right) 
        { 
            Debug.Log("Mouse Click Button : Right"); 
        }
        Debug.Log("Mouse Position : " + pointerEventData.position); 
        Debug.Log("Mouse Click Count : " + pointerEventData.clickCount);

    }

    #endregion

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(meshRenderer.materials[0]);
        }
        else
        {
            Debug.LogError("씨발" + (Material)stream.ReceiveNext());
            Debug.LogErrorFormat("씨발썅봉" + (Material)stream.ReceiveNext());
            this.meshRenderer.materials[0] = (Material)stream.ReceiveNext() as Material;
        }
    }

    #endregion

}

