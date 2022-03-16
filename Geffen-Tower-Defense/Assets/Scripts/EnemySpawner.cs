using System;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject goEnemy;

    [SerializeField]
    private AnimationCurve aniCurveDifficulty;

    [SerializeField]
    private AnimationCurve aniCurveHp;

    [SerializeField]
    private float fXMin;

    [SerializeField]
    private float fXMax;

    [SerializeField]
    private float fY;

    [SerializeField]
    private float fZ;

    private float fTime;

    private float fCurrency;

    private void Update()
    {
        this.fTime += Time.deltaTime;
        this.fCurrency += this.aniCurveDifficulty.Evaluate(this.fTime) * Time.deltaTime;
        if (this.fCurrency < 0f)
        {
            this.fCurrency = 0f;
        }
        float num = this.aniCurveHp.Evaluate(this.fTime);
        if (GameManager.singleton.fProgress < 1f && num > 0.1f)
        {
            while (this.fCurrency >= num)
            {
                this.fCurrency = 0f;
                this.SpawnEnemy();
            }
        }
        num;
    }

    private void SpawnEnemy()
    {
        Vector3 position = new Vector3(Mathf.Lerp(this.fXMin, this.fXMax, UnityEngine.Random.value), this.fY, this.fZ);
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.goEnemy, position, Quaternion.identity);
        gameObject.GetComponent<Enemy>().SetHp(this.aniCurveHp.Evaluate(this.fTime));
        position
        gameObject;
    }
}