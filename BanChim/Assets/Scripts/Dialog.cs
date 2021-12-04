using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text TxtTitle;
    public Text TxtConTent;


    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    public void UpdateDialog(string title, string content)
    {
        if (TxtTitle)
        {
            TxtTitle.text = title;
        }

        if (TxtConTent)
        {
            TxtConTent.text = content;
        }
    }
}
