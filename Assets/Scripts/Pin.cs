using System.Collections;
using UnityEngine;

public class Pin : MonoBehaviour
{
    float DistanceYToRaisePinsButIDontKnowWhyItWorksOnZOnly = 40f;
    float tiltX;
    float tiltZ;

    Rigidbody _rigidBody;
    AudioSource _tapSound;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _tapSound = GetComponent<AudioSource>();
    }

    public bool IsStanding()
    {
        if (this != null)
        {
            Vector3 standRotation = transform.rotation.eulerAngles;
            tiltX = Mathf.Abs(standRotation.x);
            tiltZ = Mathf.Abs(standRotation.z);

            if ((tiltX > 270 && tiltX < 278) && (tiltZ > 125 && tiltZ < 145))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void Raise()
    {
        if (IsStanding())
        {
            _rigidBody.useGravity = false;
            _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            transform.Translate(new Vector3(0f, DistanceYToRaisePinsButIDontKnowWhyItWorksOnZOnly, 0f), Space.World);
            transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
    }

    public void Lower()
    {
        transform.Translate(new Vector3(0f, -DistanceYToRaisePinsButIDontKnowWhyItWorksOnZOnly +3f, 0f), Space.World);
        _rigidBody.useGravity = true;
        _rigidBody.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.GetComponent<Ball>())
        {
            _tapSound.Play();
        }
    }

    //private void Update()
    //{
    //    Debug.Log(name + " " + transform.rotation.eulerAngles + " " + IsStanding());
    //}
}
