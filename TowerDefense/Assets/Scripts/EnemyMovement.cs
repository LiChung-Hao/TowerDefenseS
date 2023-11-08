using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Referencies")]
    [SerializeField] private Rigidbody2D msf_rb;

    [Header("Attributes")]
    [SerializeField] private float msf_moveSpeed = 2f;

    private Transform m_target;
    private int i_indexPath = 0;
    private float m_baseSpeed;
    private void Start()
    {
        m_baseSpeed = msf_moveSpeed;
        m_target = LevelManager.s_instance.mp_pathPoint[i_indexPath];
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(m_target.position, transform.position) < 0.1f) //move to next point
        { 
            i_indexPath++;
        }
        if (i_indexPath == LevelManager.s_instance.mp_pathPoint.Length) //reach end point
        {
            LevelManager.s_enemyEscaped++;
            EnemySpawner.e_onEnemyDestroyed.Invoke(); //call the event
            Destroy(gameObject);
            return;
        }
        else
        {
            m_target = LevelManager.s_instance.mp_pathPoint[i_indexPath];
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (m_target.position - transform.position).normalized;
        msf_rb.velocity = direction * msf_moveSpeed;
    }

    public void UpdateSpeed(float _newSpeed)
    {
        msf_moveSpeed = _newSpeed;
    }

    public void ResetSpeed()
    {
        msf_moveSpeed = m_baseSpeed;
    }
}
