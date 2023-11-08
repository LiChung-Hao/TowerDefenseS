using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Referencies")]
    [SerializeField] private Rigidbody2D msf_rb;

    [Header("Attributes")]
    [SerializeField] private float msf_bulletSpeed = 5f;
    [SerializeField] private int msf_bulletDamage = 1;

    private Transform m_target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_target == null)
        {
            Destroy(gameObject); //the target to trace has been destroyed
            return;
        }
        Vector2 dirction = (m_target.position - transform.position).normalized;

        msf_rb.velocity = dirction * msf_bulletSpeed;
    }

    public void SetTarget(Transform _target)
    {
        m_target = _target;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        _collision.gameObject.GetComponent<EnemyHealth>().TakeHit(msf_bulletDamage);
        Destroy(gameObject); //take health from enemy
    }
}
