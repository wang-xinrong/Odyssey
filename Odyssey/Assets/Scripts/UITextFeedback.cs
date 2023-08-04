using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// have to be included to use TextMeshProUGUI
using TMPro;
using UnityEngine.UI;

public class UITextFeedback : MonoBehaviour
{
    // the speed at which the text is moving upwards
    //private Vector3 moveSpeed = new Vector3(0, 0.75f, 0);
    private float RefreshRate = 0.005f;
    private float TimeToFade = 2f;
    private float _timeElapsed = 0f;

    /*
    public Image Image;
    private Color _startColour2;
    // opacity factor of the new colour
    float _fadeAlpha2 = 1f;
    TextMeshProUGUI _textMeshPro;
    private Color _startColour;
    // opacity factor of the new colour
    float _fadeAlpha = 1f;

    private RectTransform _textTransform;
    private void Awake()
    {
        _textTransform = GetComponent<RectTransform>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _startColour = _textMeshPro.color;

        Image = GetComponentInParent<Image>();
        _startColour2 = Image.color;
    }
    */

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timeElapsed += RefreshRate;

        /*
        if (_timeElapsed < TimeToFade)
        {
            _fadeAlpha = _startColour.a * (1 - (_timeElapsed / TimeToFade));
            _textMeshPro.color = new Color(_startColour.r, _startColour.g
                                         , _startColour.b, _fadeAlpha);

            _fadeAlpha2 = _startColour2.a * (1 - (_timeElapsed / TimeToFade));
            Image.color = new Color(_startColour2.r, _startColour2.g
                                         , _startColour2.b, _fadeAlpha2);
        }
        else
        */
        if (_timeElapsed > TimeToFade)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
