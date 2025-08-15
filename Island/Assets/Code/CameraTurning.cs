using UnityEngine;

public class CameraTurning : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float xSpeed = 2f;
    [SerializeField] float ySpeed = 2f;
    [SerializeField] float yMinLimit = -90f;
    [SerializeField] float yMaxLimit = 90f;
    public Transform _playerBody;

    private float _rotUpDown;
    private float _rotRightLeft;
    private float _rotateX = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        _rotRightLeft = Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        _rotUpDown = Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        _rotateX -= _rotUpDown;
        _rotateX = Mathf.Clamp(_rotateX, yMinLimit, yMaxLimit);

        transform.localRotation = Quaternion.Euler(_rotateX, 0f, 0f);
        _playerBody.Rotate(Vector3.up * _rotRightLeft);
    }
}