using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public enum TurretState { PATROL, TRACKING, ATTACK }
    public TurretState e_TurretState;

    private float turnSpeed = 1f;
    private float theta, timer;

    private Animator anim;
    public GameObject bulletPrefab;
    public ParticleSystem ps;
    public Transform shootTf;
    public float shootCooldown = 1f;
    
    public List<Transform> targets = new List<Transform>();
    public Transform headTf;

    public Transform currentTarget;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        switch (e_TurretState)
        {
            case TurretState.PATROL:
                RotationTurret();
                break;
            case TurretState.TRACKING:
                LookAtTarget();
                ShootCooldown();
                break;
            case TurretState.ATTACK:
                Shoot();
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MONSTER"))
        {
            targets.Add(other.transform);

            SetTarget();
            
            e_TurretState = TurretState.TRACKING;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MONSTER"))
        {
            SetTarget(other.transform);
        }
    }

    public void SetTarget(Transform prevTarget = null)
    {
        if (prevTarget != null)
            targets.Remove(prevTarget);

        if (targets.Count > 0)
            currentTarget = targets[0];
        else
            currentTarget = null;
    }

    private void RotationTurret()
    {
        theta += Time.deltaTime * turnSpeed;
        headTf.localRotation = Quaternion.Euler(Vector3.up * 60f * Mathf.Sin(theta));
    }

    private void LookAtTarget()
    {
        var targetDir = (currentTarget.position - this.transform.position).normalized;
        headTf.rotation = Quaternion.Slerp(headTf.rotation, Quaternion.LookRotation(targetDir), 0.1f);
    }

    private void ShootCooldown()
    {
        timer += Time.deltaTime;
        if (timer >= shootCooldown)
        {
            timer = 0f;
            e_TurretState = TurretState.ATTACK;
        }
    }
    
    private void Shoot()
    {
        ps.Play();
        anim.SetTrigger("Shoot");
        CreateBullet();

        e_TurretState = TurretState.TRACKING;
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootTf.position, shootTf.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(shootTf.forward * 50f, ForceMode.Impulse);
    }
}