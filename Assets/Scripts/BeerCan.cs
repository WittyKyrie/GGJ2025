using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TimToolBox.DebugTool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BeerCan : MonoBehaviour
{
    public Transform dispenseTransform;
    public float dispenseRandomAngle;
    [BoxGroup("Disperse")]public Vector3 dispenseAngleThresholds; //x = min, y = perfect, z = max
    
    [BoxGroup("Pour")] public Vector2 pourRotationAccelerationRange;
    [BoxGroup("Pour")] public float pourRotationAccelerationMult;
    [BoxGroup("Pour")] public float particleInitVelocity;
    [FormerlySerializedAs("pourRotationAccelerationMultCurve")] [BoxGroup("Pour")] public AnimationCurve pourRotationAccelerationAmpCurve;
    [BoxGroup("BackTrack")] public float backtrackRotationAcceleration;
    [BoxGroup("Range")]public Vector2 rotationRange;
    [BoxGroup("Range")]public Vector2 rotationSpeedRange;

    public bool mirrored;
    public bool autoBindP1;
    public bool autoBindP2;
    
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private bool _pouring = false;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _pouringTimerCount = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _currentRotationAngle;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _currentRotationSpeed;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _currentAcceleration;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _ballPerSecond = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _rateCounter = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _dispenseRate = 0;
    private List<GameObject> fluidParticle = new List<GameObject>();
    private void Start()
    {
        Reset();
        if(autoBindP1) BindP1();
        if(autoBindP2) BindP2();
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
        _rateCounter = 0;
        _currentRotationAngle = 0;
        _currentRotationSpeed = 0;
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
        if (state == InputReader.KeyState.Down) _pouring = true;
        else if (state == InputReader.KeyState.Up) _pouring = false;
    }

    private void Update()
    {
        // Rotation logic
        {
            //handle change of acceleration
            _pouringTimerCount = _pouring ? _pouringTimerCount + Time.deltaTime * pourRotationAccelerationMult : 0;
            _currentAcceleration = Mathf.Clamp01(_pouringTimerCount);
            _currentAcceleration = Mathf.Lerp(pourRotationAccelerationRange.x, pourRotationAccelerationRange.y,pourRotationAccelerationAmpCurve.Evaluate(Mathf.Clamp01(_pouringTimerCount)));
            
            //handle change of rotationSpeed
            var rotationSpeedDelta = _pouring ? _currentAcceleration : backtrackRotationAcceleration;
            if (Mathf.Abs(_currentRotationAngle) >= rotationRange.y) _currentRotationSpeed = 0;
            if (Mathf.Abs(_currentRotationAngle) <= rotationRange.x) _currentRotationSpeed = 0;
            _currentRotationSpeed += rotationSpeedDelta * Time.deltaTime;
            _currentRotationSpeed = Mathf.Clamp(_currentRotationSpeed, rotationSpeedRange.x, rotationSpeedRange.y);
            
            //handle change of rotationAngle
            var rotationSpeed = _currentRotationSpeed;
            rotationSpeed *= mirrored ? -1 : 1; 
            _currentRotationAngle += rotationSpeed * Time.deltaTime;
            // Constrain rotation within rotationRange
            if (mirrored) _currentRotationAngle = Mathf.Clamp(_currentRotationAngle, -rotationRange.y, rotationRange.x);
            else _currentRotationAngle = Mathf.Clamp(_currentRotationAngle, (rotationRange.x), (rotationRange.y));
        
            transform.localRotation = Quaternion.Euler(0f, 0f, _currentRotationAngle); // Change axis as needed
        }
        
        
        //dispense logic
        _dispenseRate = 0;
        var abs = Mathf.Abs(_currentRotationAngle);
        //y = 0.05(x-90)^{2}
        if (abs > (dispenseAngleThresholds.x) && abs < (dispenseAngleThresholds.y))
            _dispenseRate = 0.033333f * Mathf.Pow(abs - dispenseAngleThresholds.x, 2);
        //y = (x-110)+20
        if (abs >= (dispenseAngleThresholds.y))
            _dispenseRate = 0.75f * (abs - dispenseAngleThresholds.y) + 30f;
        
        if (_dispenseRate > 0)
        {
            _rateCounter += Time.deltaTime * _dispenseRate;
            if (_rateCounter >= 1)
            {
                var count = Mathf.FloorToInt(_rateCounter);
                _rateCounter -= count;
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
            fluid.GetComponent<Rigidbody2D>().AddForce(direction * particleInitVelocity, ForceMode2D.Impulse);
            fluidParticle.Add(fluid);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis((mirrored? -1 : 1) * dispenseAngleThresholds.x, Vector3.forward) * (mirrored? Vector3.left : Vector3.right) * 2f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis((mirrored? -1 : 1) * dispenseAngleThresholds.y, Vector3.forward) * (mirrored? Vector3.left : Vector3.right) * 2f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis((mirrored? -1 : 1) * dispenseAngleThresholds.z, Vector3.forward) * (mirrored? Vector3.left : Vector3.right) * 2f);
    }
}