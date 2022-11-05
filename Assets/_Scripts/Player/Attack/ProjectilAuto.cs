using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectilAuto : MonoBehaviour {

    

    public GameObject impactEffect;
    public float speed = 70f;
    private System.Random rnd = new System.Random();

    public int damage;
    public float explosionRadius = 0f;
    [SerializeField] private float knockbackStrength = 2;

    private Transform target;
    private Rigidbody targetRigidbody;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Start(){
        damage = rnd.Next(0,100);
    }

	void Update () {
		if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            
            HitTarget();
            KnockBack();

            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
	} 

    void KnockBack(){
        targetRigidbody = target.GetComponent<Enemy>().GetComponent<Rigidbody>();
        Vector3 playerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        Vector3 dir = target.position - playerPosition;
        dir.y = 0;
        targetRigidbody.AddForce(dir.normalized * knockbackStrength, ForceMode.Impulse);
    }

    void HitTarget()
    {
        // GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Destroy(effectIns, 5f);

        if(explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if(e != null)
        {
            e.TakeDammage(damage);
        }
        else
        {
            Debug.LogError("Pas de script Enemy sur l'ennemi.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}