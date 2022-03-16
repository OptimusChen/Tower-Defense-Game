using System;
using UnityEngine;


public class CreateOnDestroy : MonoBehaviour
{
    public GameObject goCreate;

    private void OnDestroy()
    {
        UnityEngine.Object.Instantiate<GameObject>(this.goCreate, base.transform.position, Quaternion.identity);
    }
}
