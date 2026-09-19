using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;

    public void Move()
    {
        
    }

}
