using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFx : MonoBehaviour
{
    private float destroyTime = 1.2f;
    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if(destroyTime <0)
            Destroy(gameObject);
    }
}
