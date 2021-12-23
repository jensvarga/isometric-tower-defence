using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    private bool swap = true;
    // Update is called once per frame
    void Update()
    {
        if (this.transform.rotation.z > 0.7 && swap)
        {
            swap = false;
        }
        
        if (this.transform.rotation.z < 0.3f && !swap)
        {
            swap = true;
        }

        if (swap)
        {
            this.transform.Rotate(0f, 0f, -10f * Time.deltaTime);
        }
        else
        {
            this.transform.Rotate(0f, 0f, 10f * Time.deltaTime);
        }
    }
}
