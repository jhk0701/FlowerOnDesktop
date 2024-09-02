using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerVirus : MonoBehaviour {
    float _interval = 5f;
    [SerializeField] GameObject prefVirus;
    [SerializeField] GameObject prefGold;

    public void Start()
    {
        _interval = 5f;
        InvokeRepeating("Generate", 5f, _interval);
    }

    public void UpdateInterval(float val){
        _interval = val;
        if(_interval <= 0f)
            _interval = 0.5f;
        
        if(IsInvoking("Generate"))
            CancelInvoke("Generate");
        
        InvokeRepeating("Generate", _interval, _interval);
    }

    public float GetInterval(){
        return _interval;
    }

    void Generate()
    {
        int r = Random.Range(1, 101);
        if(r <= 10)
            Instantiate(prefGold);
        else
            Instantiate(prefVirus);
    }
}
