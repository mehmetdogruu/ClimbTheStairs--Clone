using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public static CharacterAnimationController instance;
    private Animator _animator;

    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Awake()
    {
        instance = this;
        _animator = GetComponent<Animator>();
    }

    public void TriggerRun()
    {
        _animator.ResetTrigger(Idle);
        _animator.SetTrigger(Run);
    } 
    public void TriggerIdle()
    {
        _animator.ResetTrigger(Run);
        _animator.SetTrigger(Idle);
    } 
}
