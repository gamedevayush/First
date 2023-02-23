using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right   * (200 * Time.deltaTime));
    }
}
