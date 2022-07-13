using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 distanceAwayFromBall;

    Ball _ball;

    // Start is called before the first frame update
    void Start()
    {
        _ball = FindObjectOfType<Ball>();
        distanceAwayFromBall = transform.position - _ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TrackAndFollowBall();
    }

    private void TrackAndFollowBall()
    {
        if (!_ball) { return; }

        if(_ball.transform.position.z <= 1829f)
        {
            transform.position = _ball.transform.position + distanceAwayFromBall;
        }
    }
}
