using System;
using UnityEngine;


public class DestroyAfter : MonoBehaviour
{
    [SerializeField]
    private float fTimer;

    private void Update()
    {
        this.fTimer -= Time.deltaTime;
        if (this.fTimer <= 0f)
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }
}