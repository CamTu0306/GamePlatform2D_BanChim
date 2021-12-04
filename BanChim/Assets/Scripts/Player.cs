using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // độ trễ sau khi bắn
    public float fireRate;
    float m_curFireRate;

    // ống ngắm
    public GameObject viewFinder;

    bool m_isShooted;
    GameObject m_viewFinderClone;

    private void Awake()
    {
        m_curFireRate = fireRate;
    }

    private void Start()
    {
        // nếu viewfinder được tham chiếu khi chạy ct thì
        if (viewFinder)
        {
            // Khởi tạo game obj trên sence, tọa độ 0 0 0 giữa màn hình, k xoay
            m_viewFinderClone = Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
        }
    }

    private void Update()
    {

        // Chuyển đổi tọa đồ khi ng chơi ấn vào màn hình sang tọa độ của unity
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !m_isShooted)
        {
            Shot(mousePos);
        }

        // Nếu đã bắn 
        if (m_isShooted)
        {
            // Giảm dần firerate
            m_curFireRate -= Time.deltaTime;

            if(m_curFireRate <= 0)
            {
                m_isShooted = false;

                m_curFireRate = fireRate;
            }

            GameGUIManager.Ins.UpdateFireRate(m_curFireRate / fireRate);
        }

        if (m_viewFinderClone)
        {
            m_viewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }

    }

    void Shot(Vector3 mousePos)
    {
        m_isShooted = true;

        Vector3 ShootDirection = Camera.main.transform.position - mousePos;
        ShootDirection.Normalize();

        // Lấy tất cả thông tin của các obj mà đường raycast chạm phải
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, ShootDirection);

        if (hits != null && hits.Length > 0)
        {
            // Duyệt từng phần tử trong mảng hits
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                // Nếu phần tử trong mảng hits có va chạm vs obj và khoảng cách đến con trỏ chuột <= 0.4f 
                if (hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos)) <= 0.6f)
                {
                    Birds birds = hit.collider.GetComponent<Birds>();

                    if (birds)
                    {
                        birds.DieBird();
                    }
                }


            }

            CineController.Ins.ShakeTrigger();
        }

        AudioController.Ins.PlaySound(AudioController.Ins.shooting); 
    }
}
