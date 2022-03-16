using System;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float fHp = 2f;

    [SerializeField]
    private float fDestoyAtY = 3f;

    [SerializeField]
    private GameObject goDeathFx;

    public float FHp
    {
        get
        {
            return this.fHp;
        }
    }

    private void Start()
    {
        base.GetComponent<Rigidbody2D>().angularVelocity = (UnityEngine.Random.value - 0.5f) * 200f;
    }

    public void TakeDamage(float _fHp)
    {
        this.fHp -= _fHp;
        this.AdjustSize();
        if (this.fHp <= 0f)
        {
            UnityEngine.Object.Destroy(base.gameObject);
            Oneshotter.singleton.PlayEnemyKillSound();
            UnityEngine.Object.Instantiate<GameObject>(this.goDeathFx, base.transform.position, Quaternion.identity);
        }
    }

    public void SetHp(float _fHp)
    {
        this.fHp = _fHp;
        this.AdjustSize();
        if (this.fHp <= 0f)
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }

    private void OnEnable()
    {
        GameManager.singleton.liEnemies.Add(this);
        GameManager.singleton.liEnemyTransforms.Add(base.transform);
        this.AdjustSize();
    }

    private void OnDisable()
    {
        GameManager.singleton.liEnemies.Remove(this);
        GameManager.singleton.liEnemyTransforms.Remove(base.transform);
    }

    private void AdjustSize()
    {
        if (this.fHp > 0f)
        {
            base.transform.localScale = Vector3.one * (Mathf.Sqrt(this.fHp) * 0.07f + 0.05f);
        }
    }

    private void Update()
    {
        if (base.transform.position.y < this.fDestoyAtY)
        {
            Oneshotter.singleton.PlayEnemyGotThroughSound();
            GameManager.singleton.fHp -= this.fHp * 0.05f;
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<Turret>().LaserAttackEnemy(this);
        }
    }
}
