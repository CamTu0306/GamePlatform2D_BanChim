using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : Singleton<BackGroundController>
{
    public Sprite[] sprites;

    public SpriteRenderer bgImage; 

    // không cho lưu dữ liệu khi load sang sence khác
    // không giữ lại đối tượng BackGroundController khi load sang sence mới
    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        ChangeSprite();
    }

    // hàm thay đổi backgroung mỗi lần load 
    public void ChangeSprite()
    {
        if(bgImage != null && sprites != null && sprites.Length > 0)
        {
            int randomInx = Random.Range(0, sprites.Length);

            if(sprites[randomInx] != null)
            {
                bgImage.sprite = sprites[randomInx];
            }
        }
    }
}
