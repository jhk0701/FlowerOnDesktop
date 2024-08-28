using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool isPlaying = true;

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

    public void GameOver(){
        Time.timeScale = 0f;
        isPlaying = false;
        
        Debug.Log("Game Over");
    }
}
