using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int msf_hitToKill = 2;
    [SerializeField] private int msf_rewardAmountPerKill = 50;

    private bool m_isDestroyed = false;
    public void TakeHit(int _damage)
    {
        msf_hitToKill -= _damage;
        if (msf_hitToKill <= 0 && !m_isDestroyed)
        {
            EnemySpawner.e_onEnemyDestroyed.Invoke();
            LevelManager.s_instance.IncreaseCurrency(msf_rewardAmountPerKill);
            m_isDestroyed = true;
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
