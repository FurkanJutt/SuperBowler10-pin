using UnityEngine;

public class LoadingScreenButtons : MonoBehaviour
{
    [SerializeField] AudioClip mouseDown;
    [SerializeField] AudioClip mouseEnter;

    private void OnMouseDown()
    {
        Debug.Log("MouseClicked");
        AudioSource.PlayClipAtPoint(mouseDown, Camera.main.transform.position, 1f);
    }

    private void OnMouseEnter()
    {
        Debug.Log("MouseEntered");
        AudioSource.PlayClipAtPoint(mouseEnter, Camera.main.transform.position, 1f);
    }
}
