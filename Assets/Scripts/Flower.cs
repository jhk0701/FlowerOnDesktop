using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flower : AbstractUnit
{
    public float pHp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp <= 0f){
                _hp = 0f;
                OnDead();
            }

            slHp.value = _hp / _maxHp;
        }
    }

    public float pExp
    {
        get { return _exp; }
        set
        {
            _exp = value;
            if (_exp < 0)
                _exp = 0f;

            slExp.value = _exp / _maxExp;
        }
    }
    public int pLv
    {
        get { return _level; }
        set
        {
            _level = value;
            txtLv.text = _level.ToString();

            int lv = _level / 5;
            srFlower.sprite = Resources.Load<Sprite>($"/Img/Flower/grow-{lv}");
        }
    }

    [SerializeField] Slider slHp;
    [SerializeField] Slider slExp;
    [SerializeField] Text txtLv;
    [SerializeField] SpriteRenderer srFlower;

    void OnEnable()
    {
        ManagerGame.instance.mFlower.AddFlower(this);
    }

    private void Start()
    {
        pExp = 0f;
        pHp = _maxHp;
        pLv = 1;

        InvokeRepeating("Grow", 2f, 2f);
    }

    void Grow() {

        pExp += 10f;

        if (pExp >= _maxExp)
            LevelUp();
    }

    void LevelUp() {
        Debug.Log("Level Up!");
        pLv++;
        pExp -= _maxExp;

        ManagerGame.instance.AddScore(pLv * 100);
    }

    public override void Damage(float val, GameObject o){
        pHp -= val;
    }
    
    public override void OnDead() {
        ManagerGame.instance.mFlower.RemoveFlower(this);
    }
}
