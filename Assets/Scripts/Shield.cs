using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private float rotation_speed;
    void FixedUpdate()
    {
        transform.Rotate(0,rotation_speed * Time.fixedDeltaTime,0);
    }
}
