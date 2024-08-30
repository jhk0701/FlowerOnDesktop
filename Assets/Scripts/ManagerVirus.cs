using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerVirus : MonoBehaviour {
    float _interval = 5f;
    [SerializeField] GameObject prefVirus;

    public void Start()
    {
        _interval = 5f;
        InvokeRepeating("Generate", 5f, _interval);
    }

    public void UpdateInterval(float val){
        _interval = val;
        if(_interval <= 0f)
            _interval = 0.1f;
        
        if(IsInvoking("Generate"))
            CancelInvoke("Generate");
        
        InvokeRepeating("Generate", _interval, _interval);
    }

    public float GetInterval(){
        return _interval;
    }

    void Generate()
    {
        Instantiate(prefVirus);
    }
}
