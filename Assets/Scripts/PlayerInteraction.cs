using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float _atkRadius = 1f;
    [SerializeField] float _atkDmg = 100f;
    [SerializeField] float _reloadTime = 1f;
    bool _attackable = true;

    void Start()
    {
        _attackable = true;
    }

    void Update()
    {
        if(!_attackable) return;

        if(Input.GetMouseButtonUp(0)){
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.CircleCastAll(pos, _atkRadius, Vector2.zero);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.tag.Equals("Enemy")){
                    AbstractUnit unit = hits[i].collider.gameObject.GetComponent<AbstractUnit>();
                    unit.Damage(_atkDmg, gameObject);

                    _attackable = false;
                    Invoke("Reload", _reloadTime);
                }   
            }
        }
    }

    void Reload(){
        _attackable = true;
    }

    public void AddAttackDamage(float val){
        _atkDmg += val;
    }

    public void AddAttackRange(float val){
        _atkRadius += val;
    }

}
