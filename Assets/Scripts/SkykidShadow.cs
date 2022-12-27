using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkykidShadow : MonoBehaviour
{

    void Update()
    {
        transform.position = new Vector3(transform.position.x, -3.495f, transform.position.z);
    }
}
