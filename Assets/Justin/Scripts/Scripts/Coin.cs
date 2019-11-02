using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour 
{
	private CoinCounter coinCounter;

	void Awake()
	{
		coinCounter = GameObject.Find("CoinCount").GetComponent<CoinCounter>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			print ("You picked up a coin!");
		//Update the GUI
		coinCounter.coinCount++;
		//Set the coin to inactive
		gameObject.SetActive(false);
	}

}
