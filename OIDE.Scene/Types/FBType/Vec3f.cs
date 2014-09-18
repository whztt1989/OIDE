// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Vec3f : Table {
  public static Vec3f GetRootAsVec3f(ByteBuffer _bb, int offset) { return (new Vec3f()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Vec3f __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public float X() { int o = __offset(4); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0.0; }
  public float Y() { int o = __offset(6); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0.0; }
  public float Z() { int o = __offset(8); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0.0; }

  public static void StartVec3f(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddX(FlatBufferBuilder builder, float x) { builder.AddFloat(0, x, 0.0); }
  public static void AddY(FlatBufferBuilder builder, float y) { builder.AddFloat(1, y, 0.0); }
  public static void AddZ(FlatBufferBuilder builder, float z) { builder.AddFloat(2, z, 0.0); }
  public static int EndVec3f(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
