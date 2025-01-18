using System.Collections.Generic;
using ImprovedTimers;
using Sirenix.OdinInspector;
using TimToolBox.DebugTool;
using TimToolBox.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

public class BeerGlass : MonoBehaviour
{
    public GameObject foamPrefab;
    public Transform foamFloatLine;
    public Transform generationTransform;
    
    public float generationRandomAngle;
    [FormerlySerializedAs("initialSpeed")] public float foamInitialSpeed;
    [FormerlySerializedAs("generationBubbleThreshold")] public int formFoamThreshold;
    
    public float foamLineHeight;
    public float foamLineHeightOffset;
    public float foamLineLength;
    public float sampleCount;
    
    public float calculationTimeFrame;
    public int layerMask;
    private CountdownTimer timer = new CountdownTimer(0.1f);
    private BoxCollider2D boxCollider;
    
    [ReadOnly] public float flux = 0;
    [ReadOnly] public float fluidCount = 0;
    [ReadOnly] public float lastFluidCount = 0; 
    
    private void Awake()
    {
        layerMask = ~LayerMask.GetMask("FluidDetect", "Foam");
        timer = new CountdownTimer(calculationTimeFrame);
        boxCollider = GetComponent<BoxCollider2D>();
        
        timer.Start();
    }
    
    private void Update()
    {
        if (timer.IsFinished)
        {
            var newCount = fluidCount - lastFluidCount;
            flux = newCount / calculationTimeFrame;
            var genCount = Mathf.FloorToInt(flux / formFoamThreshold);
            GenerateFoamParticle(genCount);
            lastFluidCount = fluidCount;
            timer.Start();
        }
        
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
        fluidCount += 1;
    }
    
    public void GenerateFoamParticle(int count)
    {
        //create the fluid and shoot it out to the y direction of dispensePositiion
        for (int i = 0; i < count; i++)
        {
            var genPos = generationTransform.position.Offset(x: Mathf.Lerp(-foamLineLength / 2f, foamLineLength / 2f, Random.Range(0f, 1f)));
            GameObject fluid = Instantiate(foamPrefab, genPos, Quaternion.identity);
            var direction = generationTransform.up;
            //rotate the direction by slight variation
            direction = Quaternion.AngleAxis(Random.Range(-generationRandomAngle, generationRandomAngle),Vector3.forward) * direction;
            fluid.GetComponent<Rigidbody2D>().AddForce(direction * foamInitialSpeed, ForceMode2D.Impulse);
            var fp = fluid.GetComponent<FoamParticle>();
            fp.waterSurface = foamFloatLine;
            fp.beerGlass = this;
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
