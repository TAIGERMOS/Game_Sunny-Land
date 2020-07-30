using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; //获取Player刚体
    private Animator anim;  //获取Player动画控制器
    public Collider2D coll; //获取Player的碰撞器
    public float speed; //变量：速度
    public float jumpforce; //变量：跳跃的力
    public LayerMask ground;    //获取地面

    public int CherryNum = 0;   //变量：吃到的樱桃变量


    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
    }

    //角色移动
    void Movement(){

        float horizontalmove = Input.GetAxis("Horizontal");

        float facedircetion = Input.GetAxisRaw("Horizontal");

        //角色移动
        if(horizontalmove != 0){
            

            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedircetion));
        }

        //人物的面向问题
        if(facedircetion != 0){

            transform.localScale = new Vector3(facedircetion, 1, 1);
        }

        //角色跳跃
        if(Input.GetButtonDown("Jump")){

            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }  
    }

    //跳跃下落转换
    void SwitchAnim(){
        
        anim.SetBool("idle", false);
        
        if(anim.GetBool("jumping") == true){
            if(rb.velocity.y < 0){
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true); 
            }
        }
        else if(coll.IsTouchingLayers(ground)){
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D item) {
        if(item.tag == "Collection"){
            Destroy(item.gameObject); //销毁当前游戏体
            CherryNum = CherryNum + 1;
        }
    }

}
