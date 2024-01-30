using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance { 
        get { 
            if(_instance == null)
            {
                _instance = Resources.Load<GameAssets>("GameAssets");
            }
            return _instance;        
        }
    }

    public Transform enemyPrefab;
    public Transform enemyDieParticlesPrefab;

    public Transform arrowProjectilePrefab;

    public Transform buildingDestroyedParticlesPrefab;
    public Transform buildingConstructionPrefab;
    public Transform buildingPlacedParticles;
}
