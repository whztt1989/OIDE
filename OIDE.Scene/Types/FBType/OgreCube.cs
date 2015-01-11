// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class OgreCube : Table {
  public static OgreCube GetRootAsOgreCube(ByteBuffer _bb) { return (new OgreCube()).__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb); }
  public OgreCube __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public float Width() { int o = __offset(4); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }

  public static int CreateOgreCube(FlatBufferBuilder builder,
      float width = 0) {
    builder.StartObject(1);
    OgreCube.AddWidth(builder, width);
    return OgreCube.EndOgreCube(builder);
  }

  public static void StartOgreCube(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddWidth(FlatBufferBuilder builder, float width) { builder.AddFloat(0, width, 0); }
  public static int EndOgreCube(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
