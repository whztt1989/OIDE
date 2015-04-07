// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Light : Table {
  public static Light GetRootAsLight(ByteBuffer _bb) { return GetRootAsLight(_bb, new Light()); }
  public static Light GetRootAsLight(ByteBuffer _bb, Light obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public Light __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Colour ColourDiffuse() { return ColourDiffuse(new Colour()); }
  public Colour ColourDiffuse(Colour obj) { int o = __offset(4); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public Colour ColourSpecular() { return ColourSpecular(new Colour()); }
  public Colour ColourSpecular(Colour obj) { int o = __offset(6); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public Vec3f DirectionVector() { return DirectionVector(new Vec3f()); }
  public Vec3f DirectionVector(Vec3f obj) { int o = __offset(8); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }

  public static void StartLight(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddColourDiffuse(FlatBufferBuilder builder, int colourDiffuseOffset) { builder.AddStruct(0, colourDiffuseOffset, 0); }
  public static void AddColourSpecular(FlatBufferBuilder builder, int colourSpecularOffset) { builder.AddStruct(1, colourSpecularOffset, 0); }
  public static void AddDirectionVector(FlatBufferBuilder builder, int directionVectorOffset) { builder.AddStruct(2, directionVectorOffset, 0); }
  public static int EndLight(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
