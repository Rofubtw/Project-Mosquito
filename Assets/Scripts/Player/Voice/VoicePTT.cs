using Photon.Voice.Unity;
using UnityEngine;

public class VoicePTT : MonoBehaviour
{
    [SerializeField] private Recorder recorder;
    [SerializeField] private KeyCode pushToTalk = KeyCode.V;

    void Update()
    {
        if (!recorder) return;
       
        recorder.TransmitEnabled = Input.GetKey(pushToTalk);

    }

    public void ToggleMute(bool muted)
    {
        if (!recorder) return;

        recorder.TransmitEnabled = !muted;
    }
}
