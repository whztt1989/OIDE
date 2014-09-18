// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Clipping : Table {
  public static Clipping GetRootAsClipping(ByteBuffer _bb, int offset) { return (new Clipping()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Clipping __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public float NearClip() { int o = __offset(4); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0.0; }
  public float FarClip() { int o = __offset(6); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0.0; }

  public static void StartClipping(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddNearClip(FlatBufferBuilder builder, float nearClip) { builder.AddFloat(0, nearClip, 0.0); }
  public static void AddFarClip(FlatBufferBuilder builder, float farClip) { builder.AddFloat(1, farClip, 0.0); }
  public static int EndClipping(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
