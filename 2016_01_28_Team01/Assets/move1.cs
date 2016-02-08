using UnityEngine;
using System.Collections;

public class move1 : MonoBehaviour {


    private Rigidbody2D rigid;
    [SerializeField]
    private float liquidSpeed;

    [SerializeField]
    private float gasRiseSpeed;
    [SerializeField]
    private float gasMaxSideSpeed;
    [SerializeField]
    private float gasRateSideSpeed;

	// Use this for initialization
	void Start () {

        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //GasMove();
        //LiquidMove();
	}

    void LiquidMove()
    {
        var liquidVel = rigid.velocity;

        if (Input.GetKey(KeyCode.RightArrow))
            liquidVel.x = 1.0f * liquidSpeed;
        else if (Input.GetKey(KeyCode.LeftArrow))
            liquidVel.x = -1.0f * liquidSpeed;
        else
            liquidVel.x = 0.0f;

        rigid.velocity = liquidVel;
    }

    void GasMove()
    {
        var gasVel = rigid.velocity;

        if (Input.GetKey(KeyCode.RightArrow))
            gasVel.x += gasRateSideSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
            gasVel.x -= gasRateSideSpeed * Time.deltaTime;

        gasVel.y = gasRiseSpeed;

        rigid.velocity = gasVel;
    }
    void SquareIceMove()
    {

        var iceVel = rigid.angularVelocity;
        if (Input.GetKey(KeyCode.RightArrow))
            iceVel = -180.0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            iceVel = 180.0f;

        rigid.angularVelocity = iceVel;
    }
}
