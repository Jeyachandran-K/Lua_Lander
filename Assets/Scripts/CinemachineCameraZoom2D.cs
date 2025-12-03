using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
    public static CinemachineCameraZoom2D Instance {  get; private set; }

    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float targetOrthographicSize;
    private const float NORMAL_ORTHOGRAPHIC = 10f;

    private void Awake()
    {
        Instance = this;
    }

    public void SetTargetOrthographicSize(float targetOrthographicSize)
    {
        this.targetOrthographicSize = targetOrthographicSize;
    }
    public float GetTargetOrthographicSize()
    {
        return targetOrthographicSize;
    }
    private void Update()
    {
        float zoomSpeed = 2f;
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime*zoomSpeed);
    }
    public void SetNormalOrthographicSize()
    {
        SetTargetOrthographicSize(NORMAL_ORTHOGRAPHIC);
    }
    
}
