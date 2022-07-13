using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Ball))]
public class BallDrag : MonoBehaviour
{
    private Ball _ball;
    private Vector3 dragStart, dragEnd;
    private float timeStart, timeEnd;

    // Start is called before the first frame update
    void Start()
    {
        _ball = GetComponent<Ball>();
    }

    public void DragStart()
    {
        dragStart = Input.mousePosition;
        timeStart = Time.time;
    }

    public void DragEnd()
    {
        dragEnd = Input.mousePosition;
        timeEnd = Time.time;
        LaunchBall();
    }

    private void LaunchBall()
    {
        float dragDuration = timeEnd - timeStart;
        float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
        float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

        Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);

        _ball.ThrowBall(launchVelocity);
    }

    public void MoveBallAtStart(float amount)
    {
        if (!_ball.ballLaunced)
        {
            float xPos = Mathf.Clamp(_ball.transform.position.x + amount, -50f, 50f);
            float yPos = _ball.transform.position.y;
            float zPos = _ball.transform.position.z;
            _ball.transform.position = new Vector3(xPos, yPos, zPos);
        }
    }
}
