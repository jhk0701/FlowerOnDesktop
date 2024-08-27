using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame instance;

    private void Awake()
    {
        if (!instance) instance = this;
    }

}
