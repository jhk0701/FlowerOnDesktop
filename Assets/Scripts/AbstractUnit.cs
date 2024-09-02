using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{
    protected float _maxHp = 100f;
    protected float _hp = 0f;

    protected int _level = 1;
    protected float _maxExp = 100f;
    protected float _exp = 0f;

    public enum Type : int {
        normal = 0,
        gold = 1
    }
    public Type type;

    public abstract void Damage(float val, GameObject o);
    public abstract void OnDead();
}
