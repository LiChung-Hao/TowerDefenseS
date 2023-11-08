using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager s_instance;

    private bool m_isHoveringUI;
    private void Awake()
    {
        s_instance = this;
    }

    public void SetHoveringState(bool _state)
    {
        m_isHoveringUI = _state;
    }

    public bool IsHoveringUI()
    { 
        return m_isHoveringUI;
    }
}
