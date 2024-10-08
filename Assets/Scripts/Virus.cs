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

    [SerializeField] Animator anim;

    void Start()
    {
        _maxHp = _maxHp + ManagerGame.instance.pDifficulty;
        _maxHp += ManagerGame.instance.GetDifficulty() * 50f;
        pHp = _maxHp;

        Vector3 initPos = ManagerGame.instance.mFlower.GetMainPosition();
        float x = Random.Range(5f, 10f) * (Random.Range(1, 3) >= 2 ? 1f : -1f);
        float y = Random.Range(5f, 10f) * (Random.Range(1, 3) >= 2 ? 1f : -1f);
        initPos.x = x;
        initPos.y = y;

        transform.position = initPos;

        if(!anim) anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!ManagerGame.instance.isPlaying || pHp <= 0f)
            return;

        transform.position = Vector3.MoveTowards(transform.position, ManagerGame.instance.mFlower.GetMainPosition(), Time.deltaTime * _spd);
    }

    public override void Damage(float val, GameObject o)
    {
        pHp -= val;
        if(pHp <= 0f && o.tag.Equals("Player")){
            if (type.Equals(Type.normal))
                ManagerGame.instance.AddScore(Random.Range(10, 30));
            else if (type.Equals(Type.gold))
                ManagerGame.instance.AddScore(Random.Range(800, 1200));
        }
    }

    public override void OnDead()
    {
        GetComponent<Collider2D>().enabled = false;
        anim.SetBool("isDead", true);
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player")){
            AbstractUnit f = other.gameObject.GetComponent<AbstractUnit>();
            f?.Damage(_dmg, gameObject);

            Damage(_maxHp, gameObject);
        }
    }
}
