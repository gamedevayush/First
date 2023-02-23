using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDeactive : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Deactive", 2f);
    }
        void Deactive()
    { 
        gameObject.SetActive(false);
    }

}


