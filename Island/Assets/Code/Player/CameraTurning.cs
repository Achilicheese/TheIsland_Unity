using UnityEngine;

public class CameraTurning : MonoBehaviour
{
    [SerializeField] private float xSpeed = 100f;
    [SerializeField] private float ySpeed = 100f;
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
        float _rotRightLeft = Input.GetAxis("Mouse X") * xSpeed;
        float _rotUpDown = Input.GetAxis("Mouse Y") * ySpeed;

        _rotateX -= _rotUpDown;
        _rotateX = Mathf.Clamp(_rotateX, -90f, 70f);
        transform.localRotation = Quaternion.Euler(_rotateX, 0f, 0f);     
        _playerBody.Rotate(Vector3.up * _rotRightLeft);
    }
}