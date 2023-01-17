using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMobile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            Destroy(gameObject);
        }
    }

}
