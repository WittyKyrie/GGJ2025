using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FoamParticlePool : MonoBehaviour
{
    public static FoamParticlePool Singleton;
    [SerializeField] private GameObject foamParticlePrefab; // Prefab for the foam particle
    [SerializeField] private int defaultCapacity = 10;      // Initial pool size
    [SerializeField] private int maxSize = 50;             // Maximum pool size
    private ObjectPool<FoamParticle> foamParticlePool;       // The object pool
    
    
    private void Awake()
    {
        Singleton = this;
        // Initialize the object pool
        foamParticlePool = new ObjectPool<FoamParticle>(
            createFunc: CreateFoamParticle,            // Function to create a new object
            actionOnGet: OnGetFoamParticle,            // Action when an object is taken from the pool
            actionOnRelease: OnReleaseFoamParticle,    // Action when an object is returned to the pool
            actionOnDestroy: OnDestroyFoamParticle,    // Action when an object is destroyed
            defaultCapacity: defaultCapacity,          // Initial capacity of the pool
            maxSize: maxSize                           // Maximum pool size
        );
    }
    // Function to create a new foam particle
    private FoamParticle CreateFoamParticle()
    {
        GameObject foamParticle = Instantiate(foamParticlePrefab);
        foamParticle.transform.SetParent(transform);
        foamParticle.SetActive(false); // Set inactive initially
        return foamParticle.GetComponent<FoamParticle>();
    }

    // Action when an object is taken from the pool
    private void OnGetFoamParticle(FoamParticle foamParticle)
    {
        foamParticle.gameObject.SetActive(true); // Activate the object
    }

    // Action when an object is returned to the pool
    private void OnReleaseFoamParticle(FoamParticle foamParticle)
    {
        foamParticle.gameObject.SetActive(false); // Deactivate the object
    }

    // Action when an object is destroyed
    private void OnDestroyFoamParticle(FoamParticle foamParticle)
    {
        Destroy(foamParticle.gameObject); // Destroy the object
    }

    // Public method to get a foam particle from the pool
    public FoamParticle GetFoamParticle()
    {
        return foamParticlePool.Get();
    }

    // Public method to return a foam particle to the pool
    public void ReleaseFoamParticle(FoamParticle foamParticle)
    {
        foamParticlePool.Release(foamParticle);
    }
    
}