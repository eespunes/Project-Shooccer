﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [SerializeField] private bool isHome;
    [SerializeField] private GameObject camera;
    
    private bool _once;

    // Start is called before the first frame update
    void Start()
    {
        _once = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ReplayManager.Instance.Transition && !_once)
        {
            if (isHome) MatchController.GetInstance().AwayGoal();
            else MatchController.GetInstance().HomeGoal();
            _once = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Ball") && MatchController.GetInstance().Playing)
        {
            _once = false;
            MatchController.GetInstance().Playing = false;
            Invoke("GoalAnimation", 2f);
        }
    }

    private void GoalAnimation()
    {
        ReplayManager.Instance.Replay();
        Camera.main.gameObject.SetActive(false);
        camera.SetActive(true);
    }
}