using System;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

[Serializable]
public class Tower
{
    public string m_name;
    public int m_cost;
    public GameObject m_prefab;

    public Tower(string _name, int _cost, GameObject _prefab)
    {
        m_name = _name;
        m_cost = _cost;
        m_prefab = _prefab;
    }
}
