using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour 
{
	private Animator menuAnim;

	// Use this for initialization
	void Awake () 
	{
		menuAnim = GetComponent<Animator>();
	}
	
	public void MenuFade()
	{
		menuAnim.SetTrigger ("FadeOut");
	}
}
