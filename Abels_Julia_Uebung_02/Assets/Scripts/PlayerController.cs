using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Transform _transform;
    Camera _camera;

    private float _shootTimer;
    public Projectile projectile;
    public int speedShip;

    void Start()
    {
        this._transform = transform;
        this._camera = Camera.main;
        this._shootTimer = Time.time;
    }

    //Standard UpdateLoop (once per Frame)
    void Update()
    {
        this.Rotate();
        this.Move();
        if (Input.GetMouseButtonDown(0))
        {
            this.Shoot();
        }
    }

    void Rotate(){
        Vector2 mousePos = this._camera.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - this._transform.position.y, mousePos.x - this._transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this._transform.rotation = Quaternion.Euler(0, 0, angleDeg - 90);//diese -90 sind nötig für Sprites, die nach oben zeigen. Nutzen Sie andere Assets, könnte es sein, dass die das anpassen müssen
        
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        this._transform.position += new Vector3(h, v, 0) * (speedShip * Time.deltaTime);
    }

    private void Shoot()
    {
        if (this._shootTimer <= Time.time)
        {
            Vector2 mousePos = this._camera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = new Vector3(
                mousePos.x,
                mousePos.y,
                0
            );
            Projectile p = Instantiate(projectile, _transform.position, Quaternion.identity);
            p.Init(direction);

            this._shootTimer = Time.time + 2;
        }
    }
}
