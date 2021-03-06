// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Camera : Table {
  public static Camera GetRootAsCamera(ByteBuffer _bb) { return GetRootAsCamera(_bb, new Camera()); }
  public static Camera GetRootAsCamera(ByteBuffer _bb, Camera obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public Camera __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Clipping Clipping() { return Clipping(new Clipping()); }
  public Clipping Clipping(Clipping obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static int CreateCamera(FlatBufferBuilder builder,
      int clipping = 0) {
    builder.StartObject(1);
    Camera.AddClipping(builder, clipping);
    return Camera.EndCamera(builder);
  }

  public static void StartCamera(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddClipping(FlatBufferBuilder builder, int clippingOffset) { builder.AddOffset(0, clippingOffset, 0); }
  public static int EndCamera(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
