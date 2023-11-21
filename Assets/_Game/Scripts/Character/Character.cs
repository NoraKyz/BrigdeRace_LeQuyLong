using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class Character : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected SkinnedMeshRenderer modelCharacter;
    [SerializeField] protected LayerMask stairLayer;
    
    [Header("Properties")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected ColorType colorType;
    [SerializeField] protected Transform brickHolder;
    
    protected Stack<CharacterBrick> bricks = new Stack<CharacterBrick>();
    
    protected string currentAnimName;
    public int BrickAmount => bricks.Count;
    public ColorType ColorType => colorType;

    protected void Start()
    {
        OnInit();
    }
    protected void LateUpdate()
    {
        RotateTowardMoveDirection();
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            OnTriggerCharacter(other);
        }
    }
    protected virtual void OnInit()
    {
        ChangeColor(colorType);
    }
    protected virtual void OnTriggerCharacter(Collider other) { }
    protected virtual void Move() { }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName == animName)
        {
            return;
        }
        
        anim.ResetTrigger(animName);
        currentAnimName = animName;
        anim.SetTrigger(animName);
    }
    protected void RotateTowardMoveDirection()
    {
        if (rb.velocity == Vector3.zero)
        {
            return;
        }
        
        Vector3 targetRotation = rb.velocity;
        targetRotation.y = 0;
        transform.rotation = Quaternion.LookRotation(targetRotation);
    }
    protected void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        modelCharacter.material = colorData.GetMaterial(colorType);
    }
    public void AddBrick()
    {
        CharacterBrick brick = SimplePool.Spawn<CharacterBrick>(
            PoolType.CharacterBrick, 
            GetNextBrickPosition(),
            Quaternion.identity, 
            brickHolder
        );
        brick.ChangeColor(colorType);
        bricks.Push(brick);
    }
    public void RemoveBrick()
    {
        SimplePool.Despawn(bricks.Pop());
    }
    public void DropBrick()
    {
        while (BrickAmount > 0)
        {
            
        }
    }
    public Vector3 GetNextBrickPosition()
    {
        if(BrickAmount == 0)
        {
            return Vector3.zero;
        }
        
        return bricks.Peek().transform.localPosition + Vector3.up * Constants.CharacterBrickHeight * 1.2f;
    }
}
