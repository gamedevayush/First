using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitNGenerate : MonoBehaviour
{

	
	public bool isLast;
	void Start()
	{
		
	}
   public  void Generate(string nameofObj)
	{
		GameObject Ground = Instantiate(Resources.Load("GroundPrefab") as GameObject, new Vector3(this.transform.parent.position.x + 20.2f, 0, 0), Quaternion.identity);
		Ground.transform.parent=transform.parent.parent;
		if(nameofObj=="10")
		{
			Ground.transform.Find("Generator").GetComponent<MeshRenderer>().enabled=true;
			Ground.transform.Find("Generator").GetComponent<HitNGenerate>().isLast=true;
		}
		Ground.name=nameofObj;
	}
	
	
}
