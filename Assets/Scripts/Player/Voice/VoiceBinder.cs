using Fusion;
using Photon.Voice.Unity;
using UnityEngine;

public class VoiceBinder : NetworkBehaviour
{
    [SerializeField] private Recorder recorder;
    [SerializeField] private Speaker speaker;

    public override void Spawned()
    {
        bool isLocal = Object.HasInputAuthority;

        if (recorder)
        {
            // Yalnýzca yerelde mikrofonu aç
            recorder.RecordingEnabled = isLocal;     // Start/Stop recording için DOÐRU yöntem
            recorder.TransmitEnabled = false;       // PTT yapacaksan false baþlat (veya true)
            // recorder.RestartRecording();          // Ýsteðe baðlý; RecordingEnabled true ise zaten tetikler
        }

        if (speaker && speaker.TryGetComponent(out AudioSource src))
        {
            src.spatialBlend = 1f;
            src.dopplerLevel = 0f;
        }
    }
}
