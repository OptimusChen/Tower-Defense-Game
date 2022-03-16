using System;
using System.Collections.Generic;
using UnityEngine;


public class Turret : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private bool bDestructable = true;

    [SerializeField]
    private bool bAttackAllWithinRadius;

    [SerializeField]
    private bool bFocusHighestHpTarget;

    [SerializeField]
    private float fRange = 2f;

    [SerializeField]
    private float fFireRate = 1f;

    [SerializeField]
    private float fDamage = 0.2f;

    [SerializeField]
    private float fKnockback = 0.2f;

    [SerializeField]
    private float fEnemySpeedMulti = 1f;

    [SerializeField]
    private GameObject goShootFx;

    [SerializeField]
    private GameObject goCreateOnSacrifice;

    [SerializeField]
    private GameObject goRangeSpherePrefab;

    private GameObject goRangeShpere;

    private float fShootTimer;

    private bool bMouseOver;

    private void Start()
    {
        this.fShootTimer = this.fFireRate * UnityEngine.Random.value;
        this.gameManager = GameManager.singleton;
        this.goRangeShpere = UnityEngine.Object.Instantiate<GameObject>(this.goRangeSpherePrefab, base.transform.position, Quaternion.identity);
        this.goRangeShpere.transform.localScale = Vector3.one * this.fRange * 2f;
        this.goRangeShpere.transform.SetParent(base.transform);
        this.goRangeShpere.SetActive(false);
    }

    private void Update()
    {
        if (this.fFireRate > 0f)
        {
            this.fShootTimer += Time.deltaTime;
            while (this.fShootTimer >= this.fFireRate)
            {
                this.fShootTimer -= this.fFireRate * (UnityEngine.Random.value * 0.2f + 0.9f);
                this.Attack();
            }
        }
    }

    private void OnMouseDown()
    {
        if (this.bDestructable)
        {
            UnityEngine.Object.Destroy(base.gameObject);
            if (this.goCreateOnSacrifice != null)
            {
                UnityEngine.Object.Instantiate<GameObject>(this.goCreateOnSacrifice, base.transform.position, Quaternion.identity);
                Oneshotter.singleton.PlayOverchargeSound();
            }
        }
    }

    private void OnMouseEnter()
    {
        this.bMouseOver = true;
        this.goRangeShpere.SetActive(this.bMouseOver);
        Oneshotter.singleton.PlayHoverSound();
    }

    private void OnMouseExit()
    {
        this.bMouseOver = false;
        this.goRangeShpere.SetActive(this.bMouseOver);
    }

    public void LaserAttackEnemy(Enemy _enemy)
    {
        if (_enemy == null)
        {
            return;
        }
        if (this.fRange <= 0f)
        {
            Oneshotter.singleton.PlayBounceHurtSound(!this.bDestructable);
        }
        Transform transform = _enemy.transform;
        Vector3 position = (transform.position + base.transform.position) * 0.5f + new Vector3(0f, 0f, -0.35f);
        Quaternion rotation = Quaternion.LookRotation(transform.position - base.transform.position, Vector3.back);
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.goShootFx, position, rotation);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, (transform.position - base.transform.position).magnitude);
        _enemy.TakeDamage(this.fDamage);
        if (transform.position.y > base.transform.position.y)
        {
            Vector2 b = (transform.position - base.transform.position).normalized * this.fKnockback;
            _enemy.GetComponent<Rigidbody2D>().velocity += b;
        }
        if (this.fEnemySpeedMulti < 1f)
        {
            _enemy.GetComponent<Rigidbody2D>().velocity *= this.fEnemySpeedMulti;
        }
        transform
        position;
        rotation
        gameObject;
        b;
    }

    public void Attack()
    {
        Enemy enemy;
        if (this.bFocusHighestHpTarget)
        {
            enemy = this.gameManager.EnemyReturnHighestHpTarget(base.transform.position, this.fRange);
        }
        else
        {
            enemy = this.gameManager.EnemyReturnBotTarget(base.transform.position, this.fRange);
        }
        if (enemy == null)
        {
            return;
        }
        if (this.bAttackAllWithinRadius)
        {
            Oneshotter.singleton.PlaySplashAttackSound(!this.bDestructable);
            List<Enemy> list = this.gameManager.LiEnemiesReturnInRadius(base.transform.position, this.fRange);
            foreach (Enemy current in list)
            {
                this.LaserAttackEnemy(current);
            }
        }
        else
        {
            if (this.bFocusHighestHpTarget)
            {
                Oneshotter.singleton.PlaySnipeAttackSound(!this.bDestructable);
            }
            else
            {
                Oneshotter.singleton.PlayLaserSound(!this.bDestructable);
            }
            this.LaserAttackEnemy(enemy);
        }
        enemy
        list;
        enumerator
        current;
    }
}