using System;
using System.Collections.Generic;
using ImprovedTimers;
using Sirenix.OdinInspector;
using TimToolBox.DebugTool;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using Util.EventHandleSystem;
using Random = UnityEngine.Random;

public class BeerGlass : MonoBehaviour
{
    public Transform foamFloatLine;
    public Transform generationTransform;
    
    public float generationRandomAngle;
    public float foamInitialSpeed;
    public int formFoamThreshold;
    
    public float foamLineHeight;
    public float foamLineHeightOffset;
    public float foamLineLength;
    public float sampleCount;
    
    public float calculationTimeFrame;
    public int layerMask;
    private CountdownTimer timer = new CountdownTimer(0.1f);
    private BoxCollider2D boxCollider;
    
    [ReadOnly] public float flux = 0;
    [ReadOnly] public int fluidCount = 0;
    [ReadOnly] public int generatedFoamCount = 0;
    [ReadOnly] public float lastFluidCount = 0;

    public int fullGlassMaxCount = 300;
    public float volumePerFluid = 10;

    private List<FoamParticle> foamParticle = new List<FoamParticle>();
    
    private void Awake()
    {
        layerMask = ~LayerMask.GetMask("FluidDetect", "Foam");
        timer = new CountdownTimer(calculationTimeFrame);
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        Reset();
    }

    private void OnDisable()
    {
        CleanUpParticle();
    }

    public void Reset()
    {
        timer.Start();
        lastFluidCount = 0;
        generatedFoamCount = 0;
        fluidCount = 0;
    }

    public float GetBeerVolumeResult()
    {
        return volumePerFluid * (fluidCount);
    }
    
    public void CleanUpParticle()
    {
        foreach (var fp in foamParticle)
        {
            FoamParticlePool.Singleton.ReleaseFoamParticle(fp);
        }
        foamParticle.Clear();
    }

    private void Update()
    {
        if (timer.IsFinished && !IsGlassFull())
        {
            var newCount = fluidCount - lastFluidCount;
            flux = newCount / calculationTimeFrame;
            var genCount = Mathf.FloorToInt(flux / formFoamThreshold);
            GenerateFoamParticle(genCount);
            lastFluidCount = fluidCount;
            timer.Start();
        }
        //泡泡后结算
        if (CheckGlassFull()) { return; }
        
         //from the current float line, do a couple ray cast to sample the height of the fluid
         var origin = foamFloatLine.position.Offset(y:foamLineHeightOffset);
         var minX = origin.Offset(-foamLineLength / 2);
         var maxX = origin.Offset(foamLineLength / 2);
         
         Vector2 totalPoints = Vector2.zero;
         for (int i = 0; i < sampleCount; i++)
         {
             var start = Vector3.Lerp(minX, maxX, i / (float)sampleCount);
             RaycastHit2D hit = Physics2D.Raycast(start,Vector2.down, foamLineHeight,layerMask);
             DebugExtension.DrawSphere(start, Quaternion.identity, 0.1f, Color.magenta);
             Debug.DrawLine(start, start.Offset(y:-foamLineHeight), Color.magenta);
             if (hit.collider)
             {
                 DebugExtension.DrawSphere(hit.point, Quaternion.identity, 0.1f, Color.red);
                 totalPoints += hit.point;
             }
         }
         if (totalPoints != Vector2.zero)
         {
             var sampledHeight = (totalPoints / sampleCount).y;
             foamFloatLine.transform.position = transform.position.Set(y:sampledHeight + foamLineHeightOffset);
         }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsGlassFull()) return;
        fluidCount += 1;
        //加球后尝试结算
        CheckGlassFull();
    }

    private bool CheckGlassFull()
    {
        var val = IsGlassFull();
        if (val)
        {
            QuickEvent.DispatchMessage(new BeerIsFullEvent());
        }
        return val;
    }
    public bool IsGlassFull()
    {
        return generatedFoamCount + fluidCount >= fullGlassMaxCount;
    }
    
    public void GenerateFoamParticle(int count)
    {
        //create the fluid and shoot it out to the y direction of dispensePositiion
        for (int i = 0; i < count; i++)
        {
            var genPos = generationTransform.position.Offset(x: Mathf.Lerp(-foamLineLength / 2f, foamLineLength / 2f, Random.Range(0f, 1f)));

            FoamParticle fp =FoamParticlePool.Singleton.GetFoamParticle();
            fp.transform.position = genPos;
            var direction = generationTransform.up;
            direction = Quaternion.AngleAxis(Random.Range(-generationRandomAngle, generationRandomAngle),Vector3.forward) * direction;
            fp.GetComponent<Rigidbody2D>().AddForce(direction * foamInitialSpeed, ForceMode2D.Impulse);
            fp.waterSurface = foamFloatLine;
            fp.beerGlass = this;
            foamParticle.Add(fp);
            generatedFoamCount += 1;
            if(CheckGlassFull()) return;
        }
    }

    public bool ContainsPoint(Vector3 point)
    {
        var b = boxCollider.bounds;
        b.SetMinMax(b.min, b.max.Set(y:foamFloatLine.position.y));
        DebugExtension.DrawBox(b.center,Quaternion.identity, b.size, Color.cyan);
        return b.Contains(point);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        var origin = foamFloatLine.position.Offset(x:-foamLineLength/2);
        Gizmos.DrawLine(origin, origin.Offset(x:foamLineLength));
    }
}
