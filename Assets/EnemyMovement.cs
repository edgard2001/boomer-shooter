using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private Transform _playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = _playerTransform.position + Vector3.up * 2 - transform.position;
        if (displacement.sqrMagnitude > 2f)
            transform.position += displacement.normalized * (speed * Time.deltaTime);
    }
}
