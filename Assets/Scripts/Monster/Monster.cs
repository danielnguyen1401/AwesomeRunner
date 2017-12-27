﻿using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private GameObject deathFx;
    [SerializeField] Transform bullet;
    [SerializeField] private float distanceFromPlayerToStartMove = 20f;
    [SerializeField] float movementSpeedMin = 1f;
    [SerializeField] float movementSpeedMax = 2f;

    private bool moveRight;
    private float movementSpeed;
    private bool isPlayerInRegion;
    private Transform playerTransform;
    public bool canShoot;

    private string FUNCTION_TO_INVOKE = "StartShooting";

    void Start()
    {
        if (Random.Range(0.0f, 1.0f) > 0.5f)
            moveRight = true;
        else
            moveRight = false;

        movementSpeed = Random.Range(movementSpeedMin, movementSpeedMax);
        playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Update()
    {
        if (playerTransform)
        {
            float distanceFromPlayer = (playerTransform.position - transform.position).magnitude;
            if (distanceFromPlayer < distanceFromPlayerToStartMove)
            {
                if (moveRight)
                {
                    Vector3 temp = transform.position;
                    temp.x += Time.deltaTime * movementSpeed;
                    transform.position = temp;
                }
                else
                {
                    Vector3 temp = transform.position;
                    temp.x -= Time.deltaTime * movementSpeed;
                    transform.position = temp;
                }

                if (!isPlayerInRegion)
                {
                    if (canShoot)
                    {
                        InvokeRepeating(FUNCTION_TO_INVOKE, 0.5f, 1.5f);
                    }
                    isPlayerInRegion = true;
                }
            }
            else
            {
                CancelInvoke(FUNCTION_TO_INVOKE);
            }
        }
    }

    void StartShooting()
    {
        if (playerTransform)
        {
            Vector3 bulletPos = transform.position;
            bulletPos.y += 1.5f;
            bulletPos.x -= 1f;

            Transform newBullet = (Transform) Instantiate(bullet, bulletPos, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            newBullet.parent = transform;
        }
    }
}