using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFlower : MonoBehaviour
{
    public GameObject mainFlower;

    public Vector3 GetMainPosition()
    {
        return mainFlower.transform.position;
    }
}
