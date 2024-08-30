using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame instance;
    public ManagerFlower mFlower;
    public ManagerVirus mVirus;
    public PlayerInteraction playerInteraction;
    float _time;
    float pTime
    {
        get { return _time; }
        set { 
            _time = value;
            txtTimer.text = _time.ToString("0.00");
        }
    }
    [Space(10f)]
    [SerializeField] Text txtTimer;

    int _score;
    int pScore {
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
    [Space(10f)]
    [SerializeField] Text txtCurDmg;
    [SerializeField] Text txtCurRange;


    int _difficulty = 0;
    int _diffVal = 0;
    public int pDifficulty {
        get {
            return _diffVal;
        }
        set {
            _diffVal = value;
            if(_diffVal >= 100){
                // 난이도 증가
                MoreDifficult();
                _diffVal = 0;
            }
        }
    }


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
        pDifficulty += val / 10;
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

    public void AddDifficulty(){
        pDifficulty += 50;
    }

    public void MoreDifficult(){
        mVirus.UpdateInterval(mVirus.GetInterval() - 1f);
        _difficulty++;

        Debug.Log($"More Difficult. {_difficulty}");
    }

    public int GetDifficulty(){
        return _difficulty;
    }

    public void UpgradeDmg(){
        if(pScore < 1000)
            return;
        
        pScore -= 1000;

        float dmg = playerInteraction.AddAttackDamage(50f);
        txtCurDmg.text = string.Format("Current Damage : {0}", dmg);
    }

    public void UpgradeRange(){
        if(pScore < 1000)
            return;
        
        pScore -= 1000;

        float rng = playerInteraction.AddAttackRange(1f);
        txtCurRange.text = string.Format("Current Range : {0}", rng);
    }

}
