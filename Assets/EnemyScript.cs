using System;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    Rigidbody2D rb;
    Transform player;
    public float speed = 10f;
    public GameObject ImpactEffect;
    public int hp1 = 2, hp2 = 1, hp3 = 3;
    public ParticleSystem ps;
    bool isDead;
    float extraLife;
    GameObject Impact;
    CameraShaker shakeInstance;
    public ParticleSystem plusOne;
    float impactLife = 5f;
    public GameObject hpEffect;
    GameObject hpInst;
    PlayerMovement instance;
    playerStatus scoreinst;
    float haha;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
        ps = GetComponent<ParticleSystem>();
        isDead = false;
        shakeInstance = FindAnyObjectByType<CameraShaker>();
        instance = FindAnyObjectByType<PlayerMovement>();
        scoreinst = FindAnyObjectByType<playerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.transform.position.normalized);
        rb.position = Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.deltaTime);
        if (isDead == true && Time.time >= extraLife)
        {
            Destroy(gameObject);
        }
        if (Time.time >= impactLife)
        {
            Destroy(Impact);
        }
        if (Time.time >= haha)
            Destroy(hpInst);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            
            Impact = Instantiate(ImpactEffect, collision.transform.position, Quaternion.identity);
            impactLife += Time.time;
            Destroy(collision.gameObject);
            if (this.gameObject.CompareTag("Enemy1"))
                hp1--;
            if (this.gameObject.CompareTag("Enemy2"))
                hp2--;
            if (hp1 == 0)
            {
                StartCoroutine(shakeInstance.shake());
                for (int i = 0; i < transform.childCount - 1; i++)
                    gameObject.transform.GetChild(i).gameObject.SetActive(false);
                extraLife = Time.time + 1.5f;
                isDead = true;
                ps.Play();
                plusOne.Play();
                scoreinst.score += 2;
            }
            if (hp2 == 0)
            {
                StartCoroutine(shakeInstance.shake());
                for (int i = 0; i < transform.childCount - 1; i++)
                    gameObject.transform.GetChild(i).gameObject.SetActive(false);
                extraLife = Time.time + 1.5f;
                isDead = true;
                ps.Play();
                plusOne.Play();
                scoreinst.score += 1;
            }

            if (gameObject.CompareTag("Enemy3"))
            {
                if (FindAnyObjectByType<PlayerShooting>().isBoom == true)
                {
                    hp3--;
                    if (hp3 == 0)
                    {
                        StartCoroutine(shakeInstance.shake());
                        for (int i = 0; i < transform.childCount - 1; i++)
                            gameObject.transform.GetChild(i).gameObject.SetActive(false);
                        extraLife = Time.time + 1.5f;
                        isDead = true;
                        ps.Play();
                        plusOne.Play();
                        scoreinst.score += 3;
                    }
                }
                else
                    hp3 = 3;
            }

        }
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(shakeInstance.shake());
            hpInst = Instantiate(hpEffect, transform.position, Quaternion.identity);
            FindAnyObjectByType<playerStatus>().hp--;
            haha = Time.time + 3f;
            Vector2 pushDir = (((Vector2)instance.transform.position) - ((Vector2)transform.position)).normalized;
            instance.AddForce(pushDir, 20f);
        }
    }
}
