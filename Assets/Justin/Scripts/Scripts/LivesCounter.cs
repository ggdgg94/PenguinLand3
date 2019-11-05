using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[ExecuteInEditMode]

public class LivesCounter : MonoBehaviour 
{
	public int initialLives = 3;
	public int extraLives = 0;
	public int totalLives;

	void Start()
	{
		GetLives();
	}

	// Update is called once per frame
	void Update () 
	{
		totalLives = initialLives + extraLives;

		//Set the lives count text
		GetComponent<Text>().text = totalLives.ToString();
	}

	void GetLives()
	{
		totalLives = initialLives + extraLives;
	}
}
