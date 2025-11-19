using UnityEngine;

public class AnimatedObjDestroyScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    void Start()
    {
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);  
    }
}
