using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager buildManager;

    [Header("Referencies")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] msf_towerPrefabs;

    private int i_selectedTowerIndex = 0;
    private void Awake()
    {
        buildManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tower GetSelectedTower()
    { 
        return msf_towerPrefabs[i_selectedTowerIndex];
    }

    public void SelectTower(int _selectedTowerIndex)
    {
        i_selectedTowerIndex = _selectedTowerIndex;
    }
}
