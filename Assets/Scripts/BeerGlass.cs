 using System;
using System.Collections.Generic;
using DG.Tweening;
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
    
    public float foamLineHeight;
    public float foamLineHeightOffset;
    public float foamLineLength;
    public float sampleCount;
    
    public float calculationTimeFrame;
    public int layerMask;
    private CountdownTimer checkInfluxTimer = new CountdownTimer(1f);
    private BoxCollider2D boxCollider;
    

    public int fullGlassMaxCount = 300;
    public float volumePerFluid = 10;

    private List<FoamParticle> foamParticle = new List<FoamParticle>();
    private List<GameObject> specialParticle = new List<GameObject>();
    private bool hasDispatchedFullEvent = false;

    [BoxGroup("Foam")] public int foamFormingInfluxMin;
    [BoxGroup("Foam")] public float airMultiplierPerExtraInflux;

    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _influx = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _remainFlux = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private float _airCount = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] private int _currentTotalCount = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] public int generatedFoamCount = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] public int fluidCount = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] public float lastFluidCount = 0;
    [BoxGroup("Debug"), ShowInInspector, ReadOnly] public int snakeCount;
    
    private void Awake()
    {
        layerMask = ~LayerMask.GetMask("FluidDetect", "Foam");
        checkInfluxTimer = new CountdownTimer(calculationTimeFrame);
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
        checkInfluxTimer.Start();
        lastFluidCount = 0;
        generatedFoamCount = 0;
        fluidCount = 0;
        hasDispatchedFullEvent = false; // 重置状态
        _airCount = 0;
        snakeCount = 0;
    }

    public float GetBeerVolumeResult()
    {
        return volumePerFluid * (fluidCount);
    }

    public float GetSnakeMultiplier()
    {
        return snakeCount * .5f + 1;   
    }
    public void CleanUpParticle()
    {
        foreach (var fp in foamParticle)
        {
            FoamParticlePool.Singleton.ReleaseFoamParticle(fp);
        }
        foamParticle.Clear();
        foreach (var g in specialParticle)
        {
            Destroy(g);
        }
        specialParticle.Clear();
    }

    private void Update()
    {
        _currentTotalCount = generatedFoamCount + fluidCount;
        if (checkInfluxTimer.IsFinished && !IsGlassFull())
        {
            var newFluidCount = fluidCount - lastFluidCount;
            _influx = newFluidCount / calculationTimeFrame;
            _remainFlux = Mathf.Max(0, _influx - (foamFormingInfluxMin));
            var air = _remainFlux * airMultiplierPerExtraInflux * calculationTimeFrame;
            _airCount += air;
            if (_airCount >= 1)
            {
                var count = Mathf.FloorToInt(_airCount);
                _airCount -= count;
                GenerateFoamParticle(count);
            }
            lastFluidCount = fluidCount;
            checkInfluxTimer.Start();
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
        if (!other) return;
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Fluids"))
        {
            if (IsGlassFull()) return;
            fluidCount += 1;
            specialParticle.Add(other.gameObject);
            //加球后尝试结算
            CheckGlassFull();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("SpecialItem"))
        {
            specialParticle.Add(other.gameObject);
            if (other.gameObject.CompareTag("Snake")) snakeCount += 1;
            else if (other.gameObject.CompareTag("ChopStick"))
            {
                foreach (var fp in foamParticle)
                {
                    fp.Fade();
                }
                foamParticle.Clear();
                generatedFoamCount = 0;
            }
            else if (other.gameObject.CompareTag("Mentos"))
            {
                GenerateFoamParticle(generatedFoamCount);
            }
        }
    }
    
    private bool CheckGlassFull()
    {
        var isFull = IsGlassFull();
        if (isFull && !hasDispatchedFullEvent) // 仅当未触发过事件时执行
        {
            Debug.LogWarning("show");
            QuickEvent.DispatchMessage(new BeerIsFullEvent());
            hasDispatchedFullEvent = true; // 标记事件已触发
        }
        return isFull;
    }
    public bool IsGlassFull()
    {
        return generatedFoamCount + fluidCount >= fullGlassMaxCount;
    }

    public void TriggerLiftTable(Player.Player player)
    {
        Sequence seq = DOTween.Sequence();
        var orign = transform.position;
        
        Reset();
        
        //clear all particle
        seq.Append(transform.DOMove(orign.Offset(y:5), 0.5f));
        seq.AppendCallback(() =>
        {
            player.beerGlass.CleanUpParticle();
            player.beerCan.CleanUpParticle();
        });
        seq.Append(transform.DOMove(orign, 1f));
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
