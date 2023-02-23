using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
	public GameObject PowerVFX;
	public GameObject DieParticles;
	public GameObject PickParticles;
    private Vector3 fp;   
    private Vector3 lp;   
    private float dragDistance; 
	public float lfactor=1; 
	public float speedfactor=1; 
		float zaxis=0;
		int count=0;
		float xaxis=-18;
		string LineName;
		int power;
		public TMP_Text PowerCount;
		public TMP_Text CountDown;
		public UnityEngine.UI.Button PowerBtn;
		public UnityEngine.UI.Slider StageSlider;
		public bool isPowered;
		public bool canMove;
	private static PlayerMovement _instance;
	public static PlayerMovement Instance { get { return _instance; } }


	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	void OnEnable()
    {
		canMove = false;
		GetComponent<CharacterController>().enabled = true;
		StageSlider.value=0;
		transform.position=new Vector3(-18.13f,0.1f,0);
		PowerVFX.SetActive(false);
		count=0;
        dragDistance = (Screen.height * 15 / 100)/2;
		power=GameManager.Instance.power;
		PowerCount.text=power.ToString();
		PowerBtn.interactable=true;
		lfactor=GameManager.Instance.CurrLevel;
		StartCoroutine(CanMoveFunc());
    }
 public void UseMagnet()
    {
		transform.Find("Char").gameObject.GetComponent<BoxCollider>().enabled = true;
		StartCoroutine(DisableMagnet());
	}
 
 public void usePower()
	{
		if(power==0)
		{
			PowerBtn.interactable=false;
		}
		else
		{	
		power--;
		isPowered=true;
		PowerVFX.SetActive(true);
		Invoke("DisablePower",5f);
		}
		PowerCount.text=power.ToString();
		
		
	}  
	
	void DisablePower()
	{
		isPowered=false;
		PowerVFX.SetActive(false);
	}
    void Update()
    {
		if (canMove)
		{
			lerper();
		}
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x < fp.x))  //If the movement was to the right)
                        {  
						if(transform.position.z<0.38f )
							{
								if(transform.position.z<-0.1f)
								{
								zaxis=0;
								LineName="Middle";
								}
								else
								{
									if(LineName!="Left")
									{
									zaxis=transform.position.z+0.4f;
									LineName="Left";
									}
								}
							}
                        }
                        else
                        {   
								if(transform.position.z>-0.38f )
								{
								if(transform.position.z>0.1f)
								{
								zaxis=0;
								LineName="Middle";
								}
								else
								{
									if(LineName!="Right")
									{
									zaxis=transform.position.z-0.4f;
									LineName="Right";
									}
								}
			
								}
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {  
								
                        }
                    }
                }
                else
                {   
                    Debug.Log("Tap");
                }
            }
        }
    }
	void lerper()
	{
		Vector3 z=new Vector3(transform.position.x,transform.position.y,zaxis);
		transform.position=Vector3.Lerp(transform.position,z,(8+(lfactor/10))*Time.deltaTime);
		transform.Translate((lfactor/1000)+1.5f*speedfactor*Time.deltaTime,0,0);
	}
	
	public void SpeedUp()
	{
		if(speedfactor!=1.7f)
		{
		speedfactor=1.7f;
		Invoke("DisableSpeed",5f);
		}
	}
	
	void DisableSpeed()
	{
		speedfactor=1;
	}
	IEnumerator CanMoveFunc()
	{
		CountDown.text = "3";
		yield return new WaitForSeconds(1);
		CountDown.text = "2";
		yield return new WaitForSeconds(1);
		CountDown.text = "1";
		yield return new WaitForSeconds(1);
		canMove = true;
		CountDown.text = "GO!";
		yield return new WaitForSeconds(1);
		CountDown.text = "";
	}
	 void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name=="Generator")
		{
			if(col.gameObject.GetComponent<HitNGenerate>().isLast==true)
			{
				LevelManager.Instance.OnLevelFinish();
			}
			count++;
			
			if(count<=10)
			{
			col.GetComponent<HitNGenerate>().Generate(count.ToString());
			StageSlider.value=count;
			Destroy(col.transform.parent.gameObject,13f);			
			}
			
		
			
		}
	}
	public void Correct()
	{
		GameObject p=GameObject.Instantiate(PickParticles,this.transform);
		p.transform.localScale=new Vector3(2,2,2);
		Vibrator.Vibrate(100);
	}
	public void Die()
	{
		GetComponent<CharacterController>().enabled = false;
		GameObject p=GameObject.Instantiate(DieParticles,this.transform);
		p.transform.localScale=new Vector3(2,2,2);
		transform.Find("Char").gameObject.SetActive(false);
		Vibrator.Vibrate(1000);
	}


	IEnumerator DisableMagnet()
	{
		yield return new WaitForSeconds(10f);
		transform.Find("Char").gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	
}


