using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float RawDamage = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "UI" && other.tag != "Untagged")
        {
            if (other.tag == "Enemy")
            {
                other.SendMessageUpwards("Hit", RawDamage, SendMessageOptions.DontRequireReceiver);

            }
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
