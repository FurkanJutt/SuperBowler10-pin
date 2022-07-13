using System;
using UnityEngine;

public class Ball : MonoBehaviour
{ 
    Rigidbody _rigidbody;
    AudioSource _audioSource;
    [NonSerialized] public bool ballLaunced = false;
    Vector3 ballStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody.useGravity = false;
        ballStartPosition = transform.position;
    }

    public void ThrowBall(Vector3 velocity)
    {
        if(!ballLaunced)
        {
            _rigidbody.useGravity = true;
            _rigidbody.velocity = velocity;
            ballLaunced = true;
        }
    }

    public void MoveBallAtStart(float amount)
    {
        Debug.Log("BallMoved :" + amount);
        transform.Translate(new Vector3(amount, 0, 0));
    }

    public void ResetToStart()
    {
        ballLaunced = false;
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.position = ballStartPosition;
        transform.rotation = Quaternion.identity;
    }

    public void StopRollingSound()
    {
        _audioSource.Stop();
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Floor"))
        {
            _audioSource.Play();
        }
    }
}
