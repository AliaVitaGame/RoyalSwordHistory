using UnityEngine;
using UnityEngine.UI;

public class MenuLevelsCameraController : MonoBehaviour
{
    [SerializeField] private float speedCameraZoom = 50;
    [SerializeField] private float minZoom = 60, maxZoom = 110;
    [SerializeField] private Camera cameraMain;
    [Header("Speed move")]
    [SerializeField] private float minSpeedCameraMove = 3;
    [SerializeField] private float maxSpeedCameraMove = 30;
    [SerializeField] private Slider speedCameraMoveSettings;
    [Header("DeadLine Camera")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private float _inputX;
    private float _inputY;
    private bool _isDesctop = true;

    private Vector3 _startCameraPosition;
    private Transform _cameraTransform;

    private static float _speedCameraMove = 10;

    private void OnEnable()
    {
        PlatformManager.IsDesctopEvent += SetPlatform;
    }

    private void OnDisable()
    {
        PlatformManager.IsDesctopEvent -= SetPlatform;
    }

    private void Start()
    {
        if (cameraMain == null)
            cameraMain = Camera.main;

        _cameraTransform = cameraMain.transform;
        _startCameraPosition = _cameraTransform.position;

        speedCameraMoveSettings.minValue = minSpeedCameraMove;
        speedCameraMoveSettings.maxValue = maxSpeedCameraMove;

        RefreshUI();
    }

    private void FixedUpdate()
    {
        if (_isDesctop)
        {
            SetHorizontal(Input.GetAxis("Horizontal"));
            SetVertical(Input.GetAxis("Vertical"));
            ZoomMouse();
        }

        Vector3 directionMove = _cameraTransform.position + new Vector3(_inputX, _inputY) * GetSpeedMoveCamera();
        directionMove.x = Mathf.Clamp(directionMove.x, minX, maxX);
        directionMove.y = Mathf.Clamp(directionMove.y, minY, maxY);
        _cameraTransform.position = directionMove;
    }

    private void Update()
    {
        if (_isDesctop)
        {
            ZoomMouse();
        }
    }

    public void ZoomMouse()
    {
        AddZoomCamera(Input.GetAxis("Mouse ScrollWheel") * speedCameraZoom);
    }

    public void RefreshUI()
    {
        speedCameraMoveSettings.value = _speedCameraMove;
    }

    public void SetHorizontal(float x) => _inputX = x;
    public void SetVertical(float y) => _inputY = y;
    public void SetSpeedMoveCamera(float value) => _speedCameraMove = value;
    public void AddZoomCamera(float zoom) => cameraMain.fieldOfView = Mathf.Clamp(cameraMain.fieldOfView - zoom, minZoom, maxZoom);
    private void SetPlatform(bool value) => _isDesctop = value;
    private float GetSpeedMoveCamera() => _speedCameraMove * Time.fixedDeltaTime;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        float size = 0.5f;
        Gizmos.DrawSphere(_startCameraPosition + Vector3.right * minX, size);
        Gizmos.DrawSphere(_startCameraPosition + Vector3.right * maxX, size);
        Gizmos.DrawSphere(_startCameraPosition + Vector3.up * minY, size);
        Gizmos.DrawSphere(_startCameraPosition + Vector3.up * maxY, size);
    }
}
