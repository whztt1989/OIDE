// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Fog : Table {
  public static Fog GetRootAsFog(ByteBuffer _bb, int offset) { return (new Fog()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Fog __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Mode() { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public Colour Colour() { return Colour(new Colour()); }
  public Colour Colour(Colour obj) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public int Start() { int o = __offset(8); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public int End() { int o = __offset(10); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public int Density() { int o = __offset(12); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }

  public static void StartFog(FlatBufferBuilder builder) { builder.StartObject(5); }
  public static void AddMode(FlatBufferBuilder builder, int mode) { builder.AddInt(0, mode, 0); }
  public static void AddColour(FlatBufferBuilder builder, int colourOffset) { builder.AddOffset(1, colourOffset, 0); }
  public static void AddStart(FlatBufferBuilder builder, int start) { builder.AddInt(2, start, 0); }
  public static void AddEnd(FlatBufferBuilder builder, int end) { builder.AddInt(3, end, 0); }
  public static void AddDensity(FlatBufferBuilder builder, int density) { builder.AddInt(4, density, 0); }
  public static int EndFog(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
