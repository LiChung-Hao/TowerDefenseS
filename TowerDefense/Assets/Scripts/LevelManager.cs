using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager s_instance;
    public Transform mp_startPoint;
    public Transform[] mp_pathPoint;

    public static int s_enemyEscaped;
    public bool mp_gameOver = false;
    public int mp_currency;
    public GameObject mp_gameOverUI;
    private void Awake()
    {
        s_instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetScreenRatio(16, 7);
        mp_currency = 500;
        s_enemyEscaped = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (s_enemyEscaped > 3)
        {
            mp_gameOver = true;
            mp_gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKey(KeyCode.Escape))
        { 
            Application.Quit();
        }
    }

    public void IncreaseCurrency(int _amount)
    {
        mp_currency += _amount;
    }

    public bool SpendCurrency(int _amount)
    {
        if (mp_currency >= _amount) //if currency is enought to purchase
        {
            mp_currency -= _amount;
            return true;
        }
        else
        { 
            return false;
        }
    }

    private void SetScreenRatio(float w, float h)
    {
        if ((((float)Screen.width) / ((float)Screen.height)) > w / h)
        {
            Screen.SetResolution((int)(((float)Screen.height) * (w / h)), Screen.height, true);
        }
        else
        {
            Screen.SetResolution(Screen.width, (int)(((float)Screen.width) * (h / w)), true);
        }
    }
}
