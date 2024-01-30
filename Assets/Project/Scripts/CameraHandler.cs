using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cmVCamera;

    #region Privates
    private float orthographicSize;
    private float targetOrthographicSize;

    private bool _edgeScrolling;
    #endregion
    #region Singleton
    public static CameraHandler Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _edgeScrolling = PlayerPrefs.GetInt("edgeScrolling", 1) == 1;
    } 
    #endregion
    private void Start()
    {
        orthographicSize = cmVCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }
    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }
    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (_edgeScrolling)
        {
            float edgeScrolingSize = 30f;
            if (Input.mousePosition.x > Screen.width - edgeScrolingSize)
            {
                x = 1f;
            }
            if (Input.mousePosition.x < edgeScrolingSize)
            {
                x = -1f;
            }

            if (Input.mousePosition.y > Screen.height - edgeScrolingSize)
            {
                y = 1f;
            }
            if (Input.mousePosition.y < edgeScrolingSize)
            {
                y = -1f;
            }
        }
       
        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        float zoomAmount = 2f;

        targetOrthographicSize += Input.mouseScrollDelta.y * zoomAmount * -1f;

        float minOrthographicSize = 10f;
        float maxOrthographicSize = 30f;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cmVCamera.m_Lens.OrthographicSize = orthographicSize;
    }
    public void SetEdgeScrolling(bool edgeScrolling)
    {
        _edgeScrolling = edgeScrolling;
        PlayerPrefs.SetInt("edgeScrolling", edgeScrolling ? 1 : 0);
    }
    public bool GetEdgeScrolling() => _edgeScrolling;
}
