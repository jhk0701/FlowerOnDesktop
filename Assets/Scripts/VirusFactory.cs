using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusFactory : MonoBehaviour
{

    [SerializeField] GameObject prefVirus;

    public void Start()
    {
        InvokeRepeating("Generate", 5f, 1f);   
    }

    void Generate()
    {
        Instantiate(prefVirus);
    }
}
