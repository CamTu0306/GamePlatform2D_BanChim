using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
    public static int highestScore
    {
        // lớp PlayerPrefs lưu điểm số người chơi 
        get => PlayerPrefs.GetInt(GameConsts.Highest_Score, 0);

        set
        {
            // biến chứa điểm số đã lưu trong bộ nhớ
            int curScore = PlayerPrefs.GetInt(GameConsts.Highest_Score);

            // nếu điểm hiện tại > điểm đã lưu trong bộ nhớ
            if(value > curScore)
            {
                // lưu điểm số vào bộ nhớ
                PlayerPrefs.SetInt(GameConsts.Highest_Score, value);
            }
        }
    }
}
