using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
		public GameObject Player;
    public float dist=0.47f;
    void OnEnable()
    {
		transform.position=new Vector3(-18.7599983f,0.579999983f,-0.0299999993f);
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = Vector3.Lerp(transform.position,new Vector3(Player.transform.position.x+dist, Player.transform.position.y+0.65f, Player.transform.position.z/2),2*Time.deltaTime);
    }
}
