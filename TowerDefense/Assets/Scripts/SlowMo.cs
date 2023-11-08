using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SlowMo : MonoBehaviour
{
    [Header("Referencies")]
    [SerializeField] private LayerMask msf_enemyMask;

    [Header("Attributes")]
    [SerializeField] private float msf_targettingRange = 2f;
    [SerializeField] private float msf_attackPerSec = 5f;
    [SerializeField] private float msf_freezeSec = 1.3f;

    private float m_timeUntilFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_timeUntilFire += Time.deltaTime;
        if (m_timeUntilFire > 1 / msf_attackPerSec)
        {
            FreezeInRange();
            m_timeUntilFire = 0;
        }
    }

    private void FreezeInRange()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll
           (transform.position, msf_targettingRange, (Vector2)transform.position, 0f, msf_enemyMask);

        for (int i = 0; i < hits.Length; i++)
        { 
            RaycastHit2D hit = hits[i];
            EnemyMovement enemyMovement = hit.transform.GetComponent<EnemyMovement>();
            enemyMovement.UpdateSpeed(0.5f);
            StartCoroutine(ResetEnemySpeed(enemyMovement));
        }
    }
    //private void OnDrawGizmos() //to visualize the range
    //{
    //    Handles.color = Color.cyan;
    //    Handles.DrawWireDisc(transform.position, transform.forward, msf_targettingRange);
    //}

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(msf_freezeSec);
        em.ResetSpeed();
    }
}
