using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;


public class LocomotionManager : MonoBehaviour
{
    /*
    public string upperaction;
    public double upperpower;
    public string loweraction;
    public double lowerpower;
    */
    // パラメータ
    public float RunSpeed;
    // public float RotateSpeed;
    private int lookRightCount = 0;
    private float lastRotationTime = 0f;
    private int lookLeftCount = 0;
    public float rotationCooldown;
    public float rotationAngle;
    private Vector3 initialPosition;  // 初期位置
    private bool isWalking = false;  // 歩いているかどうか
    private Coroutine walkCoroutine;  // 歩くコルーチンの参照
    // ヘッドセット情報
    Quaternion headsetRotation;
    Vector3 xAxisDirection;

    // Start is called before the first frame update
    void Start()
    {
        //initialPosition = transform.position;  // 初期位置を保存
        //this.transform.position = new Vector3(0, 1.038f, -4.0859f);
        Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Update()
    {
        // 現在のキーボード情報
        var current = Keyboard.current;
        // aキーの入力状態取得
        var aKey = current.aKey;    
        // sキーの入力状態取得
        var sKey = current.sKey;
        // dキーの入力状態取得
        var dKey = current.dKey;
        // スペースキーの入力状態取得
        var spaceKey = current.spaceKey;

        headsetRotation = InputTracking.GetLocalRotation(XRNode.Head);
        xAxisDirection = headsetRotation * Vector3.right;

        // パラメータの更新

        /*
        mentalaction = EmotivUnityItf.MentalAction;
        mentalpower = EmotivUnityItf.MentalPower;
        eyeaction = EmotivUnityItf.EyeAction;
        /*
        upperaction = EmotivUnityItf.UpperAction;
        upperpower = EmotivUnityItf.UpperPower;
        loweraction = EmotivUnityItf.LowerAction;
        lowerpower = EmotivUnityItf.LowerPower;
        */

        /*
        if (spaceKey.wasPressedThisFrame && !isWalking)  // スペースキーが押され、歩いていない場合
        {
            isWalking = true;
            walkCoroutine = StartCoroutine(WalkForSeconds(8f));  // 8秒間歩くコルーチンを開始
        }
        */

        /*
        if (sKey.wasPressedThisFrame)  // Sキーが押された場合
        {
            if (walkCoroutine != null)
            {
                StopCoroutine(walkCoroutine);  // 歩くコルーチンを停止
            }
            this.transform.position = new Vector3(0, 0, -4.086f);  // 初期位置に戻る
            isWalking = false;
        }
        */

        if (dKey.wasPressedThisFrame)
        {
            LookRightDetected();
        }

        if (aKey.wasPressedThisFrame)
        {
            LookLeftDetected();
        }

        Locomotion();
        //Debug.Log(Time.time - lastRotationTime);
    }

    void Locomotion()
    {
        // 現在のキーボード情報
        var current = Keyboard.current;
        // wキーの入力状態取得
        var wKey = current.wKey;
        // mentalactionが"push"の時に走る
        if (wKey.isPressed)
        {    
            // ヘッドセットの向いている方向に進む
            //transform.position += xAxisDirection * RunSpeed * Time.deltaTime;
            //transform.position += forwardDirection * RunSpeed * Time.deltaTime;
            transform.position += transform.forward * RunSpeed * Time.deltaTime; 
        }
    }

    void LookRightDetected()
    {
        lookRightCount++;
        //Debug.Log(lookRightCount);
        if ((lookRightCount >= 1) && (Time.time - lastRotationTime >= rotationCooldown))
        {
            RotateR();
            lookRightCount = 0;  // カウントをリセット
        }
    }

    void RotateR()
    {
        transform.Rotate(0f, rotationAngle, 0f);
        lastRotationTime = Time.time;
    }

    void LookLeftDetected()
    {
        lookLeftCount++;
        //Debug.Log(lookLeftCount);

        if ((lookLeftCount >= 1) && (Time.time - lastRotationTime >= rotationCooldown))
        {
            RotateL();
            lookLeftCount = 0;  // カウントをリセット
        }
    }

    void RotateL()
    {
        transform.Rotate(0f, -rotationAngle, 0f);
        lastRotationTime = Time.time;
    }

    /*
    private IEnumerator WalkForSeconds(float seconds)
    {
        float endTime = Time.time + seconds;  // 歩く終了時間を計算
        
        while (Time.time < endTime)
        {
            //transform.position += xAxisDirection * RunSpeed * Time.deltaTime;
            //transform.position += forwardDirection * RunSpeed * Time.deltaTime;
            transform.position += transform.forward * RunSpeed * Time.deltaTime;  // 歩く
            yield return null;
        }

        this.transform.position = new Vector3(0, 0, -4.086f);  // 初期位置に戻る
        isWalking = false;
    }
    */
}
