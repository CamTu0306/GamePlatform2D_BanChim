using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;

    public Dialog gameDialog;
    public Dialog pauseDialog;

    public Image fireRateFilled;
    public Text TxtTimer;
    public Text TxtKilledCouting;

    Dialog m_curDialog;

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    public override void Awake()
    {
       MakeSingleton(false);
    }

    public void ShowGameGui(bool isShow)
    {
        if (gameGUI)
        {
            // phương thức SetActive ẩn/ hiện gameobject 
            gameGUI.SetActive(isShow);
        }

        if (homeGUI)
        {
            homeGUI.SetActive(!isShow);
        }
    }

    public void UpdateTimer(string time)
    {
        if (TxtTimer)
        {
            TxtTimer.text = time;
        }
    }

    public void UpdateKilledCounting(int killed)
    {
        if (TxtKilledCouting)
            TxtKilledCouting.text = "x" +  killed.ToString();
    }

    public void UpdateFireRate(float rate)
    {
        fireRateFilled.fillAmount = rate;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        if (pauseDialog)
        {
            pauseDialog.Show(true);
            pauseDialog.UpdateDialog("GAME PAUSE", "BEST KILLED: x" + Prefs.highestScore);
            m_curDialog = pauseDialog;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if (m_curDialog)
        {
            m_curDialog.Show(false);
        }
    }

    public void BackToHome()
    {
        ResumeGame();

        // lấy tên của sence người dùng đang chơi
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReplayGame()
    {
        if (m_curDialog)
        {
            m_curDialog.Show(false);
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Application.Quit();
    }

}
