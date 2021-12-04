using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{
    public float xSpeed;
    public float minYspeed;
    public float maxYspeed;
    public GameObject deathVfx;

    Rigidbody2D m_rb;
    // biến kiểm tra chim có di chuyển sang trái không
    bool m_moveLeftOnStart;

    // biến kiểm tra chim có die chưa
    bool m_isDead;

    private void Awake()
    {
        // tham chiếu đến Rigidbody2D của bird bằng phương thức Getcomponent
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomMovingDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // tốc độ bay của chim
        // xSpeed chim bay sang phải, -xSpeed chim bay sang trái
        m_rb.velocity = m_moveLeftOnStart ? new Vector2(-xSpeed, Random.Range(minYspeed, maxYspeed))
                                            : new Vector2(xSpeed, Random.Range(minYspeed, maxYspeed));

        Flip(); 
    }

    // Hàm random ngẫu nhiên hướng di chuyển của chim
    public void RandomMovingDirection()
    {
        // Nếu vị trí chim > 0(tức là bên phải) thì sẽ di chuyển sang trái và ngược lại
        m_moveLeftOnStart = transform.position.x > 0 ? true : false;
    }

    // kiểm tra hướng quay của chim
    void Flip()
    {
        if (m_moveLeftOnStart)
        {
            if (transform.localScale.x < 0)
                return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }else
        {
            if (transform.localScale.x > 0)
                return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void DieBird()
    {
        m_isDead = true;

        GameManager.Ins.BirdKilled++;

        Destroy(gameObject);

        if (deathVfx)
        {
           Instantiate(deathVfx, transform.position, Quaternion.identity);
        }

        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKilled);
          
    }
}
