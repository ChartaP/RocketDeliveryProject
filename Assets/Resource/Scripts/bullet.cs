using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    public float fDamage = 20f;
    [SerializeField]
    private float fDrag = 0f;

    [SerializeField]
    private GameObject effect = null;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBullet(float damage,float drag)
    {
        fDamage = damage;
        fDrag = drag;
        transform.GetComponent<Rigidbody>().drag = fDrag;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == transform.tag)
            return;
        if (isDead == false)
        {
            Debug.Log(collision.transform.name);
            Instantiate(effect, transform.position, transform.rotation);
            isDead = true;
            Destroy(this.gameObject, 0.001f);
        }
    }
}
