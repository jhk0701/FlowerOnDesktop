using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Virus : AbstractUnit
{
    [SerializeField] Slider slHp;
    [SerializeField] float _spd = 1f;
    [SerializeField] float _dmg = 10f;

    public float pHp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp <= 0f)
            {
                _hp = 0f;
                OnDead();
            }

            slHp.value = _hp / _maxHp;
        }
    }

    void Start()
    {
        Vector3 initPos = ManagerGame.instance.mFlower.GetMainPosition();
        float x = Random.Range(5f, 10f) * (Random.Range(1, 3) >= 2 ? 1f : -1f);
        float y = Random.Range(5f, 10f) * (Random.Range(1, 3) >= 2 ? 1f : -1f);
        initPos.x = x;
        initPos.y = y;

        transform.position = initPos;
    }

    void Update()
    {
        if(ManagerGame.instance.isPlaying)
            transform.position = Vector3.MoveTowards(transform.position, ManagerGame.instance.mFlower.GetMainPosition(), Time.deltaTime * _spd);
    }

    public override void Damage(float val)
    {
        pHp -= val;
    }

    public override void OnDead()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player")){
            // 꽃에 데미지
            AbstractUnit f = other.gameObject.GetComponent<AbstractUnit>();
            f?.Damage(_dmg);

            // 바이러스도 파괴
            Damage(_maxHp);
        }
    }
}
