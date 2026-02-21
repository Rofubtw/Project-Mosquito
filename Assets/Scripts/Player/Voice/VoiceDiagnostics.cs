using UnityEngine;
using Photon.Voice.Unity;

public class VoiceDiagnostics : MonoBehaviour
{
    [SerializeField] private Recorder recorder;

    void Update()
    {
        if (!recorder) return;

        // Gönderim var mý?
        bool tx = recorder.IsCurrentlyTransmitting;

        // Anlýk seviye (0..1 civarý). Sessizlikte çok küçük olur.
        float level = recorder.LevelMeter != null ? recorder.LevelMeter.CurrentAvgAmp : 0f;

        // Basit görsel: Ekranýn sol üstünde yazdýr
        //Debug.Log($"[VOICE] Tx:{tx}  Level:{level:0.000}  Rec:{recorder.RecordingEnabled}  Send:{recorder.TransmitEnabled}");
    }
}
