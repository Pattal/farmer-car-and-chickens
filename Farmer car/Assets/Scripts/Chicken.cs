using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chicken : MonoBehaviour
{
    static public bool isExisted;

    public static UnityEvent Dead = new UnityEvent();

    private void OnEnable()
    {
        isExisted = true;
    }

    private void OnDestroy()
    {
        isExisted = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Cube"))
        {
            Dead?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
