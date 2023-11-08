using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [Header("Referencies")]
    [SerializeField] private Transform msf_cannonRotatePoint;
    [SerializeField] private LayerMask msf_enemyMask;
    [SerializeField] private GameObject msf_bulletPrefabs;
    [SerializeField] private Transform msf_firingPoint;
    [SerializeField] private GameObject msf_upgradeUI;
    [SerializeField] private Button msf_upgradeButton;
    [SerializeField] private GameObject msf_gunBase;

    [Header("Attributes")]
    [SerializeField] private float msf_targettingRange = 2f;
    [SerializeField] private float msf_rotationSpeed = 200f;
    [SerializeField] private float msf_bulletPerSec = 1f;
    [SerializeField] private int msf_upgradeCost = 40;

    private Transform m_target;
    private float m_timeUntilFire;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_target == null)
        {
            FindTarget();
            return;
        }

        RotateTorwardTarget();

        if (!CheckTargetIsInRange())
        {
            m_target = null;
        }
        else 
        {
            m_timeUntilFire += Time.deltaTime;
            if (m_timeUntilFire > 1 / msf_bulletPerSec)
            {
                Shoot();
                m_timeUntilFire = 0;
            }
        }
    }
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll
            (transform.position, msf_targettingRange, (Vector2)transform.position, 0f, msf_enemyMask);
        
        if (hits.Length > 0) //means there are targets within the range
        {
            m_target = hits[0].transform; //set the first one in the array as the target
        }
    }

    private void RotateTorwardTarget()
    {
        float angle = Mathf.Atan2
            (m_target.position.y - transform.position.y, m_target.position.x - transform.position.x)
            * Mathf.Rad2Deg + 180f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
        msf_cannonRotatePoint.rotation = Quaternion.RotateTowards(msf_cannonRotatePoint.rotation, targetRotation, msf_rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector3.Distance(m_target.position, transform.position) <= msf_targettingRange;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(msf_bulletPrefabs, msf_firingPoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetTarget(m_target);
    }
    public void Upgrade()
    {
        if (msf_upgradeCost > LevelManager.s_instance.mp_currency)
        {
            return;
        }
        msf_gunBase.GetComponent<SpriteRenderer>().color = Color.red;
        msf_rotationSpeed = 550;
        gameObject.tag = "Upgraded";
    }
    public void OpenUpgradeUI()
    {
        msf_upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        msf_upgradeUI.SetActive(false);
    }
    //private void OnDrawGizmos() //to visualize the range
    //{
    //    Handles.color = Color.cyan;
    //    Handles.DrawWireDisc(transform.position, transform.forward, msf_targettingRange);
    //}
}
