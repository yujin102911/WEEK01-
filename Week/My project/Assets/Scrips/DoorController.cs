using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isDoorOpen = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        if (!isDoorOpen){
            animator.SetTrigger("Open");
            isDoorOpen = true;
            Debug.Log("¹® ¿­¸²");
        }
    }

    public void CloseDoor()
    {
        if (isDoorOpen)
        {
            animator.SetTrigger("Close");
            isDoorOpen = false;
            Debug.Log("¹® ´ÝÈû");
        }
    }


}
