// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class PhysicsConstraint : Table {
  public static PhysicsConstraint GetRootAsPhysicsConstraint(ByteBuffer _bb) { return GetRootAsPhysicsConstraint(_bb, new PhysicsConstraint()); }
  public static PhysicsConstraint GetRootAsPhysicsConstraint(ByteBuffer _bb, PhysicsConstraint obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public PhysicsConstraint __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Target() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public sbyte Type() { int o = __offset(6); return o != 0 ? bb.GetSbyte(o + bb_pos) : (sbyte)0; }
  public Vec3f Pivot() { return Pivot(new Vec3f()); }
  public Vec3f Pivot(Vec3f obj) { int o = __offset(8); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public Vec3f Axis() { return Axis(new Vec3f()); }
  public Vec3f Axis(Vec3f obj) { int o = __offset(10); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public short Flag() { int o = __offset(12); return o != 0 ? bb.GetShort(o + bb_pos) : (short)0; }
  public float MinLimit(int j) { int o = __offset(14); return o != 0 ? bb.GetFloat(__vector(o) + j * 4) : (float)0; }
  public int MinLimitLength() { int o = __offset(14); return o != 0 ? __vector_len(o) : 0; }
  public float MaxLimit(int j) { int o = __offset(16); return o != 0 ? bb.GetFloat(__vector(o) + j * 4) : (float)0; }
  public int MaxLimitLength() { int o = __offset(16); return o != 0 ? __vector_len(o) : 0; }
  public bool DisableLinkedCollision() { int o = __offset(18); return o != 0 ? 0!=bb.Get(o + bb_pos) : (bool)false; }

  public static void StartPhysicsConstraint(FlatBufferBuilder builder) { builder.StartObject(8); }
  public static void AddTarget(FlatBufferBuilder builder, int targetOffset) { builder.AddOffset(0, targetOffset, 0); }
  public static void AddType(FlatBufferBuilder builder, sbyte type) { builder.AddSbyte(1, type, 0); }
  public static void AddPivot(FlatBufferBuilder builder, int pivotOffset) { builder.AddStruct(2, pivotOffset, 0); }
  public static void AddAxis(FlatBufferBuilder builder, int axisOffset) { builder.AddStruct(3, axisOffset, 0); }
  public static void AddFlag(FlatBufferBuilder builder, short flag) { builder.AddShort(4, flag, 0); }
  public static void AddMinLimit(FlatBufferBuilder builder, int minLimitOffset) { builder.AddOffset(5, minLimitOffset, 0); }
  public static int CreateMinLimitVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static void StartMinLimitVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddMaxLimit(FlatBufferBuilder builder, int maxLimitOffset) { builder.AddOffset(6, maxLimitOffset, 0); }
  public static int CreateMaxLimitVector(FlatBufferBuilder builder, float[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddFloat(data[i]); return builder.EndVector(); }
  public static void StartMaxLimitVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddDisableLinkedCollision(FlatBufferBuilder builder, bool disableLinkedCollision) { builder.AddBool(7, disableLinkedCollision, false); }
  public static int EndPhysicsConstraint(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
