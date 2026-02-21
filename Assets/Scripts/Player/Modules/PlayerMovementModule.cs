using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Addons.SimpleKCC;
using Sirenix.OdinInspector;
using System;
using System.Threading;
using UnityEngine;

public class PlayerMovementModule : PlayerModuleBase, IInputListener, INetworkRender
{
    [SerializeField, BoxGroup("Settings")] private PlayerMovementSettingsScriptable movementSettings;

    public SimpleKCC KCC { get; private set; }

    [Networked]
    private Vector3 currentVelocity { get; set; }

    [Networked]
    public NetworkBool IsFloatingOnAir { get; set; }

    [Networked]
    public int NetworkedJumpCount { get; set; }

    private int visibleJumpCount;


    [Networked]
    public int NetworkedLandCount { get; set; }

    private int visibleLandCount;


    [Networked]
    public int NetworkedMoveCount { get; set; }

    private int visibleMoveCount;

    #region Actions

    public delegate void OnPlayerMovedDelegate(Vector2 lookRotation, Vector3 moveVelocity);

    public event Action OnPlayerJump;

    public event Action OnPlayerLanded;

    public event OnPlayerMovedDelegate OnPlayerMoved;

    #endregion

    protected override UniTask InitializeModule(CancellationToken cancellationToken)
    {
        KCC = GetComponent<SimpleKCC>();
        if (KCC == null)
        {
            Debug.LogError($"SimpleKCC component not found on PlayerMovementModule of {gameObject.name}");
            return UniTask.CompletedTask;
        }

        PlayerController.HealthController.OnDied += OnPlayerDied;
        return UniTask.CompletedTask;
    }

    public void RegisterInputListener()
    {
        PlayerController.OnInputRecieved += OnInputRecieved;
    }

    private void OnInputRecieved(in NetworkedInputData networkInputData, bool hasInput)
    {
        if (hasInput == false)
        {
            ProcessNoInput();
            return;
        }

        ProcessInput(networkInputData);
    }

    private void OnPlayerDied() { }

    public void ProcessInput(NetworkedInputData networkInputData)
    {
        float lookRotationY = PlayerController.CameraController.CameraTransform.eulerAngles.y;
        float jetpackImpulse = CalculateFlightImpulse(networkInputData);
        Vector3 desiredMoveVelocity = CalculateMovementDirection(networkInputData.MoveDirection, lookRotationY);

        bool isFastFall = PlayerController.IsButtonPressedContinuously(networkInputData.Buttons, InputButtonType.FlyDown);

        MovePlayer(desiredMoveVelocity, jetpackImpulse, isFastFall, networkInputData.Buttons);
    }

    private void ProcessNoInput()
    {
        MovePlayer();
    }

    private void MovePlayer(Vector3 desiredMoveVelocity = default, float jetpackImpulse = 0, bool isFastFall = false, NetworkButtons buttons = default)
    {
        bool isGrounded = KCC.IsGrounded;

        float gravity = movementSettings.GetGravity(jetpackImpulse > 0);
        if (isFastFall && jetpackImpulse <= 0f)
        {
            gravity *= movementSettings.DownGravityMultiplier;
        }
        KCC.SetGravity(gravity);

        bool hasInput = desiredMoveVelocity != Vector3.zero;
        float acceleration = GetAcceleration(hasInput);

        if (hasInput)
        {
            var currentRotation = KCC.TransformRotation;
            var targetRotation = Quaternion.LookRotation(desiredMoveVelocity);
            var nextRotation = Quaternion.Lerp(currentRotation, targetRotation, movementSettings.RotationSpeed * Runner.DeltaTime);
            KCC.SetLookRotation(nextRotation.eulerAngles);
        }

        currentVelocity = Vector3.Lerp(currentVelocity, desiredMoveVelocity, acceleration * Runner.DeltaTime);

        // Hız limitleri (normal uçuş)
        Vector3 realVel = KCC.RealVelocity;
        if (realVel.y > movementSettings.FlyMaxUpSpeed && jetpackImpulse > 0f)
        {
            jetpackImpulse = 0f;
        }
        if (realVel.y < -movementSettings.FlyMaxDownSpeed && jetpackImpulse < 0f)
        {
            jetpackImpulse = 0f;
        }

        KCC.Move(currentVelocity, jetpackImpulse);

        if (IsFloatingOnAir && isGrounded)
        {
            NetworkedLandCount++;
        }

        if (jetpackImpulse != 0)
        {
            NetworkedJumpCount++;
        }

        NetworkedMoveCount++;
        IsFloatingOnAir = !isGrounded;
    }

