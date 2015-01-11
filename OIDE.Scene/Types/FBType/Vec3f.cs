// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Vec3f : Struct {
  public Vec3f __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public float X() { return bb.GetFloat(bb_pos + 0); }
  public float Y() { return bb.GetFloat(bb_pos + 4); }
  public float Z() { return bb.GetFloat(bb_pos + 8); }

  public static int CreateVec3f(FlatBufferBuilder builder, float X, float Y, float Z) {
    builder.Prep(4, 12);
    builder.PutFloat(Z);
    builder.PutFloat(Y);
    builder.PutFloat(X);
    return builder.Offset();
  }
};


}
