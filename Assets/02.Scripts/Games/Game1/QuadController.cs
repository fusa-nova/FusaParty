using Photon.Pun;
using UnityEngine;

public class QuadController : MonoBehaviour
{
    #region Public Fields
    public bool answer;
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
        if (quizQuad == null)
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

    #region PhotonView RPC
    public void MaterialRPC(int materialId, int number)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ChangeQuadMaterial", RpcTarget.All, materialId, number);
    }
    [PunRPC]
    public void ChangeQuadMaterial(int materialId, int number)
    {
        GetComponent<MeshRenderer>().material = Resources.Load("Game1/" + materialId, typeof(Material)) as Material;
        if(number > 0)
        {
            answer = true;
        }
    }

    #endregion
}
