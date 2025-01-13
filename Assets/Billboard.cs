using UnityEngine;

public class Billboard : MonoBehaviour
{
    
private Transform _cameraTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookAt = _cameraTransform.forward;
        lookAt.y = 0;
        transform.rotation = Quaternion.LookRotation(lookAt);
    }
}
