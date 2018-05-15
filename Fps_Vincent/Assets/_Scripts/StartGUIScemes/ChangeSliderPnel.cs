using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSliderPnel : MonoBehaviour {

    public float speed = 5;
    private float _FloDeltaPosion;        //累加位置
    private bool isMovingControl = false;


    void OnPress(bool boolIsRess)       //鼠标按下时为True
    {
        isMovingControl = boolIsRess;
        if (!boolIsRess)
        {
            _FloDeltaPosion = 0;
        }
    }
	void OnDrag(Vector2 vec)
    {
        if (isMovingControl)
        {
            _FloDeltaPosion += vec.x;
            this.transform.Translate(new Vector3(_FloDeltaPosion / 1000, 0, 0), Space.World);
        }
    }

   

    void Update()
    {
        //测试
        //print(transform.localPosition.x);
        //print(transform.position.x);
        //待改
        //运动受限
        if (this.transform.localPosition.x > 0)             //向右移动
        {
            //this.transform.localPosition = new Vector3(0, 0, 0);
            this.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * speed);
        }

        if (this.transform.localPosition.x <= -3200)        //向左移动
        {
            this.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-3200, 0, 0), Time.deltaTime * speed);
        }

        if(this.transform.localPosition.x < 0 && this.transform.localPosition.x >-800)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(-800, 0, 0), Time.deltaTime * speed);
            if(Mathf.RoundToInt(this.transform.localPosition.x) == -800)
            {
                this.transform.localPosition = new Vector3(-800, 0, 0);
            }
        }

        if (this.transform.localPosition.x < -800 && this.transform.localPosition.x > -1600)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(-800, 0, 0), Time.deltaTime * speed);
            if (Mathf.RoundToInt(this.transform.localPosition.x) == -1600)
            {
                this.transform.localPosition = new Vector3(-1600, 0, 0);
            }
        }

        if (this.transform.localPosition.x < -1600 && this.transform.localPosition.x > -2400)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(-2400, 0, 0), Time.deltaTime * speed);
            if (Mathf.RoundToInt(this.transform.localPosition.x) == -2400)
            {
                this.transform.localPosition = new Vector3(-2400, 0, 0);
            }
        }

        if (this.transform.localPosition.x < -2400 && this.transform.localPosition.x > -3200)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(-2400, 0, 0), Time.deltaTime * speed);
            if (Mathf.RoundToInt(this.transform.localPosition.x) == -3200)
            {
                this.transform.localPosition = new Vector3(-3200, 0, 0);
            }
        }
    }
}
