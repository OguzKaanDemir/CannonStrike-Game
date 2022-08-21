using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    GameManager Manager;

    private void Start()
    {
        Manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            collision.gameObject.tag = "Untagged";
            Manager.currnetPoint += 1;
            StartCoroutine(Manager.CheckWin());
        }
    }
}
