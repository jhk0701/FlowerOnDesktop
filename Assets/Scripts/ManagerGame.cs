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

    int _gold {get; set;}
    int pGold{
        get { return _gold; }
        set {
            _gold = value;
            txtGold.text = _gold.ToString();
        }
    }
    [SerializeField] Text txtGold;
    [SerializeField] Text txtBestScoreLabel;
    [SerializeField] Text txtBestScore;

    public bool isPlaying = true;

    [SerializeField] GameObject panelGameOver;
    [SerializeField] Text txtTotalScore;
    [Space(10f)]
    [SerializeField] Text txtCurDmg;
    [SerializeField] Text txtCurRange;
    [SerializeField] Text txtCurReload;


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
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;
        isPlaying = true;
    }

    private void Update()
    {
        pTime += Time.deltaTime;
    }

    public void AddScore(int val){
        pScore += val;
        pGold += val;
        pDifficulty += val / 10;
    }

    public void GameOver(){
        Debug.Log("Game Over");
        
        Time.timeScale = 0f;
        isPlaying = false;

        panelGameOver.SetActive(true);

        float bestScore = PlayerPrefs.HasKey("BestScore") ? PlayerPrefs.GetFloat("BestScore") : 0f;
        float curScore = pScore + pTime;

        if(bestScore < curScore){ // 갱신
            txtBestScoreLabel.text = "신기록이에요!";
            txtBestScore.text = curScore.ToString("0.00");

            PlayerPrefs.SetFloat("BestScore", curScore);
        }
        else{
            txtBestScoreLabel.text = "최고 점수";
            txtBestScore.text = bestScore.ToString("0.00");
        }

        txtTotalScore.text = curScore.ToString("0.00");
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
        if(pGold < 1000)
            return;
        
        pGold -= 1000;

        float dmg = playerInteraction.UpgradeAttackDamage(100f);
        txtCurDmg.text = string.Format("Current Damage : {0}", dmg);
    }

    public void UpgradeRange(){
        if(pGold < 1000)
            return;
        
        pGold -= 1000;

        float rng = playerInteraction.UpgradeAttackRange(1f);
        txtCurRange.text = string.Format("Current Range : {0}", rng);
    }

    public void UpgradeReload(){
        if(pGold < 1000 || playerInteraction.GetReload() <= 0.5f)
            return;
        
        pGold -= 1000;

        float reload = playerInteraction.UpgradeReload();
        txtCurReload.text = string.Format("Current Reload Time : {0} sec", reload);
    }

}
