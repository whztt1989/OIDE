// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Colour : Struct {
  public Colour __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public float R() { return bb.GetFloat(bb_pos + 0); }
  public float G() { return bb.GetFloat(bb_pos + 4); }
  public float B() { return bb.GetFloat(bb_pos + 8); }
  public float A() { return bb.GetFloat(bb_pos + 12); }

  public static int CreateColour(FlatBufferBuilder builder, float R, float G, float B, float A) {
    builder.Prep(4, 16);
    builder.PutFloat(A);
    builder.PutFloat(B);
    builder.PutFloat(G);
    builder.PutFloat(R);
    return builder.Offset();
  }
};


}
