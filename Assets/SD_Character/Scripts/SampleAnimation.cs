using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAnimation : MonoBehaviour
{

    // Animator コンポーネント
    private Animator animator;

    // 設定したフラグの名前
    private const string key_isRun = "IsRun";
    private const string key_isAttack01 = "IsAttack01";
    private const string key_isAttack02 = "IsAttack02";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    // 初期化メソッド
    void Start()
    {
        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();
    }

    // 1フレームに1回コールされる
    void Update()
    {
        // 矢印上ボタンを押下している
        if (Input.GetKey(KeyCode.UpArrow)||(Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow)))
        {
            // IdleからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        else
        { 
            // RunからIdleに遷移する
            this.animator.SetBool(key_isRun, false);
        }

        // パンチ aを押す
        if (Input.GetKeyUp("a"))
        {
            //Attack01に遷移する
            this.animator.SetBool(key_isAttack01, true);
        }
        else
        {
            // Attack01からIdleに遷移する
            this.animator.SetBool(key_isAttack01, false);
        }
		
		// キック sを押す
        if (Input.GetKeyUp("s"))
        {
            //Attack02に遷移する
            this.animator.SetBool(key_isAttack02, true);
        }
        else
        {
            // Attack02からIdleに遷移する
            this.animator.SetBool(key_isAttack02, false);
        }
       
        // ジャンプ spaceを押す
        if (Input.GetKeyUp("space"))
        {
            //Jumpに遷移する
            this.animator.SetBool(key_isJump, true);
        }
        else
        {
            // JumpからIdleに遷移する
            this.animator.SetBool(key_isJump, false);
        }

        // ダメージ ｄを押す
        if (Input.GetKeyUp("d"))
        {
            //Damageに遷移する
            this.animator.SetBool(key_isDamage, true);
        }
        else
        {
            // DamageからIdleに遷移する
            this.animator.SetBool(key_isDamage, false);
        }

        // 死亡 fを押す
        if (Input.GetKeyUp("f"))
        {
            //Deadに遷移する
            this.animator.SetBool(key_isDead, true);
        }
    }
}