using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// have to be included to use TextMeshProUGUI
using TMPro;

public class UITextFeedback : MonoBehaviour
{
    public delegate void GenerateFeedback(Transform transform, string text);
    public static event GenerateFeedback OnGenerateFeedback;

    // the speed at which the text is moving upwards
    private Vector3 moveSpeed = new Vector3(0, 0.75f, 0);
    private float RefreshRate = 0.005f;
    public float TimeToFade = 1f;

    private float _timeElapsed = 0f;

    TextMeshProUGUI _textMeshPro;
    private Color _startColour;
    // opacity factor of the new colour
    float _fadeAlpha = 1f;

    private RectTransform _textTransform;

    public void ResetColour()
    {
        Debug.Log(_startColour);
        _textMeshPro.color = _startColour;
    }

    private void Awake()
    {
        _textTransform = GetComponent<RectTransform>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _startColour = _textMeshPro.color;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += moveSpeed;
        _timeElapsed += RefreshRate;

        if (_timeElapsed < TimeToFade)
        {
            _fadeAlpha = _startColour.a * (1 - (_timeElapsed / TimeToFade));
            _textMeshPro.color = new Color(_startColour.r, _startColour.g
                                         , _startColour.b, _fadeAlpha);
        }
        else
        {
            // can look into how to change this implementation
            // into one that uses object pooling
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
