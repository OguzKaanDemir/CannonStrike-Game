using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject[] Bullets;
    [SerializeField] Transform ShootPos;
    public float bulletSpeed;
    public void Shoot()
    {
        GameObject newBullet = Instantiate(Bullets[Random.Range(0, Bullets.Length)], ShootPos.position, ShootPos.rotation, GameObject.Find("Bullets").transform);
        Vector2 dir = (transform.rotation * Vector2.up) * bulletSpeed;
        newBullet.GetComponent<Rigidbody2D>().AddForce(dir);
    }
}
