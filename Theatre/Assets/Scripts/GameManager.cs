﻿using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;



    public class GameManager : MonoBehaviourPunCallbacks
    {

    #region Public Fields
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;
    public GameObject OVRPlayerController;
    //public GameObject leftHand;
    //public GameObject rightHand;

    #endregion



    #region Photon Callbacks


    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }


        #endregion

        #region Private Methods
        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);

    }
    #endregion


    #region Public Methods


    public void LeaveRoom()
            {
                PhotonNetwork.LeaveRoom();
            }

    // public void JoinPracticeRoom()
    // {
    //     Scene sceneToLoad = SceneManager.GetSceneByName("Practice Room");
    //     SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Additive);
    //     SceneManager.MoveGameObjectToScene(OVRPlayerController, sceneToLoad);
    // }


    #endregion


    #region MonoBehaviour CallBacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        //else if (leftHand == null)
        //{
        //    Debug.LogError("<Color=Red><a>Missing</a></Color> leftHand Reference. Please set it up in GameObject 'Game Manager'", this);
        //}
        //else if (rightHand== null)
        //{
        //    Debug.LogError("<Color=Red><a>Missing</a></Color> rightHand Reference. Please set it up in GameObject 'Game Manager'", this);
        //}
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);

            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
           PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
           //PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);


        }
    }
    #endregion


    #region Photon Callbacks


    public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                //LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                //LoadArena();
            }
        }


        #endregion
    }

