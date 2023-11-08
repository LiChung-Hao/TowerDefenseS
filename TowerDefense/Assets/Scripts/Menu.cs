using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("Referencies")]
    [SerializeField] private TextMeshProUGUI msf_currencyUI;
    [SerializeField] private Animator msf_animator;

    private bool m_menuOn = true;
    private void OnGUI()
    {
        msf_currencyUI.text = LevelManager.s_instance.mp_currency.ToString();
    }

    public void MenuOnOff()
    {
        m_menuOn = !m_menuOn;
        msf_animator.SetBool("MenuOpen", m_menuOn);
    }
}
