using UnityEngine;

public class projectile2D : MonoBehaviour
{
    [SerializeField] Transform shootPoints;
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D bulletPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);


            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);


            if (hit.collider != null)
            {
                // set target image to new position
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log("Hit" + hit.collider.name);

                //CalculateProjectile
                Vector2 projectilevelocity = CalculateProjectileVelocity(shootPoints.position, hit.point, 1f);

                //shoot prefab using rigibody2d
                Rigidbody2D shootBullet = Instantiate(bulletPrefab,shootPoints.position,Quaternion.identity);

                //add projectile velocity vector to bullet rigibody
                shootBullet.linearVelocity = projectilevelocity;    
            }


        }
    }


    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 Target,float time)
    {
        Vector2 distance = Target - origin;

        //find velocity X , Y Axis
        float velocityX= distance.x/ time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        //get projectile vector
        Vector2 projectileVelocity = new Vector2(velocityX, velocityY);
        //give vector to projectile
        return projectileVelocity;  
    }
}
