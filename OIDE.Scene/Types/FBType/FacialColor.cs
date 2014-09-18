// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class FacialColor : Table {
  public static FacialColor GetRootAsFacialColor(ByteBuffer _bb, int offset) { return (new FacialColor()).__init(_bb.GetInt(offset) + offset, _bb); }
  public FacialColor __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string TextName() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartFacialColor(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddTextName(FlatBufferBuilder builder, int textNameOffset) { builder.AddOffset(0, textNameOffset, 0); }
  public static int EndFacialColor(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
