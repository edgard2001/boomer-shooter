using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private Transform _playerTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 10, 5);
        
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
