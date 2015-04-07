// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class TransformStateData : Table {
  public static TransformStateData GetRootAsTransformStateData(ByteBuffer _bb) { return GetRootAsTransformStateData(_bb, new TransformStateData()); }
  public static TransformStateData GetRootAsTransformStateData(ByteBuffer _bb, TransformStateData obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public TransformStateData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Quat4f Rot() { return Rot(new Quat4f()); }
  public Quat4f Rot(Quat4f obj) { int o = __offset(4); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public Vec3f Loc() { return Loc(new Vec3f()); }
  public Vec3f Loc(Vec3f obj) { int o = __offset(6); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public Vec3f Scl() { return Scl(new Vec3f()); }
  public Vec3f Scl(Vec3f obj) { int o = __offset(8); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }

  public static void StartTransformStateData(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddRot(FlatBufferBuilder builder, int rotOffset) { builder.AddStruct(0, rotOffset, 0); }
  public static void AddLoc(FlatBufferBuilder builder, int locOffset) { builder.AddStruct(1, locOffset, 0); }
  public static void AddScl(FlatBufferBuilder builder, int sclOffset) { builder.AddStruct(2, sclOffset, 0); }
  public static int EndTransformStateData(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
