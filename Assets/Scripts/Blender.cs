using UnityEngine;

public class Blender : MonoBehaviour
{
    private Animator _blenderAnimator;

    private void Start()
    {
        _blenderAnimator = GetComponent<Animator>();
    }
    
    public void Lid(bool open)
    { 
        _blenderAnimator.SetBool("open", open);
    }
    
    public void Shake()
    {
        _blenderAnimator.Play("Blender_shake");
    }
}
