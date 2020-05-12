using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 _direction;
    Transform _transform;
    Camera _camera;

    public int projectileSpeed;

    private float _time;
    private Vector3 _stepVector;
    private int _liveTime;

    //We set values in a Init method. Virtual, so we can extend it later :)
    public virtual Projectile Init(Vector3 direction)
    {
        this._direction = direction;
        this.projectileSpeed = 10;
        this._liveTime = 5;
        this._transform = this.transform;
        this._time = Time.time;
        this._stepVector = _transform.position - _direction;
        return this;
    }

    void Start()
    {
        this._transform = this.transform;
        this._camera = Camera.main;
        this.Rotate();
    }

    void Update()
    {
        float step = projectileSpeed * Time.deltaTime;
        Vector3 position = _transform.position;
        position = Vector3.MoveTowards(position, position - _stepVector, step);
        _transform.position = position;

        if (Time.time - this._liveTime > this._time)
        {
            if (PrefabUtility.IsPartOfPrefabInstance(transform))
            {
                //if a part of a prefab instance then get the instance handle
                Object prefabInstance = PrefabUtility.GetPrefabInstanceHandle(transform);
                //destroy the handle
                Destroy(prefabInstance);
            }

            //the usual destroy immediate to clean up scene objects
            Destroy(transform.gameObject);
        }
    }


    void Rotate()
    {
        Vector3 pos = this._transform.position + this._direction;
        float AngleRad = Mathf.Atan2(pos.y - this._transform.position.y, pos.x - this._transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this._transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
    }
}