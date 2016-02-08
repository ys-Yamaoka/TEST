using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private Vector3 vec;
    public Sprite[] charaSprite;
    public float temp;
    private RaycastHit2D hit;
    private SpriteRenderer _sprite;
    public GameObject[] fence;
    private Rigidbody2D _rigid;
    public bool climb;
    private bool climbMove;
    public GameObject[] _text;
    private Vector2 oldPos;
    public GameObject pushBox;

    // Use this for initialization
    void Start () {

        _sprite = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        vec = Vector3.zero;

        //常温の部屋
        //キャラクターが熱いとき
        if (temp < 101 && temp > 0) temp -= 2 * Time.deltaTime;
        //キャラクターが冷たいとき
        if (temp < 0 && temp > 101) temp += 2 * Time.deltaTime;

        //プレイヤーの性質変化
        //気体の場合
        if (temp > 50)
        {
           _sprite.sprite = charaSprite[1];
            //気体のプレイヤーの動き
            _rigid.gravityScale = -2.0f;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                vec.x = vec.x + 0.5f;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                vec.x = vec.x - 0.5f; 
            }
        }
        //液体の場合
        if (temp < 50 && temp > -50)
        {
            _sprite.sprite = charaSprite[0];
            _rigid.gravityScale = 2.0f;
            //液体のプレイヤー動き
            if (Input.GetKey(KeyCode.RightArrow))
            {
                vec.x++;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                vec.x--;
            }
            //木箱に近づいたとき
            if (climb)
            {
                //文字表示
                for (int i = 0; i < _text.Length; i++)
                {
                    _text[i].GetComponent<Text>().enabled = true;
                }
                //上がる
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    oldPos = transform.position;
                    climbMove = true;
                }
                //押すとき
                else if (Input.GetKey(KeyCode.Space))
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        pushBox.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        pushBox.transform.position += vec;
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        pushBox.transform.position -= vec;
                    }
                }
            }
            if (!climb)
            {
                for (int i = 0; i < _text.Length; i++)
                {
                    _text[i].GetComponent<Text>().enabled = false;
                }
            }
            //上がるときの動き
            if (climbMove)
            {
                if (oldPos.y + 1.0f > transform.position.y)
                {
                    vec.y+=10;
                }
                else if (oldPos.x + 1.0 > transform.position.x)
                {
                    vec.x+=10;
                }
                else climbMove=false;
            }
        }
        //固体の場合
        if (temp < -50)
        {
            _sprite.sprite = charaSprite[2];
            _rigid.gravityScale = 2.0f;
            //固体のプレイヤー動き
            SquareIceMove();
        }

        //ギミック
        //フェンス（網など）
        if (_sprite.sprite == charaSprite[2])
        {
            for (int i = 0; i < fence.Length; i++)
            {
                fence[i].GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
        else if (_sprite.sprite == charaSprite[0] || _sprite.sprite == charaSprite[1])
        {
            for (int i = 0; i < fence.Length; i++)
            {
                if (fence[i].GetComponent<BoxCollider2D>().isTrigger == false)
                {
                    fence[i].GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
        }
        transform.position += vec * Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.tag == "HotZone")
        {
            temp += 4 * Time.deltaTime;
        }
        if (col.transform.tag == "CoolZone")
        {
            temp += 4 * Time.deltaTime;
        }
        if (col.transform.tag == "Fire")
        {
            if (temp <= 100)
                temp += 300 * Time.deltaTime;
        }
        if (col.transform.tag == "Cold")
        {
            if (temp >= -100)
                temp -= 300 * Time.deltaTime;
        }
        if (col.transform.tag == "Climb")
        {
            climb = true;
            pushBox = col.gameObject;
            pushBox.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
    //void OnCollisionStay2D(Collision2D col)
    //{
    //}
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Climb")
        {
            climb = false;
            pushBox = null;
        }

    }
    void SquareIceMove()
    {

        var iceVel = _rigid.angularVelocity;
        if (Input.GetKey(KeyCode.RightArrow))
            iceVel = -180.0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            iceVel = 180.0f;

        _rigid.angularVelocity = iceVel;
    }
}
