// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Colour : Table {
  public static Colour GetRootAsColour(ByteBuffer _bb, int offset) { return (new Colour()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Colour __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public float R() { int o = __offset(4); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0.0; }
  public float G() { int o = __offset(6); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0.0; }
  public float B() { int o = __offset(8); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0.0; }
  public float A() { int o = __offset(10); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0.0; }

  public static void StartColour(FlatBufferBuilder builder) { builder.StartObject(4); }
  public static void AddR(FlatBufferBuilder builder, float r) { builder.AddFloat(0, r, 0.0); }
  public static void AddG(FlatBufferBuilder builder, float g) { builder.AddFloat(1, g, 0.0); }
  public static void AddB(FlatBufferBuilder builder, float b) { builder.AddFloat(2, b, 0.0); }
  public static void AddA(FlatBufferBuilder builder, float a) { builder.AddFloat(3, a, 0.0); }
  public static int EndColour(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
