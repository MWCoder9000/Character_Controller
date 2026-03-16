using Unity.VisualScripting;
using UnityEngine;

public class HeardHitbox : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    LayerMask layermask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            Transform gunTransform = other.gameObject.transform.Find("Gun");
            other.GetComponent<EnemyPatrol>().SomethingHeard();
            if (other.tag == "Enemy")
            {

                Vector3 Enemy = other.gameObject.transform.position;
                Vector3 Player = playerTransform.transform.position;
                Vector3 Direction = (Enemy - Player).normalized;
                float dotProduct = Vector3.Dot(Enemy, Player);
                float angleBetweenInDegrees = Vector3.Angle(Enemy,
                Player);
                Vector3 RotationAxis = Vector3.Cross(transform.up, Direction);
                int clockwise = 1;
                if (RotationAxis.z < 0)
                {
                    clockwise = -1;
                }
                if (dotProduct < 0.8f)
                    other.gameObject.transform.Rotate(0, 0, angleBetweenInDegrees * clockwise);

                Ray ray = new Ray(gunTransform.position, gunTransform.forward);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, 6, layermask))
                {

                    other.GetComponent<EnemyPatrol>().TargetSeen(true);
                }
            }
        }
    }
}
