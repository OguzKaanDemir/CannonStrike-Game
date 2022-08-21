using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapManager : MonoBehaviour
{   
    public TrapType trapType;

    [SerializeField] Vector3 StartVal, EndVal;

    public float rotSpeed, moveSpeed;

    public bool go, back;

    public enum TrapType
    {
        Rotary, Moving, RotaryAndMoving, Fixed 
    }

    void Update()
    {
        TrapAction();
    }

    void TrapAction()
    {
        switch (trapType)
        {
            case TrapType.Rotary:
                Rotary();
                break;
            case TrapType.Moving:
                Moving();
                break;
            case TrapType.RotaryAndMoving:
                RotaryAndMoving();
                break;
            default:
                break;
        }
    }

    void Rotary()
    {
        transform.Rotate(new Vector3(0, 0, rotSpeed), Space.Self);
    }
    void Moving()
    {
        if (go)
        {
            go = false;
            transform.DOMove(StartVal, moveSpeed).OnComplete(() =>
            {
                back = true;   
            });
        }
        else if (back)
        {
            back = false;
            transform.DOMove(EndVal, moveSpeed).OnComplete(() =>
            {
                go = true;
            });
        }
    }
    void RotaryAndMoving()
    {
        Rotary();
        Moving();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,1), Random.Range(2, 3.5f)),ForceMode2D.Impulse);
        }
    }
}
