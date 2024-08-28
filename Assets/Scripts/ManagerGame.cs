using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame instance;
    public ManagerFlower mFlower;

    float _time;
    float pTime
    {
        get { return _time; }
        set { 
            _time = value;
            txtTimer.text = _time.ToString("0.00");
        }
    }
    [SerializeField] Text txtTimer;

    int _score;
    int pScore{
        get { return _score; }
        set {
            _score = value;
            txtScore.text = _score.ToString();
        }
    }
    [SerializeField] Text txtScore;

    public bool isPlaying = true;

    [SerializeField] GameObject panelGameOver;
    [SerializeField] Text txtTotalScore;


    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        isPlaying = true;
    }

    private void Update()
    {
        pTime += Time.deltaTime;
    }

    public void AddScore(int val){
        pScore += val;
    }

    public void GameOver(){
        Time.timeScale = 0f;
        isPlaying = false;

        panelGameOver.SetActive(true);
        txtTotalScore.text = (pScore + pTime).ToString("0.00");
        
        Debug.Log("Game Over");
    }

    public void Retry(){
        SceneManager.LoadScene(0);
    }
}
