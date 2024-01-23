using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToolTipUI : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRectTransform;

    #region Privates
    private TextMeshProUGUI _textMeshPro;
    private RectTransform _backgroundRectTransfrom;
    private RectTransform _rectTransform;
    private ToolTipTimer _toolTipTimer;
    #endregion
    public static ToolTipUI Instance { get; private set; }
    #region Unity Methods
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        _backgroundRectTransfrom = transform.Find("background").GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();

        Hide();
    }
    private void Update()
    {
        HandleFollowMouse();

        if(_toolTipTimer != null)
        {
            _toolTipTimer.timer -= Time.deltaTime;
            if(_toolTipTimer.timer <= 0f)
            {
                Hide();
            }
        }
    } 
    #endregion
    private void HandleFollowMouse()
    {
        Vector2 anchoredPos = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPos.x + _backgroundRectTransfrom.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPos.x = canvasRectTransform.rect.width - _backgroundRectTransfrom.rect.width;
        }
        if (anchoredPos.y + _backgroundRectTransfrom.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPos.y = canvasRectTransform.rect.height - _backgroundRectTransfrom.rect.height;
        }

        _rectTransform.anchoredPosition = anchoredPos;
    }
    private void SetText(string tooltipText)
    {
        _textMeshPro.SetText(tooltipText);
        _textMeshPro.ForceMeshUpdate();

        Vector2 textSize = _textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8,8);
        _backgroundRectTransfrom.sizeDelta = textSize + padding;
    }
    public void Show(string tooltipText, ToolTipTimer toolTipTimer = null)
    {
        _toolTipTimer = toolTipTimer;
        gameObject.SetActive(true);
        SetText(tooltipText);
        HandleFollowMouse();
    }
    public void Hide() => gameObject.SetActive(false);

    public class ToolTipTimer
    {
        public float timer;
    }
}
