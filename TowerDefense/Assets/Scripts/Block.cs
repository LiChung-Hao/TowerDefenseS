using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Referencies")]
    [SerializeField] private SpriteRenderer msf_sr;
    [SerializeField] private Color msf_hoverColor;

    private Color m_startColor;
    private GameObject m_tower;
    // Start is called before the first frame update
    void Start()
    {
        m_startColor = msf_sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        msf_sr.color = msf_hoverColor;
    }
    private void OnMouseExit()
    {
        msf_sr.color = m_startColor;
    }

    private void OnMouseDown()
    {
        if (UpgradeManager.s_instance.IsHoveringUI() || LevelManager.s_instance.mp_gameOver)
        { 
            return;
        }
        if (m_tower != null)
        {
            if (m_tower.tag!="Upgraded" && m_tower.tag != "SlowMo")
            {
                m_tower.GetComponent<Cannon>().OpenUpgradeUI();
            }
            return;
        }

        Tower towerToBuild = BuildManager.buildManager.GetSelectedTower();

        if (towerToBuild.m_cost > LevelManager.s_instance.mp_currency)
        {
            Debug.Log("You can't afford this!");
            return ;
        }

        LevelManager.s_instance.SpendCurrency(towerToBuild.m_cost);
        m_tower = Instantiate(towerToBuild.m_prefab, transform.position, Quaternion.identity);
    }
}
