using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

namespace Com.Fusa.FusaParty
{

    /// <summary>
    /// Player Manager.
    /// Handles fire Input and Beams
    /// </summary>
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {

        #region Public Field

        [Tooltip("The current Health of our player")]
        public float Health = 1f;

        public static GameManager Instance;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstacne;

        #endregion

        #region Private Fields

        [Tooltip("The Beams GameOBject to control")]
        [SerializeField]
        private GameObject beams;
        // True, when the user is firing
        bool isFiring;

        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {

            // #Important
            // Used in GameManager.cs : We keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstacne = this.gameObject;
            }
            // #Critical
            // We flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);

            if (beams == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }
        }
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollwing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }

            #if UNITY_5_4_OR_NEWER
            // Unity 5.4 has a new scene management. Register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
            {
                this.CalledOnLevelWasLoaded(scene.buildIndex);
            };

            #endif
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {
            if (photonView.IsMine)
            {
                ProcessInputs();
            }
            if (Health <= 0f)
            {
                GameManager.Instance.LeaveRoom();
            }
            // trigger Beams active state
            if (beams != null && isFiring != beams.activeInHierarchy)
            {
                beams.SetActive(isFiring);
            }
        }

        /// <summary>
        /// MonoBehaviour method called when the Collider 'other' enters the trigger.
        /// Affect Health of the Player if the collider is a beam.
        /// Note : when jumping and firing at the same, you'll find that the player's own beam intersects with itself
        /// One could move the collider further away to prevent this or check if the beam belongs to the player.
        /// </summary>
        /// <param name="other"></param>
        void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            // We are only interested in Beamers
            // We should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f;
        }

        /// <summary>
        /// MonoBehaviour method called once per frame for every Collider 'other' that is touching the trigger.
        /// We're going to affect health while the beams are touching the player
        /// </summary>
        /// <param name="other">Other.</param>
        void OnTriggerStay(Collider other)
        {
            // we don't do anything if we are not the local player.
            if (!photonView.IsMine)
            {
                return;
            }
            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            // We slowly affect health when beam is constantly hitting us, so player has to move to prevent death.
            Health -= 0.1f * Time.deltaTime;
        }

        #if !UNITY_5_4_OR_NEWER
        /// <summary>
        /// See CalledOnLevelWasLoaded. Outdated in Unity 5.4
        /// </summary>
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
        #endif

        void CalledOnLevelWasLoaded(int level)
        {
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }
        }

        #endregion

        #region Custom

        /// <summary>
        /// Processes the inputs. Maintain a flag representing when the user is pressing Fire.
        /// </summary>
        void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!isFiring)
                {
                    isFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (isFiring)
                {
                    isFiring = false;
                }
            }
        }
        #endregion

        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(isFiring);
                stream.SendNext(Health);
            }
            else
            {
                // Network player, receive data
                this.isFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
            }
        }

        #endregion
    }
}