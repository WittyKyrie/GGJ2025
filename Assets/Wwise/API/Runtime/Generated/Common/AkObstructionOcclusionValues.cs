#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AkObstructionOcclusionValues : global::System.IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkObstructionOcclusionValues(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkObstructionOcclusionValues obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~AkObstructionOcclusionValues() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkObstructionOcclusionValues(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public float occlusion { set { AkSoundEnginePINVOKE.CSharp_AkObstructionOcclusionValues_occlusion_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkObstructionOcclusionValues_occlusion_get(swigCPtr); } 
  }

  public float obstruction { set { AkSoundEnginePINVOKE.CSharp_AkObstructionOcclusionValues_obstruction_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkObstructionOcclusionValues_obstruction_get(swigCPtr); } 
  }

  public void Clear() { AkSoundEnginePINVOKE.CSharp_AkObstructionOcclusionValues_Clear(swigCPtr); }

  public static int GetSizeOf() { return AkSoundEnginePINVOKE.CSharp_AkObstructionOcclusionValues_GetSizeOf(); }

  public void Clone(AkObstructionOcclusionValues other) { AkSoundEnginePINVOKE.CSharp_AkObstructionOcclusionValues_Clone(swigCPtr, AkObstructionOcclusionValues.getCPtr(other)); }

  public AkObstructionOcclusionValues() : this(AkSoundEnginePINVOKE.CSharp_new_AkObstructionOcclusionValues(), true) {
  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.