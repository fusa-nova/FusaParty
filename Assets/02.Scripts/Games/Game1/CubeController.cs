using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class CubeController : MonoBehaviour
{
    #region Public Fields
    [Tooltip("스피어 회전 속도 조정을 위한 필드 값")]
    public float rotateSpeed;
    #endregion

    #region Private Fields
    [SerializeField]
    GameObject quizCube;
    Game1Manager game1Manager;

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
                Random.InitState(int.Parse(this.gameObject.name.Substring(4, 1)) * 10 + 1 + (int)Time.time);
                quizCube.transform.Rotate(Vector3.up * rotateSpeed * Random.value);
                Random.InitState(int.Parse(this.gameObject.name.Substring(4, 1)) * 10 + 2 + (int)Time.time);
                quizCube.transform.Rotate(Vector3.left * rotateSpeed * Random.value);
                Random.InitState(int.Parse(this.gameObject.name.Substring(4, 1)) * 10 + 3 + (int)Time.time);
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

}
