using System.Collections.Generic;
using UnityEngine;

namespace Controllers.SatelliteDodge {
    public class RadialForce : MonoBehaviour {

        public float mass = 1f;
        public static float range = 1000;
 
        void FixedUpdate () 
        {
            Collider2D[] cols  = Physics2D.OverlapCircleAll(this.transform.position, range); 
            List<Rigidbody2D> rbs = new List<Rigidbody2D>();
 
            foreach(Collider2D c in cols)
            {
                if (!c.gameObject.CompareTag("Player")) continue;
			
                Rigidbody2D rb = c.gameObject.GetComponent<Rigidbody2D>();
                if(rb != null && rb != this.GetComponent<Rigidbody2D>() && !rbs.Contains(rb))
                {	
                    rbs.Add(rb);
                    Vector2 offset = this.transform.position - c.transform.position;
                    if (offset.sqrMagnitude > 0.05) {
                        rb.AddForce(offset / offset.sqrMagnitude * this.mass);
                    }
                }
            }
        }
    }
}
