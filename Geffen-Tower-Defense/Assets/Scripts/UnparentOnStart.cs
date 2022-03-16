using System;
using UnityEngine;


public class UnparentOnStart : MonoBehaviour
{
    private void Start()
    {
        base.transform.SetParent(null);
        base.transform.localScale = Vector3.one;
    }
}