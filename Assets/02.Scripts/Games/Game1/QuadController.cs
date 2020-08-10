using Photon.Pun;
using UnityEngine;

public class QuadController : MonoBehaviour, IPunObservable
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

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.IsWriting)
        //{
        //    // We own this player: send the others our data
        //    stream.SendNext(this.GetComponent<MeshRenderer>().material);
        //}
        //else
        //{
        //    // Network player, receive data
        //    this.GetComponent<MeshRenderer>().material = (Material)stream.ReceiveNext();
        //}
    }

    #endregion
}
