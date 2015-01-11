// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Fog : Table {
  public static Fog GetRootAsFog(ByteBuffer _bb) { return (new Fog()).__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb); }
  public Fog __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public uint Mode() { int o = __offset(4); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }
  public Colour Colour() { return Colour(new Colour()); }
  public Colour Colour(Colour obj) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public uint Start() { int o = __offset(8); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }
  public uint End() { int o = __offset(10); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }
  public uint Density() { int o = __offset(12); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }

  public static int CreateFog(FlatBufferBuilder builder,
      uint mode = 0,
      int colour = 0,
      uint start = 0,
      uint end = 0,
      uint density = 0) {
    builder.StartObject(5);
    Fog.AddDensity(builder, density);
    Fog.AddEnd(builder, end);
    Fog.AddStart(builder, start);
    Fog.AddColour(builder, colour);
    Fog.AddMode(builder, mode);
    return Fog.EndFog(builder);
  }

  public static void StartFog(FlatBufferBuilder builder) { builder.StartObject(5); }
  public static void AddMode(FlatBufferBuilder builder, uint mode) { builder.AddUint(0, mode, 0); }
  public static void AddColour(FlatBufferBuilder builder, int colourOffset) { builder.AddOffset(1, colourOffset, 0); }
  public static void AddStart(FlatBufferBuilder builder, uint start) { builder.AddUint(2, start, 0); }
  public static void AddEnd(FlatBufferBuilder builder, uint end) { builder.AddUint(3, end, 0); }
  public static void AddDensity(FlatBufferBuilder builder, uint density) { builder.AddUint(4, density, 0); }
  public static int EndFog(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
