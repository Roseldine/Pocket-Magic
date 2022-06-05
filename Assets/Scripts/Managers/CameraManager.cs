
using UnityEngine;
using Cinemachine;
using StardropTools.Tween;

public class CameraManager : StardropTools.Singletons.SingletonCoreManager<CameraManager>
{
    [Header("Cameras")]
    [SerializeField] Player player;
    [SerializeField] new Camera camera;
    [SerializeField] Transform camTransform;
    [SerializeField] CineVirtualCamera[] cameras;
    [SerializeField] float followSpeed = 7;
    [SerializeField] float noInputSpeed = 3;
    [SerializeField] float cameraOffset = 2;
    [SerializeField] float slowMove = 1.5f;
    [Space]
    [SerializeField] float minCamZ = 1.5f;

    [Header("Camera Effects")]
    [SerializeField] TweenCluster camShake;
    //[SerializeField] GameObject psSpeed;

    Vector3 targetPos;
    Vector3 pivotStartPos, pivotStartRot;
    CineVirtualCamera playCamera;


    public override void Initialize()
    {
        base.Initialize();
        SubscribeToEvents();
    }

    public override void SubscribeToEvents()
    {
        base.SubscribeToEvents();

        GameManager.Instance.OnPlayEnter.AddListener(GetPlayer);

        GameManager.Instance.OnIdleEnter.AddListener(() => ChangePriority(0));
        GameManager.Instance.OnIdleEnter.AddListener(() => playCamera.Position = Vector3.zero);
        GameManager.Instance.OnPlayEnter.AddListener(() => ChangePriority(1));
        GameManager.Instance.OnFinishEnter.AddListener(() => ChangePriority(1));

        //GameManager.Instance.OnPlayUpdate.AddListener(FollowPlayer);
        //PlayerManager.Instance.OnLimitReached.AddListener(LimitReached);
    }

    public override void LateInitialize()
    {
        base.LateInitialize();

        playCamera = cameras[1];

        pivotStartPos = playCamera.pivot.localPosition;
        pivotStartRot = playCamera.pivot.localEulerAngles;
    }

    void GetPlayer()
    {
        if (player == null)
            player = PlayerManager.Instance.Player;

        GameManager.Instance.OnPlayEnter.RemoveListener(GetPlayer);
    }

    void FollowPlayer()
    {
        if (SingleInputManager.Instance.HasInput)
        {
            targetPos = player.Position + SingleInputManager.Instance.DirectionXZ * cameraOffset;
            targetPos.z = Mathf.Clamp(targetPos.z, minCamZ, Mathf.Infinity);
            targetPos.x = 0;
            playCamera.Position = Vector3.Lerp(playCamera.Position, targetPos, Time.deltaTime * followSpeed);
        }

        else
        {
            targetPos = player.Position;
            targetPos.z = Mathf.Clamp(targetPos.z, minCamZ, Mathf.Infinity);
            targetPos.x = 0;
            playCamera.Position = Vector3.Lerp(playCamera.Position, targetPos, Time.deltaTime * noInputSpeed);
        }
    }

    public void ChangePriority(int cameraID)
    {
        for (int i = 0; i < cameras.Length; i++)
            if (i != cameraID)
                cameras[i].SetPriority(0);

        cameras[cameraID].SetPriority(1);
    }

    public void CameraShake()
    {
        playCamera.LocalRotationEuler = pivotStartRot;
        camShake.PlayTweens();
    }

    //public void ActivateSpeedEffect(bool value)
    //    => psSpeed.SetActive(value);

    public void ResetPositions()
    {

    }


    protected override void OnValidate()
    {
        base.OnValidate();

        if (cameras.Exists())
        {
            foreach (CineVirtualCamera cam in cameras)
                if (cam.gameObject != null)
                {
                    cam.pivot = cam.gameObject.transform;
                    cam.cameraTransform = cam.pivot.GetChild(0);
                    cam.camera = cam.cameraTransform.GetComponent<CinemachineVirtualCamera>();
                }
        }
    }
}

[System.Serializable]
public class CineVirtualCamera
{
    public GameObject gameObject;
    public Transform pivot;
    public Transform cameraTransform;
    public CinemachineVirtualCamera camera;

    public Vector3 Position { get => pivot.position; set => pivot.position = value; }
    public Vector3 RotationEuler { get => pivot.eulerAngles; set => pivot.eulerAngles = value; }

    public Vector3 LocalPosition { get => pivot.localPosition; set => pivot.localPosition = value; }
    public Vector3 LocalRotationEuler { get => pivot.localEulerAngles; set => pivot.localEulerAngles = value; }

    public void SetPriority(int value)
        => camera.Priority = value;

    public void SetActive(bool value)
        => gameObject.SetActive(value);
}
