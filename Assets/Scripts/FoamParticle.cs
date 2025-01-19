using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FoamParticle : MonoBehaviour
{
    public float fluidDensity = 1f;    // Density of the fluid (e.g., water density is ~1)
    public float gravityScale = 9.81f; // Gravity scale (default is Earth's gravity)
    public float particleVolume = 1f; // Volume of the particle (adjust based on size)
    private Rigidbody2D rb;
    
    public Transform waterSurface;
    public BeerGlass beerGlass;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = Vector3.zero;
        transform.DOScale(0.8f, 2f);
    }

    private void FixedUpdate()
    {
        if (!waterSurface) return;
        
        if (!beerGlass.ContainsPoint(transform.position)) return;
        float submergedDepth = Mathf.Max(0, waterSurface.position.y - transform.position.y);
        // Calculate the buoyancy force: F = fluidDensity * gravityScale * submergedVolume
        float buoyancyForce = fluidDensity * gravityScale * submergedDepth * particleVolume;
        // Apply the buoyancy force upwards
        rb.AddForce(Vector2.up * buoyancyForce);
    }
}
