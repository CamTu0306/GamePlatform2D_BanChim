using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // biến chứa các con chim
    public Birds[] birdPrefabs;
    // biến tạo khoảng thời gian chim xuất hiện
    public float spawnTime;
    // giới hạn thời gian người chơi
    public int timeLimit;

    int m_curTimeLimit;
    int m_birdKilled;

    bool m_isGameover;

    public int BirdKilled { get => m_birdKilled; set => m_birdKilled = value; }
    public bool IsGameover { get => m_isGameover; set => m_isGameover = value; }

    public override void Awake()
    {
        MakeSingleton(false);

        m_curTimeLimit = timeLimit;
    }

    public override void Start()
    {
        GameGUIManager.Ins.ShowGameGui(false);
        GameGUIManager.Ins.UpdateKilledCounting(m_birdKilled);

    }

    public void PlayGame()
    {
        StartCoroutine(GameSpawn());

        StartCoroutine(TimeCountDown());

        GameGUIManager.Ins.ShowGameGui(true);
    }

    // giảm giá trị của timeLimit
    IEnumerator TimeCountDown()
    {
        while(m_curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            m_curTimeLimit--;

            if(m_curTimeLimit <= 0)
            {
                m_isGameover = true;

                if(m_birdKilled > Prefs.highestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST KILLED: x" + m_birdKilled);
                }else if(m_birdKilled <= Prefs.highestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST KILLED: x" + Prefs.highestScore);
                }

                Prefs.highestScore = m_birdKilled;

                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog = GameGUIManager.Ins.gameDialog;
                Time.timeScale = 0f;

                
            }

            GameGUIManager.Ins.UpdateTimer(InToTime(m_curTimeLimit));

            
        }
    }

    // thực hiện công việc nào đó trong khoảng thời gian chờ nhất định
    IEnumerator GameSpawn()
    {
        while (!IsGameover)
        {
            SpawnBird();
            // đợi trong khoảng thời gian spawnTime thì sẽ tạo ra chim
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;

        // tạo điểm xuất hiện ngẫu nhiên của chim
        float randCheck = Random.Range(0f, 1f);  

        if(randCheck > 0.5f)
        {
            // tạo điểm xuất hiện của chim bên phải
            spawnPos = new Vector3(12, Random.Range(1.5f, 4f), 0);
        }
        else
        {
            // tạo điểm xuất hiện của chim bên trái
            spawnPos = new Vector3(-12, Random.Range(1.5f, 4f), 0);
        }

        if(birdPrefabs != null && birdPrefabs.Length > 0)
        {
            int randIdx = Random.Range(0, birdPrefabs.Length);

            if(birdPrefabs[randIdx] != null)
            {
                Birds birdClone = Instantiate(birdPrefabs[randIdx], spawnPos, Quaternion.identity);
            }
        }
    }

    string InToTime(int time)
    {
        float minute = Mathf.Floor(time / 60);
        float second = Mathf.RoundToInt(time % 60);

        return minute.ToString("00") + " : " + second.ToString("00");
    }
}
