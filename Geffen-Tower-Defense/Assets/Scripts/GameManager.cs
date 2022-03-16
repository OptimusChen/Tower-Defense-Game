using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public List<Enemy> liEnemies;

    public List<Transform> liEnemyTransforms;

    public LayerMask lmWalls;

    public Image imgProgress;

    public Image imgHp;

    public float fProgress;

    public float fHp = 1f;

    public GameObject goVictory;

    public GameObject goDefeat;

    private int iVictoryState;

    private float fLevelEndTimer;

    public float fLevelDuration = 50f;

    private void Awake()
    {
        GameManager.singleton = this;
        this.liEnemies = new List<Enemy>();
        this.liEnemyTransforms = new List<Transform>();
    }

    private void Update()
    {
        this.fProgress += Time.deltaTime / this.fLevelDuration;
        if (this.fProgress >= 1.1f)
        {
            this.SetVictoryState(1);
        }
        this.imgProgress.fillAmount = Mathf.Clamp(this.fProgress, 0f, 1.1f) / 1.1f;
        this.imgHp.fillAmount = Mathf.Clamp(this.fHp, 0f, 1f);
        if (this.fHp <= 0f)
        {
            this.SetVictoryState(-1);
        }
        if (this.iVictoryState != 0)
        {
            this.fLevelEndTimer += Time.deltaTime;
            if (this.fLevelEndTimer > 6f)
            {
                if (this.iVictoryState == -1)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                if (this.iVictoryState == 1)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name + "X");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey(KeyCode.N) && Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name + "X");
        }
    }


    public Enemy EnemyReturnHighestHpTarget(Vector2 _v2Position, float _fMaxDistance)
    {
        Enemy result = null;
        float num = 0f;
        for (int i = 0; i < this.liEnemies.Count; i++)
        {
            Transform transform = this.liEnemyTransforms[i];
            Vector2 vector = transform.position;
            float num2 = this.liEnemies[i].FHp;
            if (num2 > num && (transform.position - _v2Position).magnitude < _fMaxDistance && transform.position.y < 8.6f)
            {
                Vector2 direction = transform.position - _v2Position;
                if (Physics2D.Raycast(_v2Position, direction, direction.magnitude, this.lmWalls).collider == null)
                {
                    num = num2;
                    result = this.liEnemies[i];
                }
            }
        }
        return result;
        result
        num;
        i
        transform;
        vector
        num2;
        direction;
    }

    public List<Enemy> LiEnemiesReturnInRadius(Vector2 _v2Position, float _fMaxDistance)
    {
        List<Enemy> list = new List<Enemy>();
        for (int i = 0; i < this.liEnemies.Count; i++)
        {
            if ((this.liEnemyTransforms[i].position - _v2Position).magnitude < _fMaxDistance)
            {
                Vector2 direction = this.liEnemyTransforms[i].position - _v2Position;
                if (Physics2D.Raycast(_v2Position, direction, direction.magnitude, this.lmWalls).collider == null)
                {
                    list.Add(this.liEnemies[i]);
                }
            }
        }
        return list;
        list
        i;
        direction;
    }

    public void SetVictoryState(int _iState)
    {
        if (this.iVictoryState == 0)
        {
            this.iVictoryState = _iState;
            if (this.iVictoryState == 1)
            {
                for (int i = this.liEnemies.Count - 1; i >= 0; i--)
                {
                    this.liEnemies[i].TakeDamage(1E+08f);
                }
                this.goVictory.SetActive(true);
                Oneshotter.singleton.PlayWinSound();
            }
            if (this.iVictoryState == -1)
            {
                this.goDefeat.SetActive(true);
                Oneshotter.singleton.PlayLoseSound();
            }
        }
        i;
    }

    public Enemy EnemyReturnBotTarget(Vector2 _v2Position, float _fMaxDistance)
    {
        Enemy result = null;
        float num = 3.40282347E+38f;
        for (int i = 0; i < this.liEnemies.Count; i++)
        {
            Transform transform = this.liEnemyTransforms[i];
            float y = transform.position.y;
            if (y < num && (transform.position - _v2Position).magnitude < _fMaxDistance)
            {
                Vector2 direction = transform.position - _v2Position;
                if (Physics2D.Raycast(_v2Position, direction, direction.magnitude, this.lmWalls).collider == null)
                {
                    num = y;
                    result = this.liEnemies[i];
                }
            }
        }
        return result;
        result
        num;
    }
    transform
    y;
    direction
}