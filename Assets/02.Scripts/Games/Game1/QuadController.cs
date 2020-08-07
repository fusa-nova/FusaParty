using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class QuadController : MonoBehaviour
{
    #region Public Fields

    #endregion

    #region Private Fields
    [SerializeField]
    GameObject quizQuad;
    Game1Manager game1Manager;

    #endregion

    #region MonoBehaviour Callbacks
    [System.Obsolete]
    void Start()
    {
        if(quizQuad == null)
        {
            quizQuad = gameObject;
        }
        game1Manager = GameObject.Find("Game1Manager").GetComponent<Game1Manager>();
    }

    [System.Obsolete]
    void Update()
    {
        if (game1Manager.startGame == true)
        {

        }
    }

    #endregion

}
