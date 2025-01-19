using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool; // Import the Unity ObjectPool namespace

public class FluidParticlePool : MonoBehaviour
{
    public static FluidParticlePool Singleton;

    [SerializeField] private GameObject fluidParticlePrefab; // Prefab for the fluid particle
    [SerializeField] private int defaultCapacity = 10;       // Initial pool size
    [SerializeField] private int maxSize = 50;              // Maximum pool size

    private ObjectPool<GameObject> fluidParticlePool;        // The object pool

    private void Awake()
    {
        Singleton = this;

        // Initialize the object pool
        fluidParticlePool = new ObjectPool<GameObject>(
            createFunc: CreateFluidParticle,            // Function to create a new object
            actionOnGet: OnGetFluidParticle,            // Action when an object is taken from the pool
            actionOnRelease: OnReleaseFluidParticle,    // Action when an object is returned to the pool
            actionOnDestroy: OnDestroyFluidParticle,    // Action when an object is destroyed
            defaultCapacity: defaultCapacity,           // Initial capacity of the pool
            maxSize: maxSize                            // Maximum pool size
        );
    }

    // Function to create a new fluid particle
    private GameObject CreateFluidParticle()
    {
        GameObject fluidParticle = Instantiate(fluidParticlePrefab);
        fluidParticle.transform.SetParent(transform);
        fluidParticle.SetActive(false); // Set inactive initially
        return fluidParticle;
    }

    // Action when an object is taken from the pool
    private void OnGetFluidParticle(GameObject fluidParticle)
    {
        fluidParticle.SetActive(true); // Activate the object
    }

    // Action when an object is returned to the pool
    private void OnReleaseFluidParticle(GameObject fluidParticle)
    {
        fluidParticle?.SetActive(false); // Deactivate the object
    }

    // Action when an object is destroyed
    private void OnDestroyFluidParticle(GameObject fluidParticle)
    {
        Destroy(fluidParticle); // Destroy the object
    }

    // Public method to get a fluid particle from the pool
    public GameObject GetFluidParticle()
    {
        return fluidParticlePool.Get();
    }

    // Public method to return a fluid particle to the pool
    public void ReleaseFluidParticle(GameObject fluidParticle)
    {
        fluidParticlePool.Release(fluidParticle);
    }
}
