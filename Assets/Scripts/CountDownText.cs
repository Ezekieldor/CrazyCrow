using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class CountDownText : MonoBehaviour
{
	public delegate void CountDownFinished ();
	public static event CountDownFinished OnCountDownFinished;
	
	Text countDown;
	
    void OnEnable ()
	{
		countDown = GetComponent <Text> ();
		countDown.text = "3";
		StartCoroutine ("CountDown");
	}
	
	IEnumerator CountDown ()
	{
		for (int i = 3; i > 0; i--)
		{
			countDown.text = i.ToString ();
			yield return new WaitForSeconds (1);
		}
		
		OnCountDownFinished ();
	}
}
