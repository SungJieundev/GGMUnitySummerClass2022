using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    protected Rigidbody2D _rigidbody;
    protected float _timeToLive;

    protected int _enemyLayer;
    protected int _obstacleLayer;

    protected bool _isDead = false;

    [SerializeField]
    protected BulletDataSO _bulletData;
    public BulletDataSO BulletData
    {
        get => _bulletData;
        set => _bulletData = value;
    }

    protected bool _isEnemy;
    public bool IsEnemy
    {
        get => _isEnemy;
        set => _isEnemy = value;
    }

    private void Awake()
    {
        _obstacleLayer = LayerMask.NameToLayer("Obstacle"); // 占쏙옙岺占? 占쏙옙占싱억옙占쏙옙 占쏙옙호占쏙옙 占싯아울옙占쏙옙
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    public override void Init()
    {
        _isDead = false;
        _timeToLive = 0;
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    private void FixedUpdate()
    {
        _timeToLive += Time.fixedDeltaTime;
        _rigidbody.MovePosition(transform.position + _bulletData.bulletSpeed * transform.right * Time.fixedDeltaTime);

        if(_timeToLive >= _bulletData.lifeTime)
        {
            _isDead = true;
            // Destroy(gameObject);
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead == true) return;

        IHittable hittable = collision.GetComponent<IHittable>();

        if (hittable != null && hittable.IsEnemy == IsEnemy)
            return;

        hittable?.GetHit(_bulletData.damage, gameObject);

        if (collision.gameObject.layer == _obstacleLayer)
            HitObstacle(collision);
        if (collision.gameObject.layer == _enemyLayer)
            HitEnemy(collision);

        _isDead = true;
        // Destroy(gameObject);
        PoolManager.Instance.Push(this);
    }

    private void HitEnemy(Collider2D col) // 占쏙옙占쏙옙 占싼억옙占쏙옙 占쏙옙占쏙옙占쏙옙占? 효占쏙옙
    {
        Vector2 randomOffest = Random.insideUnitCircle * 0.5f;
        Impact impact = PoolManager.Instance.Pop(_bulletData.impactEnemyPrefab.name) as Impact;
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        impact.SetPositionAndRotation(col.transform.position + (Vector3)randomOffest, rot);
        impact.SetScaleAndTime(Vector3.one * 0.7f, 0.2f);
    }

    private void HitObstacle(Collider2D col) // 占쏙옙岺占쏙옙占? 占싼억옙占쏙옙 占쏙옙占쏙옙占? 占쏙옙 효占쏙옙
    {
        // 占쏙옙占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 처占쏙옙占쏙옙 占쏙옙占썩서
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f);

        if(hit.collider != null)
        {
            Impact impact = PoolManager.Instance.Pop(_bulletData.impactObstaclePrefab.name) as Impact;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
            impact.SetPositionAndRotation(hit.point +(Vector2)transform.right * 0.5f, rot);
            impact.SetScaleAndTime(Vector3.one, 0.2f);
        }

    }
}