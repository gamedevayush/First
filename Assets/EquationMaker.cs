using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquationMaker : MonoBehaviour
{
    
	
	public TMP_Text ques;
	public TMP_Text an1;
	public TMP_Text an2;
	public TMP_Text an3;
	public GameObject s1;
	public GameObject s2;
	public GameObject s3;
 	string oper;
	 int eqv1=0;
	 int eqv2=0;
	 float ans1;
	 float ans2;
	 float ans3;
	 float ans;
	 int lfactor;
	 int ran_oper_int;
	 int ran_ans_int;
		
    void Start()
    {
		lfactor=GameManager.Instance.CurrLevel;
		if(lfactor>6)
		{
			lfactor=6;
		}
       eqv1=Random.Range(1,4+lfactor);
       eqv2=Random.Range(1,4+lfactor);
		if(lfactor<=3)
		{
			ran_oper_int=Random.Range(0,3);
		}
		else
		{
			ran_oper_int=Random.Range(0,4);
		}
	   if(ran_oper_int==0) //ADD
	   {
		   ans=eqv1+eqv2;
		   oper="+";
	   }
	   if(ran_oper_int==1) //SUB
	   {
		   ans=eqv1-eqv2;
		   oper="-";
	   }
	   if(ran_oper_int==2) //MUL
	   {
		   ans=eqv1*eqv2;
		   oper="x";
	   }
	   if(ran_oper_int==3) //DIV
	   {
		   ans=(float)eqv1/(float)eqv2;
		   oper="/";
	   }
	   Debug.Log("Run");
	   ran_ans_int=Random.Range(0,3);
	   if(ran_ans_int==0) //S1
	   {
		   ans1=ans;
		   ans2=ans-1;
		   ans3=ans+1;
		   s1.GetComponent<AnswerSender>().Correcthai();
		   Debug.Log("Run2");
	   }
	   if(ran_ans_int==1) //S2
	   {
		   ans1=ans-1;
		   ans2=ans;
		   ans3=ans+1;
		   s2.GetComponent<AnswerSender>().Correcthai();
		   Debug.Log("Run2");
	   }
	   if(ran_ans_int==2) //S3
	   {
		   ans1=ans-1;
		   ans2=ans+1;
		   ans3=ans;
		   s3.GetComponent<AnswerSender>().Correcthai();
		   Debug.Log("Run2");
	   }
	   if(ran_oper_int==3) //If divide, show three deciamAlpoints
	   {
		   an1.text=ans1.ToString("0.00");
		   an2.text=ans2.ToString("0.00");
		   an3.text=ans3.ToString("0.00");
	   }
	   else
	   {
			
			an1.text=ans1.ToString();
		   an2.text=ans2.ToString();
		   an3.text=ans3.ToString();
	   }
	   ques.text=eqv1.ToString()+oper.ToString()+eqv2.ToString()+"=?";
    }
   
	


}