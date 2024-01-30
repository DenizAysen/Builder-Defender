using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class ChromacticAberrationEffect : MonoBehaviour
{
    private Volume _volume;

    public static ChromacticAberrationEffect Instance { get; private set; }
    #region Unity Methods
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _volume = GetComponent<Volume>();
    }
    private void Update()
    {
        if (_volume.weight > 0)
        {
            float decreaseSpeed = 1f;
            _volume.weight -= Time.deltaTime * decreaseSpeed;
        }
    } 
    #endregion
    public void SetWeight(float weight)
    {
        _volume.weight = weight;
    }
}
