using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject square;
    public GameObject EndPanel;
    public Text timeTxt;
    public Text NowScore;
    public Text BestScore;

    bool isPlay = true;

    float time = 0.0f;

    string key = "BestScore";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeSquare", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlay) 
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
    }

    void MakeSquare()
    {
        Instantiate(square);
    }

    public void GameOver()
    {
        isPlay = false;
        Time.timeScale = 0.0f;
        NowScore.text = time.ToString("N2");

        if (PlayerPrefs.HasKey(key))
        {
            float Best = PlayerPrefs.GetFloat(key);
            if (Best < time)
            {
                PlayerPrefs.SetFloat(key, time);
                BestScore.text = time.ToString("N2");
            }
            else
            {
                BestScore.text = Best.ToString("N2");
            }
        }
        else
        {
            PlayerPrefs.SetFloat(key, time);
            BestScore.text += time.ToString("N2");
        }

        EndPanel.SetActive(true);
    }
}
