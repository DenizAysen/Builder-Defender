using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass 
{
    private static Camera _mainCamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if (_mainCamera == null) _mainCamera = Camera.main;

        Vector3 _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _mouseWorldPosition.z = 0f;
        return _mouseWorldPosition;
    }
    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
