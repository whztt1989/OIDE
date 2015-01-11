// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Quat4f : Table {
  public static Quat4f GetRootAsQuat4f(ByteBuffer _bb) { return (new Quat4f()).__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb); }
  public Quat4f __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public float W() { int o = __offset(4); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float X() { int o = __offset(6); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float Y() { int o = __offset(8); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float Z() { int o = __offset(10); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }

  public static int CreateQuat4f(FlatBufferBuilder builder,
      float w = 0,
      float x = 0,
      float y = 0,
      float z = 0) {
    builder.StartObject(4);
    Quat4f.AddZ(builder, z);
    Quat4f.AddY(builder, y);
    Quat4f.AddX(builder, x);
    Quat4f.AddW(builder, w);
    return Quat4f.EndQuat4f(builder);
  }

  public static void StartQuat4f(FlatBufferBuilder builder) { builder.StartObject(4); }
  public static void AddW(FlatBufferBuilder builder, float w) { builder.AddFloat(0, w, 0); }
  public static void AddX(FlatBufferBuilder builder, float x) { builder.AddFloat(1, x, 0); }
  public static void AddY(FlatBufferBuilder builder, float y) { builder.AddFloat(2, y, 0); }
  public static void AddZ(FlatBufferBuilder builder, float z) { builder.AddFloat(3, z, 0); }
  public static int EndQuat4f(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
