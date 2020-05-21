using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Text countText;
    public Text winText;
    public PlayerController playerController;

    private int count = 0;

	void Start()
    {
        playerController.OnCollisionEvent += IncreaseScore;
    }

    void Update()
    {

    	float moveHorizontal = Input.GetAxis("Horizontal");
    	float moveVertical = Input.GetAxis("Vertical");

    	playerController.Move(moveHorizontal, moveVertical);
    }

    public void IncreaseScore()
    {
    	count = count + 1;

        SetCountText();
    }
}
