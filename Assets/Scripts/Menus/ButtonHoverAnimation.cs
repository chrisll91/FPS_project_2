using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverShake : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;
    
    void Start()
    {
        
        
        
        if (animator == null)
        {

            animator = GetComponent<Animator>();
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("isHovering", true);
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {

        animator.SetBool("isHovering", false);
        

    }
}
