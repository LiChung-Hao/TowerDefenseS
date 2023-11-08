using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referencies")]
    [SerializeField] private GameObject[] msf_enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int msf_baseEnemies = 8;
    [SerializeField] private float msf_enemyPerSecBase = 0.5f;
    [SerializeField] private float msf_enemyPerSecCap = 15f;
    [SerializeField] private float msf_timeBetweenWaves = 5f;
    [SerializeField] private float msf_difficultyScaler = 0.75f;

    [Header("Events")]
    public static UnityEvent e_onEnemyDestroyed = new UnityEvent();

    private int m_currentWave = 1;
    private float m_timeSinceLastSpawn;
    private float m_eps;
    private int m_enemiesAlive;
    private int m_enemiesLeftToSpawn;
    private bool m_isSpawning = false;


    private void Awake()
    {
        e_onEnemyDestroyed.AddListener(EnemyDestroyed);
    }
    private void Start()
    {
        StartCoroutine(StartSpawning());
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_isSpawning)
        {
            return;
        }

        m_timeSinceLastSpawn += Time.deltaTime;

        if (m_timeSinceLastSpawn > 1f / m_eps && m_enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            m_timeSinceLastSpawn = 0;
        }

        if (m_enemiesAlive == 0 && m_enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(msf_timeBetweenWaves);
        m_isSpawning = true;
        m_enemiesLeftToSpawn = EnemyPerWave();
        m_eps = EnemyPerSecond();
    }

    void SpawnEnemy()
    {
        int i_enemyTypeIndex = Random.Range(0, msf_enemyPrefabs.Length);
        Instantiate(msf_enemyPrefabs[i_enemyTypeIndex], LevelManager.s_instance.mp_startPoint.position, Quaternion.identity);
        Debug.Log("Enemy spawnded!");
        m_enemiesAlive++;
        m_enemiesLeftToSpawn--;
    }

    private void EnemyDestroyed()
    {
        m_enemiesAlive--;
    }
    private int EnemyPerWave()
    { 
        return Mathf.RoundToInt(msf_baseEnemies * Mathf.Pow(m_currentWave, msf_difficultyScaler));
        //increase the difficulty:
        //wave1: 8*1^0.75
        //wave2: 8*2^0.75
    }

    private float EnemyPerSecond()
    {
        return Mathf.Clamp(msf_enemyPerSecBase * Mathf.Pow(m_currentWave, msf_difficultyScaler),0f, msf_enemyPerSecCap);
    }

    private void EndWave()
    {
        m_isSpawning = false;
        m_timeSinceLastSpawn = 0;
        m_currentWave++;
        StartCoroutine(StartSpawning());
    }
}
