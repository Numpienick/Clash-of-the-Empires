using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : Player {

    protected TextMeshProUGUI moneyCountText;
    public GameObject moneyCount;
    private Player playerRef;

	// Use this for initialization
	void Start () {
        playerRef = transform.root.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        moneyCountText = moneyCount.GetComponent<TextMeshProUGUI>();
        moneyCountText.SetText("Money: " + playerRef.money.ToString());
	}

   public void updateView()
    {

    }
}
