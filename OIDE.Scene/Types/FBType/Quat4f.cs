// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Quat4f : Struct {
  public Quat4f __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public float W() { return bb.GetFloat(bb_pos + 0); }
  public float X() { return bb.GetFloat(bb_pos + 4); }
  public float Y() { return bb.GetFloat(bb_pos + 8); }
  public float Z() { return bb.GetFloat(bb_pos + 12); }

  public static int CreateQuat4f(FlatBufferBuilder builder, float W, float X, float Y, float Z) {
    builder.Prep(4, 16);
    builder.PutFloat(Z);
    builder.PutFloat(Y);
    builder.PutFloat(X);
    builder.PutFloat(W);
    return builder.Offset();
  }
};


}
