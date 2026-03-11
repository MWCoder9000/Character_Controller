using UnityEngine;

public class HeardHitbox : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform, gunTransform, EnemyTransform;
    LayerMask layermask;
    EnemyPatrol enemyPatrol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunTransform = gameObject.transform.Find("Gun");
        enemyPatrol = EnemyTransform.GetComponent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyPatrol.SomethingHeard();
        if (other.tag == "Player")
        {
            Vector3 lookVector = playerTransform.position - gameObject.transform.position;
            lookVector.y = transform.position.y;
            Quaternion rotation = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.01f);

            Ray ray = new Ray(gunTransform.position, gunTransform.forward);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, 6, layermask))
            {
                enemyPatrol.TargetSeen(true);
            }
        }
    }
}
