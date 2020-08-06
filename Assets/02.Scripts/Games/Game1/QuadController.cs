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
    public int materialNumber;

    #endregion

    #region MonoBehaviour Callbacks
    void Start()
    {
        if(quizQuad == null)
        {
            quizQuad = this.gameObject;
        }
    }

    #endregion

}
