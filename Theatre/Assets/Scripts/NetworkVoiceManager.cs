using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;
using System;


[RequireComponent(typeof(VoiceConnection))]
public class NetworkVoiceManager : MonoBehaviour
{
    public Transform remoteVoiceParent;
    private VoiceConnection voiceConnection;

    void Awake()
    {
        voiceConnection = GetComponent<VoiceConnection>();
    }

    private void OnEnable()
    {
        voiceConnection.SpeakerLinked += this.OnSpeakerCreated;

    }

    private void OnDisable()
    {
        voiceConnection.SpeakerLinked -= this.OnSpeakerCreated;
    }

    private void OnSpeakerCreated(Speaker speaker)
    {
        speaker.transform.SetParent(this.remoteVoiceParent);
        speaker.OnRemoteVoiceRemoveAction += OnRemoteVoiceRemove;
        Debug.Log("speaker is created");
    }

    private void OnRemoteVoiceRemove(Speaker speaker)
    {
        if (speaker != null)
        {
            Destroy(speaker.gameObject);
        }
    }
}