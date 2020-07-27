using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.Fusa.FusaParty
{
    public class Launcher : MonoBehaviour
    {
        #region Private Serializable Fields

        #endregion

        #region Private Fields

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";

        #endregion

        #region MonoBehaviour CallBacks


        /// <summary>
        /// MonoBehavior method called on GameObject by Unity during early initialization phase.
        /// </summary>

        void Awake()
        {
            // #Critical
            // this make sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        private void Start()
        {
            Connect();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room
        /// - If not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
        {
            // We check if we are connected or not, we join if we are, else we initiate the connection to the server.
            if(PhotonNetwork.IsConnected)
            {
                // #Critical: We need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Critical: We must first and foremost connect to Photone Online Server.
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();

            }
        }

        #endregion

    }
}
