using UnityEngine;

public class TurretTracking : MonoBehaviour, ITurretState
{
    public TurretAI turret { get; set; }
    
    private float timer;
    public float shootCooldown = 1f;
    
    void Start()
    {
        turret = this.GetComponent<TurretAI>();
    }
    
    public void Enter()
    {
        
    }

    public void Stay()
    {
        LookAtTarget();
        ShootCooldown();
    }

    public void Exit()
    {
        
    }
    
    private void LookAtTarget()
    {
        var targetDir = (turret.currentTarget.position - this.transform.position).normalized;
        turret.headTf.rotation = Quaternion.Slerp(turret.headTf.rotation, Quaternion.LookRotation(targetDir), 0.1f);
    }

    private void ShootCooldown()
    {
        timer += Time.deltaTime;
        if (timer >= shootCooldown)
        {
            timer = 0f;
            turret.ChangeToAttack();
        }
    }
}