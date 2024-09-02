using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float _atkRadius = 1f;
    [SerializeField] float _atkDmg = 100f;
    [SerializeField] float _reloadTime = 1f;
    bool _attackable = true;
    
    [SerializeField] Transform tfCursor;
    [SerializeField] Image imgRing;

    void Start()
    {
        Reload();        
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        tfCursor.position = mousePos;

        if(!_attackable){
            // 1초 동안 차오름.
            // Time.deltaTime => 1 프레임이 지나는 시간.
            // 현재 타겟 프레임 60 = 60fps => 1 sec
            // 1 : Time.deltaTime = 0.8 : x
            // Time.deltaTime * 0.8f = x
            imgRing.fillAmount += (1 /_reloadTime) * Time.deltaTime;

            return;
        }

        if(Input.GetMouseButtonUp(0)){
            Vector2 pos = mousePos;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(pos, _atkRadius, Vector2.zero);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.tag.Equals("Enemy")){
                    AbstractUnit unit = hits[i].collider.gameObject.GetComponent<AbstractUnit>();
                    unit.Damage(_atkDmg, gameObject);

                    _attackable = false;
                    imgRing.gameObject.SetActive(true);
                    imgRing.fillAmount = 0f;
                    Invoke("Reload", _reloadTime);
                }   
            }
        }
    }

    void Reload(){
        imgRing.gameObject.SetActive(false);
        _attackable = true;
        imgRing.fillAmount = 1f;
    }

    public float UpgradeAttackDamage(float val){
        _atkDmg += val;
        return _atkDmg;
    }

    public float UpgradeAttackRange(float val){
        _atkRadius += val;
        return _atkRadius;
    }

    public float GetReload(){
        return _reloadTime;
    }

    public float UpgradeReload(){
        _reloadTime -= 0.1f;
        if(_reloadTime <= 0.5f)
            _reloadTime = 0.5f;

        return _reloadTime;
    }

}
