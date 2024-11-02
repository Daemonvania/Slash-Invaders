using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour
{
    private int maxballAmount = 3;
    private int currentBallAmount;
    public int ballLevel { get; set; } = 1;

    
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
    
    //todo OBSERVER
    public void IncreaseBallLevel()
    {
        ballLevel++;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
