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
            txtTimer.text = _time.ToString("N2");
        }
    }
    [SerializeField] Text txtTimer;

    private void Awake()
    {
        if (!instance) instance = this;
    }


    private void Update()
    {
        pTime += Time.deltaTime;
    }
}
