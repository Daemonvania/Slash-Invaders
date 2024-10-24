using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour
{
    //todo make it with get set
    private int maxballAmount = 3;
    private int currentBallAmount;

    [SerializeField] GameObject[] ballIcons;

    private void Start()
    {
        currentBallAmount = maxballAmount;
    }

    //todo observer pattern
    public void BallLost()
    {
        currentBallAmount--;
        ballIcons[currentBallAmount].SetActive(false);
        if (currentBallAmount == 0)
        {
            Time.timeScale = 0;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
