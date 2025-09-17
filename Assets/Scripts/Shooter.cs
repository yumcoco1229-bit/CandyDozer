using System.Collections;
using UnityEngine;
using TMPro;

public class Shooter : MonoBehaviour
{
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;
    int shotPower = MaxShotPower;
    AudioSource shotSound;
    public GameObject[] candyPrefabs;
    public Transform candyParentTransform;//ごちゃつくから親セット
    public CandyManager candyManager;
    public float shotForce;
    public float shotTorque;
    public float baseWidth;
    public TextMeshProUGUI powerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void start()
    {
        shotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shot();
    }
    GameObject SampleCandy()
    {
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
        //candyPrefabの中から1種類GameObect返すメソッド
    }
    Vector3 GetInstantiatePosition()
    {   //baseWidhthはおしだし台の幅
        //変なとこ押されても大丈夫な設定
        //ここのtransform.positionはくっつけたオブジェクトの位置
        //Screen.widthは画面の一番右 画面の何％のところクリックされたか
        float x = baseWidth *
        (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }
    public void Shot()
    {
        if (candyManager.GetCandyAmount() <= 0) return;
        if (shotPower <= 0) return;

        GameObject candy = (GameObject)Instantiate(
        //何をどこにどの向きで
        SampleCandy(),
        GetInstantiatePosition(),//このスクリプトがついてるところ
        Quaternion.identity//回転これは型(0,0,0)他のだと方向性失われることも
                           //ベクトル用意してそれを軸に回すEulerだと決められる
                           //Debug.Break()一時停止
    );
        //親の設定//親の座標と一緒にするか決められる

        candy.transform.parent = candyParentTransform;
        //candy.transform.SetParent = (candyParentTransform false);
        //こっちのほうが高機能 親子関係＝剣持ったら子要素になってる一緒に
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        //いきなりでてきたtransformeはそのオブジェクトのtransform　z軸力方向
        //transform.foward 重要
        candyRigidBody.AddForce(transform.forward * shotForce);
        //Torque 回転力（y軸回転）コイルの法則と一緒らしい
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));
        candyManager.ConsumeCandy();
        ConsumerPower();
        shotSound.Play();

    }
    //void OnGUI()
    //{
    //    GUI.color = Color.black;
    //   string label = "";
    //  for (int i = 0; i < shotPower; i++) label = label + "+";
    // GUI.Label(new Rect(50, 65, 100, 30), label);

    //}
    void DrawPower()
    {
        string label = "";
        for (int i = 0; i < shotPower; i++)
        {
            label = label + "+";

        }
        powerText.text = label;
    }
    void ConsumerPower()
    {
        shotPower--;
        StartCoroutine(RecoverPower());
    }
    IEnumerator RecoverPower()
    {
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
  }
        
        
}
