using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ManagerFlower : MonoBehaviour
{
    [SerializeField] GameObject prefFlower;
    public Flower mainFlower;
    public List<Flower> subFlowers;

    void Start()
    {
        Instantiate(prefFlower, Vector3.zero, quaternion.identity);
    }

    public Vector3 GetMainPosition()
    {
        if(!mainFlower)
        {
            CleanSubFlowers();

            if (!SetMainFlower())
                return Vector3.zero;
        }
        
        return mainFlower.transform.position;
    }

    public void AddFlower(Flower fl){
        subFlowers.Add(fl);
        CleanSubFlowers();
        SetMainFlower();
    }

    public void RemoveFlower(Flower fl){
        int id = subFlowers.FindIndex(n=>n.Equals(fl));
        if(id >= 0)
            subFlowers.RemoveAt(id);
        else
        {
            mainFlower = null;
            CleanSubFlowers();
            SetMainFlower();
        }
    }

    void CleanSubFlowers(){
        for (int i = 0; i < subFlowers.Count; i++)
        {
            if(!subFlowers[i])
            {
                subFlowers.RemoveAt(i);
                i--;
            }
        }
    }

    bool SetMainFlower(){
        if(!mainFlower)
        {
            if(subFlowers.Count > 0)
            {
                mainFlower = subFlowers[0];
                subFlowers.RemoveAt(0);
            }
            else{
                ManagerGame.instance.GameOver();
                return false;
            }
        }

        return true;
    }
}
