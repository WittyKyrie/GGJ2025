using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class BeerCan : MonoBehaviour
{
    public Transform dispenseTransform;
    public float dispenseMultiplier;
    public AnimationCurve dispenseCurve;

    public float dispenseRandomAngle;
    [ReadOnly, ShowInInspector] private float dispenseRate;
    private float rateCounter;
    
    private bool pouring = false;
    private float currentRotationAngle;
    
    public float pourRotationSpeed;
    public float backtrackRotationSpeed;
    public Vector2 rotationRange;
    public float dispenseMinAngle;

    public bool mirrored;
    
    private List<GameObject> fluidParticle = new List<GameObject>();
    private void Start()
    {
        Reset();
    }

    private void OnEnable()
    {
        Reset();
    }
    
    private void OnDisable()
    {
        CleanUpParticle();
    }
    
    [Button]
    public void Reset()
    {
        rateCounter = 0;
        currentRotationAngle = 0;
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void CleanUpParticle()
    {
        foreach (var fp in fluidParticle)
        {
            FluidParticlePool.Singleton.ReleaseFluidParticle(fp);
        }
        fluidParticle.Clear();
    }

    public void UnBindP1()
    {
        InputReader.Instance.OnP1PourKeyInput -= OnPourKeyInput;
    }
    public void UnBindP2()
    {
        InputReader.Instance.OnP2PourKeyInput -= OnPourKeyInput;
    }
    [Button]
    public void BindP1()
    {
        mirrored = true;
        InputReader.Instance.OnP1PourKeyInput += OnPourKeyInput;
    }
    
    [Button]
    public void BindP2()
    {
        mirrored = false;
        InputReader.Instance.OnP2PourKeyInput += OnPourKeyInput;
    }
    
    private void OnPourKeyInput(InputReader.KeyState state)
    {
        if (state == InputReader.KeyState.Down) pouring = true;
        else if (state == InputReader.KeyState.Up) pouring = false;
    }

    private void Update()
    {
        // Rotation logic
        var rotationSpeed = pouring ? pourRotationSpeed : backtrackRotationSpeed;
        rotationSpeed *= mirrored ? -1 : 1; 
        currentRotationAngle += rotationSpeed * Time.deltaTime;
        // Constrain rotation within rotationRange
        if(mirrored) currentRotationAngle = Mathf.Clamp(currentRotationAngle, -rotationRange.y, rotationRange.x);
        else currentRotationAngle = Mathf.Clamp(currentRotationAngle, (rotationRange.x), (rotationRange.y));
        
        transform.localRotation = Quaternion.Euler(0f, 0f, currentRotationAngle); // Change axis as needed
        
        //get the rotation of z axis and set it as dispense rate
        var ratio = Mathf.Abs(currentRotationAngle - rotationRange.x) / Mathf.Abs(rotationRange.y - rotationRange.x);
        dispenseRate = dispenseCurve.Evaluate(ratio) * dispenseMultiplier;
        
        //dispense logic
        if (Mathf.Abs(currentRotationAngle) > (dispenseMinAngle))
        {
            rateCounter += Time.deltaTime * dispenseRate * dispenseMultiplier;
            if (rateCounter >= 1)
            {
                var count = Mathf.FloorToInt(rateCounter);
                rateCounter -= count;
                DispenseFluidParticle(count);
            }
        }
    }
    
    public void DispenseFluidParticle(int count)
    {
        //create the fluid and shoot it out to the y direction of dispensePositiion
        for (int i = 0; i < count; i++)
        {
            GameObject fluid =  FluidParticlePool.Singleton.GetFluidParticle();
            fluid.transform.position = dispenseTransform.position;
            var direction = dispenseTransform.up;
            //rotate the direction by slight variation
            direction = Quaternion.AngleAxis(UnityEngine.Random.Range(-dispenseRandomAngle, dispenseRandomAngle),Vector3.forward) * direction;
            fluid.GetComponent<Rigidbody2D>().AddForce(direction * 10, ForceMode2D.Impulse);
            fluidParticle.Add(fluid);
        }
    }
}
