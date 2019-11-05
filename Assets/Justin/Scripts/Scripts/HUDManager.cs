using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour 
{
	private Animator hudAnim;
	// Use this for initialization
	void Awake () 
	{
		hudAnim = GetComponent<Animator>();
	}
	
	public void HUDFade()
	{
		hudAnim.SetTrigger("FadeIn");
	}
}
