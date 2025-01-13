using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform inertialTiltPivot;
    [SerializeField] private float inertialTiltStrength = 5f;
    [SerializeField] private float inertialTiltResponsiveness = 5f;
    
    private Rigidbody _rb;
    [SerializeField] private float speed = 5f;
    
    [SerializeField] private Transform cameraTransform;
    private float _cameraPitch = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inertialTiltPivot.rotation = Quaternion.identity;
        
        _rb = GetComponent<Rigidbody>();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = inertialTiltPivot.localRotation;
        if (Input.GetKey(KeyCode.A))
        {
            inertialTiltPivot.localRotation = Quaternion.Lerp(rotation, Quaternion.Euler(0, rotation.y, inertialTiltStrength), Time.deltaTime * inertialTiltResponsiveness);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inertialTiltPivot.localRotation = Quaternion.Lerp(rotation, Quaternion.Euler(0, rotation.y, -inertialTiltStrength), Time.deltaTime * inertialTiltResponsiveness);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            inertialTiltPivot.localRotation = Quaternion.Lerp(rotation, Quaternion.Euler(inertialTiltStrength, rotation.y, 0), Time.deltaTime * inertialTiltResponsiveness);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inertialTiltPivot.localRotation = Quaternion.Lerp(rotation, Quaternion.Euler(-inertialTiltStrength, rotation.y, 0), Time.deltaTime * inertialTiltResponsiveness);
        }
        else
        {
            inertialTiltPivot.localRotation = Quaternion.Lerp(rotation, Quaternion.Euler(0, rotation.y, 0), Time.deltaTime * inertialTiltResponsiveness);
        }
        
        Vector2 velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        _rb.linearVelocity = velocity.x * transform.right + _rb.linearVelocity.y * transform.up + velocity.y * transform.forward;

        Quaternion cameraRotation = cameraTransform.rotation;
        _cameraPitch -= Input.GetAxisRaw("Mouse Y");
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, cameraRotation.y, cameraRotation.z);
        
        
        transform.rotation *= Quaternion.Euler(0, Input.GetAxisRaw("Mouse X"), 0);
    }
}
