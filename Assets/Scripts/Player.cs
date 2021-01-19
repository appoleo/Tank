using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移动速度
    public float moveSpeed = 3;
    // 子弹旋转角度
    private Vector3 bulletEularAngles;
    // 子弹间隔计时器
    private float timeVal;
    // 受保护计时器
    private float defendTimeVal = 3;
    // 是否受保护的
    private bool isDefended = true;

    // 坦克的方向，上右下左
    public Sprite[] tankSprite;
    // 精灵渲染器
    private SpriteRenderer sr;
    // 子弹预置体
    public GameObject bulletPrefab;
    // 爆炸预置体
    public GameObject explosionPrefab;
    // 保护预置体
    public GameObject defendEffectPrefab;

    private void Awake()
    {
        // 获取组件的引用，初始化
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 保护控制
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
        // 攻击CD
        if (timeVal > 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 坦克攻击控制
    /// </summary>
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEularAngles));
            timeVal = 0;
        }
    }

    /// <summary>
    /// 坦克移动控制
    /// </summary>
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0) // 向左
        {
            sr.sprite = tankSprite[3];
            bulletEularAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0) // 向右
        {
            sr.sprite = tankSprite[1];
            bulletEularAngles = new Vector3(0, 0, -90);
        }

        // 禁止同时两个方向移动
        if (h != 0)
        {
            return;
        }

        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0) // 向下
        {
            sr.sprite = tankSprite[2];
            bulletEularAngles = new Vector3(0, 0, 180);
        }
        else if (v > 0) // 向上
        {
            sr.sprite = tankSprite[0];
            bulletEularAngles = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// 坦克死亡控制
    /// </summary>
    private void Die()
    {
        if (isDefended)
        {
            return;
        }
        // 爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // 死亡
        Destroy(gameObject);
    }
}
