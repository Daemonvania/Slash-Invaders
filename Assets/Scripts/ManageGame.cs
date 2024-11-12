using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGame : Subject
{
    private int maxballAmount = 3;
    private int currentBallAmount;
    public int ballLevel { get; set; } = 1;

    
    [SerializeField] GameObject[] ballIcons;

    private void Start()
    {
        currentBallAmount = maxballAmount;
    }
    
    public void BallLost()
    {
        currentBallAmount--;
        ballIcons[currentBallAmount].SetActive(false);
        ballLevel = 1;
        NotifyObservers(GameActions.BallLost, this);
        if (currentBallAmount == 0)
        {
            Time.timeScale = 0;
        }
    }
    
    //todo OBSERVER
    public void IncreaseBallLevel()
    {
        ballLevel++;
        NotifyObservers(GameActions.BallLevelUp, this);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
