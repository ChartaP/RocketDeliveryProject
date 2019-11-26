using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float fDamage = 20f;
    [SerializeField]
    private float fDrag = 0f;
    [SerializeField]
    private float fLifeTime = 10.0f;

    [SerializeField]
    private GameObject effect = null;
    

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, fLifeTime);
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
        if (!(collision.transform.tag == "Untagged" || collision.transform.tag == "Terrain" || collision.transform.tag == "Item" || collision.transform.tag == "Regdoll"))
        {
            collision.transform.GetComponent<ObjectCtrl>().GetDamage(fDamage);
        }

        Instantiate(effect, transform.position, transform.rotation);
        Destroy(this.gameObject, 0.001f);
    }
}
