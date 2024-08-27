using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flower : MonoBehaviour
{
    float _maxHp = 100f;
    float _hp = 0f;

    int _level = 1;
    float _maxExp = 100f;
    float _exp = 0f;

    public float pHp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp < 0f)
                _hp = 0f;

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
        }
    }

    [SerializeField] Slider slHp;
    [SerializeField] Slider slExp;
    [SerializeField] Text txtLv;

    private void Start()
    {
        pExp = 0f;
        pHp = _maxHp;
        pLv = 1;

        InvokeRepeating("Grow", 2f, 2f);
    }

    void Grow()
    {
        Debug.Log("Grow!");
        pExp += 10f;

        if (pExp >= _maxExp) LevelUp();
    }

    void LevelUp()
    {
        Debug.Log("Level Up!");
        pLv++;
        pExp -= _maxExp;
    }
}
