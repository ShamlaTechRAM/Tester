using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.VoicePro;
using FrostweepGames.Plugins.Native;

public class SoundInput : MonoBehaviour
{
    public Recorder recorder;
    public Listener listener;
    public bool isRecording;
    public bool buttonToggle;

    public Button muteMyClientButton;
    private bool muteMyClientToggle;

    void Awake() {
        // buttonToggle = false;
        muteMyClientToggle = false;
    }

    void Start() {
        UpdateMicrophone();
        muteMyClientButton.onClick.AddListener(MuteMyClientButtonClicked);
    }

    void Update() {
        // if (buttonToggle == true && !isRecording) {
        //     StartRecord();
        // } else if (buttonToggle == false && isRecording) {
        //     StopRecord();
        // }
    }

    // public void Record() {
    //     if (Input.GetKeyDown(KeyCode.R) && !isRecording) {
    //         StartRecord();
    //     } else if (Input.GetKeyUp(KeyCode.R) && isRecording) {
    //         StopRecord();
    //     }
    // }

    // public void StartRecord() {
    //     recorder.RefreshMicrophones();
    //     Debug.Log("Refreshed microphone list");
    //     recorder.StartRecord();
    //     isRecording = true;
    //     Debug.Log("Is currently recording");
    // }

    // public void StopRecord() {
    //     recorder.StopRecord();
    //     isRecording = false;
    //     Debug.Log("Recording stopped");
    // }

    private void MuteMyClientButtonClicked() {
        muteMyClientToggle = !muteMyClientToggle;
        if (muteMyClientToggle) {
            UpdateMicrophone();
            if (!NetworkRouter.Instance.ReadyToTransmit || !recorder.StartRecord()) {
                //muteMyClientToggle.isOn = false;
                recorder.StartRecord();
                //Debug.Log("True 1");
            }
            Debug.Log("True 2");
        } else {
            recorder.StopRecord();
            //Debug.Log("recorder.StopRecord();");
            //Debug.Log("False");
        }
    }

    public void UpdateMicrophone() {
        recorder.RefreshMicrophones();
        Debug.Log("Refreshed mics");
        foreach (var device in CustomMicrophone.devices)
        {
            Debug.Log("Name: " + device);
        }
        recorder.SetMicrophone(CustomMicrophone.devices[0]);
        Debug.Log("Active mic: " + CustomMicrophone.devices[0]);
    }
}
