using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSun : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(5f * Time.unscaledDeltaTime, 0f, 0f);
    }
}
