using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Damage : MonoBehaviour {

    public static UI_Damage instance { get; private set; }
    private UILabel damageLabel;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        damageLabel = this.GetComponent<UILabel>();
        gameObject.SetActive(false);
    }

    public void Show(float value)
    {
        gameObject.SetActive(true);
        damageLabel.text = "-" + value;// value.ToString();  +""
        StartCoroutine(HitPanel());
    }


    IEnumerator HitPanel()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
