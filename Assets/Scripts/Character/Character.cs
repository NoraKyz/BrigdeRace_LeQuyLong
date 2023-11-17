using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class Character : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    
    [Header("Properties")]
    [SerializeField] protected float speed;
    [SerializeField] List<CharacterBrick> bricks = new List<CharacterBrick>();
    
    protected CharacterAnimID currentAnimID;
    
    private static readonly int AnimID = Animator.StringToHash("AnimID");

    protected void Start()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {
        
    }

    protected void AddBrick()
    {
        
    }

    protected void RemoveBrick()
    {
        
    }

    protected void DropBrick()
    {
        
    }

    protected abstract void Move();

    protected void ChangeAnim(CharacterAnimID animID)
    {
        if (currentAnimID == animID)
        {
            return;
        }
        
        currentAnimID = animID;
        anim.SetInteger(AnimID, (int) animID);
    }
}
