using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimation : MonoBehaviour
{
    private Animator m_Animator;
    private Vector2 oldPos;
    private const float tolerance = 1e-2f;
    public MovableObj obj;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        oldPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetBool("Walking", obj.GetDraggingState());
        //Debug.Log(gameObject.transform.position.x);
        //Debug.Log(oldPos.x);
        //Debug.Log(gameObject.transform.position.x - oldPos.x);
        if (gameObject.transform.position.x - oldPos.x > tolerance) 
        {
            m_Animator.SetFloat("Direction", 1);
        }
        else if(gameObject.transform.position.x - oldPos.x < -tolerance)
        {
            m_Animator.SetFloat("Direction", -1);
        }
        oldPos = gameObject.transform.position;
    }

    public void DeleteTrigger()
    {
        m_Animator.SetTrigger("Magic");
    }
}