    public void NetworkRender()
    {
        if (visibleJumpCount < NetworkedJumpCount)
        {
            OnPlayerJump?.Invoke();
            visibleJumpCount = NetworkedJumpCount;
        }
        if (visibleLandCount < NetworkedLandCount)
        {
            OnPlayerLanded?.Invoke();
            visibleLandCount = NetworkedLandCount;
        }
        if (visibleMoveCount < NetworkedMoveCount)
        {
            OnPlayerMoved?.Invoke(KCC.GetLookRotation(true, false), currentVelocity);
            visibleMoveCount = NetworkedMoveCount;
        }
    }

    #region HELPER METHODS

    private float CalculateFlightImpulse(in NetworkedInputData data)
    {
        // Hover modu kaldırıldı
        float dt = Runner.DeltaTime;
        float vy = KCC.RealVelocity.y;

        // Yukarı itiş: max yükseliş hızını aşmadan artır
        if (PlayerController.IsButtonPressedContinuously(data.Buttons, InputButtonType.FlyUp))
        {
            float maxStep = movementSettings.FlyUpThrustPerSecond * dt;
            float needed = movementSettings.FlyMaxUpSpeed - vy; // pozitifse ihtiyaç var
            if (needed <= 0f)
                return 0f;
            return Mathf.Min(maxStep, needed);
        }

        // Aşağı itiş: sadece yukarı çıkarken (vy>0) momentum kırmak için limitli uygula.
        if (PlayerController.IsButtonPressedContinuously(data.Buttons, InputButtonType.FlyDown) && movementSettings.FlyDownThrustPerSecond > 0f)
        {
            if (vy > 0f)
            {
                float maxStep = movementSettings.FlyDownThrustPerSecond * dt; // uygulanabilecek en fazla aşağı itiş
                float needToZero = -vy; // negatif değer (yukarı ivmeyi sıfırlamak)
                float desired = needToZero;
                return Mathf.Clamp(desired, -maxStep, 0f);
            }
            // Zaten aşağı iniyorsa thrust verme; hızlı düşüş gravity multiplier ile sağlanacak
        }

        return 0f;
    }

    private Vector3 CalculateMovementDirection(Vector2 inputDirection, float lookRotationY)
    {
        var lookRotation = Quaternion.Euler(0f, lookRotationY, 0f);
        return lookRotation * new Vector3(inputDirection.x, 0f, inputDirection.y);
    }

    public float GetAcceleration(bool hasInput)
    {
        bool isGrounded = KCC.IsGrounded;
        if (hasInput)
            return isGrounded ? movementSettings.GroundAcceleration : movementSettings.AirAcceleration;
        else
            return isGrounded ? movementSettings.GroundDeceleration : movementSettings.AirDeceleration;
    }

    public float GetMovementSpeed(bool isFastMove)
    {
        float speed = isFastMove ? movementSettings.FastMovementSpeed : movementSettings.MovementSpeed;
        if (!KCC.IsGrounded)
        {
            speed *= movementSettings.FlySpeedMultiplier;
        }
        return speed;
    }

    public override void ResetModule()
    {
        KCC.SetActive(true);
        // Hover modu kaldırıldığı için ekstra reset gerekmez
    }

    #endregion
}
