using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 100f;
    
    [SerializeField] private Light muzzleFlash;
    [SerializeField] private float muzzleFlashStrength = 100f;
    [SerializeField] private float muzzleFlashDuration = 0.1f;
    private Animator _animator;
    private bool _isShooting;
    
    [SerializeField] private Transform cameraTransform;
    
    [SerializeField] private LineRenderer lineRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        muzzleFlash.intensity = 0;
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isShooting && Input.GetButtonDown("Fire1"))
        {
            _isShooting = true;
            _animator.SetTrigger("Shoot");
        }
        
        Gradient gradient = new Gradient();
        float alpha = Mathf.Lerp(lineRenderer.colorGradient.alphaKeys[0].alpha, 0, Time.deltaTime * 3);
        gradient.SetKeys(lineRenderer.colorGradient.colorKeys, new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f) });
        lineRenderer.colorGradient = gradient;
    }

    public void OnShotFired()
    {
        muzzleFlash.intensity = muzzleFlashStrength;
        Invoke(nameof(EndFlash), muzzleFlashDuration);
        
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.05f);
        
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Health>().TakeDamage(damage);
            }
            
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            
            Gradient gradient = new Gradient();
            gradient.SetKeys(lineRenderer.colorGradient.colorKeys, new GradientAlphaKey[] { new GradientAlphaKey(0.3f, 0.0f) });
            lineRenderer.colorGradient = gradient;
        }
    }


    private void EndFlash()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.05f);
        muzzleFlash.intensity = 0;
        _isShooting = false;
    }
    
    public void OnShotEnd()
    {
        _isShooting = false;
    }
}
