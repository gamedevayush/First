using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void destroy()
	{
	GameObject.Destroy(this.gameObject,13);
	}
}
