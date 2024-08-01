using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent onHighlighted;
    [SerializeField] private UnityEvent onClick;
    [SerializeField] private RaycastObserver raycastObserver;
    private bool disableFlag;
    private bool blockFlag;
    private bool highlightFlag;
    private bool prevHighlightFlag;
    public bool HighlightEventFlag {  get; private set; }
    public bool ClickEventFlag { get; private set; }

    public void SetDisable(bool disable)
    {
        disableFlag = disable;
    }

    public void SetBlock(bool block)
    {
        blockFlag = block;
    }

    public void ResetButton()
    {
        animator.SetTrigger("Reset");
        blockFlag = false;
    }

    void Update()
    {
        animator.SetBool("Disable", disableFlag);
        ClickEventFlag = false;
        HighlightEventFlag = false;

        if (disableFlag)
        {

        }
        else
        {
            if (raycastObserver.IsTrigger)
            {
                highlightFlag = true;
            }
            else
            {
                highlightFlag = false;
            }

            if (!prevHighlightFlag &&  highlightFlag) 
            {
                onHighlighted.Invoke();
                HighlightEventFlag = true;
            }

            prevHighlightFlag = highlightFlag;

            animator.SetBool("Highlighted", highlightFlag);

            if (highlightFlag && !blockFlag)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetTrigger("Pressed");
                    ClickEventFlag = true;
                    blockFlag = true;
                    onClick.Invoke();
                }
            }
        }
    }
}
