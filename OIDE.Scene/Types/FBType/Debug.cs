// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Debug : Table {
  public static Debug GetRootAsDebug(ByteBuffer _bb) { return (new Debug()).__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb); }
  public Debug __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public byte Show() { int o = __offset(4); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; }
  public byte ShowAABB() { int o = __offset(6); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; }

  public static int CreateDebug(FlatBufferBuilder builder,
      byte Show = 0,
      byte ShowAABB = 0) {
    builder.StartObject(2);
    Debug.AddShowAABB(builder, ShowAABB);
    Debug.AddShow(builder, Show);
    return Debug.EndDebug(builder);
  }

  public static void StartDebug(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddShow(FlatBufferBuilder builder, byte Show) { builder.AddByte(0, Show, 0); }
  public static void AddShowAABB(FlatBufferBuilder builder, byte ShowAABB) { builder.AddByte(1, ShowAABB, 0); }
  public static int EndDebug(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
