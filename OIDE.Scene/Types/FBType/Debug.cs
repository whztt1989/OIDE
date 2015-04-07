// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Debug : Table {
  public static Debug GetRootAsDebug(ByteBuffer _bb) { return GetRootAsDebug(_bb, new Debug()); }
  public static Debug GetRootAsDebug(ByteBuffer _bb, Debug obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public Debug __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public bool Show() { int o = __offset(4); return o != 0 ? 0!=bb.Get(o + bb_pos) : (bool)false; }
  public bool ShowAABB() { int o = __offset(6); return o != 0 ? 0!=bb.Get(o + bb_pos) : (bool)false; }

  public static int CreateDebug(FlatBufferBuilder builder,
      bool Show = false,
      bool ShowAABB = false) {
    builder.StartObject(2);
    Debug.AddShowAABB(builder, ShowAABB);
    Debug.AddShow(builder, Show);
    return Debug.EndDebug(builder);
  }

  public static void StartDebug(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddShow(FlatBufferBuilder builder, bool Show) { builder.AddBool(0, Show, false); }
  public static void AddShowAABB(FlatBufferBuilder builder, bool ShowAABB) { builder.AddBool(1, ShowAABB, false); }
  public static int EndDebug(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
