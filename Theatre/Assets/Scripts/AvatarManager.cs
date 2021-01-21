using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AvatarManager : MonoBehaviourPun, Photon.Pun.IPunObservable
{
    public GameObject upperBody;
    public GameObject leftHand;
    public GameObject rightHand;

    public Transform playerGlobal;
    public Transform eyeLocal;
    public Transform leftLocal;
    public Transform rightLocal;

    private PhotonView PV;

    void Start()
    {
        Debug.Log("AvatarManager Start(): avatar instantiated");

        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            Debug.Log("player is mine");

            playerGlobal = GameObject.Find("OVRPlayerController").transform;
            eyeLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
            leftLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor");
            rightLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/RightHandAnchor");

            this.transform.SetParent(playerGlobal);
            upperBody.transform.SetParent(eyeLocal);
            leftHand.transform.SetParent(leftLocal);
            rightHand.transform.SetParent(rightLocal);

            upperBody.transform.localPosition = new Vector3(0f, 0f, 0f);
            this.transform.localPosition = new Vector3(0f, 0f, 0f);
            leftHand.transform.localPosition = new Vector3(0f, 0f, 0f);
            rightHand.transform.localPosition = new Vector3(0f, 0f, 0f);


            //GetComponent<Recorder>().enabled = true;

        }
    }




    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(eyeLocal.localPosition);
            stream.SendNext(eyeLocal.localRotation);
            stream.SendNext(leftLocal.localPosition);
            stream.SendNext(leftLocal.localRotation);
            stream.SendNext(rightLocal.localPosition);
            stream.SendNext(rightLocal.localRotation);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            upperBody.transform.localPosition = (Vector3)stream.ReceiveNext();
            upperBody.transform.localRotation = (Quaternion)stream.ReceiveNext();
            leftHand.transform.localPosition = (Vector3)stream.ReceiveNext();
            leftHand.transform.localRotation = (Quaternion)stream.ReceiveNext();
            rightHand.transform.localPosition = (Vector3)stream.ReceiveNext();
            rightHand.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
