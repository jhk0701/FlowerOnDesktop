using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Virus : MonoBehaviour
{
    float _maxHp = 100f;
    float _hp = 0f;

    [SerializeField] Slider slHp;

    public float pHp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp < 0f)
            {
                _hp = 0f;
                Dead();
            }

            slHp.value = _hp / _maxHp;
        }
    }

    private void Start()
    {

    }

    public void OnDamage(float val)
    {
        pHp -= val;

    }

    public void Dead()
    {
        Destroy(gameObject, 3f);
    }


}
