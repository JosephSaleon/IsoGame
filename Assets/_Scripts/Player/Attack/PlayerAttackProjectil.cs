using UnityEngine;

public class PlayerAttackProjectil : MonoBehaviour {

    [Header("Value")]
    // value
    public float range = 15f;
    private float fireCountdown = 0f;
    public float fireRate = 1f;
    private float turnSpeed = 6.5f;
    public bool auto = true;

    [Header("Ref")]
    // ref
    private PlayerInputManager playerInputManager;
    public Transform partToRotate;
    public Transform firePoint;
    public GameObject projectilPrefab;

    // ref update
    private Enemy targetEnemy;
    private Transform target;


    void Start () {
        if(auto){
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        } 
        playerInputManager = PlayerInputManager.Instance;

	}

    

    void PlayerShot()
    {
        if(playerInputManager.isAiming()){
            GameObject projectilObject = (GameObject)Instantiate(projectilPrefab, firePoint.position, firePoint.rotation);
            ProjectilAuto projectil = projectilObject.GetComponent<ProjectilAuto>();
        }
    }
	
	void Update () {
        PlayerShot();

		if(auto)
        {
            if(target != null){
                LockOnTarget();
            }
        }
        else
        {
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1 / fireRate;
        }
        fireCountdown -= Time.deltaTime;
	}


   

    

    void Shoot()
    {
        if(auto){
            GameObject projectilObject = (GameObject)Instantiate(projectilPrefab, firePoint.position, firePoint.rotation);
            ProjectilAuto projectil = projectilObject.GetComponent<ProjectilAuto>();
            if(projectil != null)
            {
                projectil.Seek(target);
            }
        }else{
            return;
        }
        
    }


     // Auto Shooting
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void UpdateTarget()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in ennemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}