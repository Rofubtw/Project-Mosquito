using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraController : PlayerModuleBase
{
	[SerializeField] private CinemachineCamera cinemachineCamera;
	[SerializeField] private CinemachineOrbitalFollow cinemachineOrbitalFollow;
	[SerializeField] private PlayerCameraSettingsScriptable cameraSettings;
	
	private Vector2 _smoothedDelta;
	private Vector2 _pendingLookDelta; // LateUpdate icin buffer
	private bool _hasPendingLook;
	private CursorLockMode _lastCursorLock; // lock state takip
	public Camera Camera => Camera.main;
	public Transform CameraTransform => Camera.transform;

    protected override UniTask InitializeModule(CancellationToken cancellationToken)
    {
		SetCameras();
		_lastCursorLock = Cursor.lockState;
		return UniTask.CompletedTask;
    }

	public override void SetupForLocalPlayer()
	{
		base.SetupForLocalPlayer();
		SetCameras();
		_lastCursorLock = Cursor.lockState;
		PlayerController.InputHandler.OnLookInputReceived += OnLookInput; // RefreshCamera yerine bufferla
		PlayerController.HealthController.OnDied += OnPlayerDied;
	}

	private void OnDestroy()
	{
		PlayerController.InputHandler.OnLookInputReceived -= OnLookInput;
		PlayerController.HealthController.OnDied -= OnPlayerDied;
	}

	private void LateUpdate()
	{
		// Cursor lock durum değişti mi? Smoothing buffer ve pending input temizle
		if (Cursor.lockState != _lastCursorLock)
		{
			_smoothedDelta = Vector2.zero;
			_pendingLookDelta = Vector2.zero;
			_hasPendingLook = false;
			_lastCursorLock = Cursor.lockState;
		}

		// Kamerayi LateUpdate'te uygula
		if (_hasPendingLook)
		{
			RefreshCamera(_pendingLookDelta);
			_pendingLookDelta = Vector2.zero;
			_hasPendingLook = false;
		}
	}

	private void OnLookInput(Vector2 delta)
	{
		// Bir frame icinde birden fazla cagri olursa topla
		_pendingLookDelta += delta;
		_hasPendingLook = true;
	}

	private void OnPlayerDied()
	{
		if (cinemachineCamera)
			cinemachineCamera.enabled = false;
	}

	private void SetCameras()
	{
		if (!cinemachineCamera || cameraSettings == null) return;
		if (HasInputAuthority)
		{
			cinemachineCamera.Priority = cameraSettings.LocalPriority;
			cinemachineCamera.enabled = true;
			if (Camera) Camera.enabled = true;
			if (cameraSettings.DisableRecentering && cinemachineOrbitalFollow != null)
			{
				cinemachineOrbitalFollow.HorizontalAxis.Recentering.Enabled = false;
				cinemachineOrbitalFollow.VerticalAxis.Recentering.Enabled = false;
				cinemachineOrbitalFollow.RadialAxis.Recentering.Enabled = false;
			}
		}
		else
		{
			cinemachineCamera.Priority = cameraSettings.RemotePriority;
		}
	}

	private void RefreshCamera(Vector2 playerLookRotation)
	{
		if (!HasInputAuthority || cinemachineOrbitalFollow == null || cameraSettings == null) return;

		float dt = cameraSettings.UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
		Vector2 rawDelta = playerLookRotation;
		if (cameraSettings.ApplyDeltaTime)
		{
			rawDelta *= dt;
		}

		Vector2 workingDelta = rawDelta;
		if (cameraSettings.LookSmoothing > 0f)
		{
			float alpha = 1f - Mathf.Exp(-cameraSettings.LookSmoothing * dt);
			_smoothedDelta = Vector2.Lerp(_smoothedDelta, rawDelta, alpha);
			workingDelta = _smoothedDelta;
		}

		float pitchDelta = workingDelta.x * cameraSettings.PitchSensitivity * (cameraSettings.InvertY ? -1f : 1f);
		float yawDelta = workingDelta.y * cameraSettings.YawSensitivity;

		var hAxis = cinemachineOrbitalFollow.HorizontalAxis;
		var vAxis = cinemachineOrbitalFollow.VerticalAxis;

		float newYaw = hAxis.Value + yawDelta;
		newYaw = hAxis.ClampValue(newYaw);
		hAxis.Value = newYaw;
		hAxis.CancelRecentering();
		cinemachineOrbitalFollow.HorizontalAxis = hAxis;

		float minPitch = cameraSettings.ClampPitch ? cameraSettings.PitchMin : vAxis.Range.x;
		float maxPitch = cameraSettings.ClampPitch ? cameraSettings.PitchMax : vAxis.Range.y;
		float newPitch = vAxis.Value + pitchDelta;
		newPitch = Mathf.Clamp(newPitch, minPitch, maxPitch);
		vAxis.Value = newPitch;
		vAxis.CancelRecentering();
		cinemachineOrbitalFollow.VerticalAxis = vAxis;
	}

	public override void ResetModule()
    {
		SetCameras();
		_smoothedDelta = Vector2.zero;
		_pendingLookDelta = Vector2.zero;
		_hasPendingLook = false;
		_lastCursorLock = Cursor.lockState;
	}
}
