using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerController : MonoBehaviour
{
    public GameObject OtherOption;

    private TextMeshProUGUI _myTextMeshProUGUI;

    // Start is called before the first frame update
    public void Start()
    {
        _myTextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void OnPointerEnter()
    {
        // Update colors in options
        OtherOption.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
        _myTextMeshProUGUI.color = Color.cyan;
    }
}
