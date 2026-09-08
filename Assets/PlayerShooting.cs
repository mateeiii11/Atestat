using System.Threading;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform gunPoint_top;
    public Transform gunPoint_middle;
    public Transform gunPoint_low;
    public int count;
    public GameObject bulletPrefab;
    public float delay;
    float shootTime;
    public float forceMultiplier;
    public CameraShaker shakeInstance;
    public PlayerMovement movementInstance;
    public ParticleSystem ps;
    public bool isBoom;
    private void Awake()
    {
        count = 0;
        ps = GetComponent<ParticleSystem>();
        isBoom = false;
    }
    void Update()
    {
        var noise = ps.noise;
        if (Input.GetButtonDown("Fire1") && Time.time >= shootTime)
        {
            count++;
            shootTime = Time.time + delay;
            var strength = noise.strength.constant;
            strength += 1.6f;
            noise.strength = new ParticleSystem.MinMaxCurve(strength);
            if (count < 6)
            {
                isBoom = false;
                 Shoot();
            }
            else if (count == 6)
            {
                ShootBoom();
                isBoom = true;
                count = 0;
                noise.strength = new ParticleSystem.MinMaxCurve(0.5f);
            }
        }
    }

    void Shoot()
    {
        GameObject bulletMiddle = Instantiate(bulletPrefab, gunPoint_middle.transform.position, gunPoint_middle.transform.rotation);
        bulletMiddle.GetComponent<Rigidbody2D>().AddForce(gunPoint_middle.transform.right.normalized * -forceMultiplier, ForceMode2D.Impulse);
    }

    void ShootBoom()
    {
        StartCoroutine(shakeInstance.shake());
        GameObject bulletTop = Instantiate(bulletPrefab, gunPoint_top.transform.position, Quaternion.identity);
        GameObject bulletMiddle = Instantiate(bulletPrefab, gunPoint_middle.transform.position, Quaternion.identity);
        GameObject bulletLow = Instantiate(bulletPrefab, gunPoint_low.transform.position, Quaternion.identity);

        bulletTop.GetComponent<Rigidbody2D>().AddForce(gunPoint_top.transform.right.normalized * -15, ForceMode2D.Impulse);
        bulletMiddle.GetComponent<Rigidbody2D>().AddForce(gunPoint_middle.transform.right.normalized * -15, ForceMode2D.Impulse);
        bulletLow.GetComponent<Rigidbody2D>().AddForce(gunPoint_low.transform.right.normalized * -15, ForceMode2D.Impulse);

        movementInstance.AddForce(gunPoint_middle.right, 20f);

    }
}
