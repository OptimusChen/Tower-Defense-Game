using System;
using UnityEngine;


public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float fSpeed;

    private void Start()
    {
    }

    private void Update()
    {
        base.transform.rotation = Quaternion.Euler(base.transform.rotation.eulerAngles.x, base.transform.rotation.eulerAngles.y, base.transform.rotation.eulerAngles.z + this.fSpeed * Time.deltaTime);
    }
}