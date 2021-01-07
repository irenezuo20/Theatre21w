using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AvatarManager : MonoBehaviourPun, Photon.Pun.IPunObservable
{
    public GameObject avatar;
    public GameObject leftHand;

    public Transform playerGlobal;
    public Transform playerLocal;
    public Transform leftLocal;

    private PhotonView PV;

    void Start()
    {
        Debug.Log("I'm instantiated");

        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            Debug.Log("player is mine");

            playerGlobal = GameObject.Find("OVRPlayerController").transform;
            playerLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
            leftLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor");

            this.transform.SetParent(playerGlobal);
            this.transform.localPosition = new Vector3(0f, 0f, 0f);

            avatar.transform.SetParent(playerLocal);
            leftHand.transform.SetParent(leftLocal);
            //GetComponent<Recorder>().enabled = true;

        }
    }




    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);
            stream.SendNext(leftLocal.localPosition);
            stream.SendNext(leftLocal.localRotation);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
            leftHand.transform.localPosition = (Vector3)stream.ReceiveNext();
            leftHand.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
