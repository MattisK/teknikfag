using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class SpritBulletScript : MonoBehaviour
{
    Quaternion rotation;
    public float speed;
    public float lifeTime;
    public float dmg;
    public string type = "sprit";
    public float scaleFactor;

    // Start is called before the first frame update
    void Start()
    {
        scaleFactor = 1f;
        speed = 2.5f;
        lifeTime = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed*Time.deltaTime;
        transform.localScale = transform.localScale*(1+(scaleFactor*Time.deltaTime));
        lifeTime -= Time.deltaTime;
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1,1,1,lifeTime*3.3f);
        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "rhinovirus")
        {
            other.gameObject.GetComponent<RinovirusScript>().health -= dmg*0.8f;
        } else if(other.gameObject.tag == "stafylokker"){
            other.gameObject.GetComponent<Stafylokker>().health -= dmg*0.2f;
        }
    }
}
