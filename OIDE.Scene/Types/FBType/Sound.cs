// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Sound : Table {
  public static Sound GetRootAsSound(ByteBuffer _bb, int offset) { return (new Sound()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Sound __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Name() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public string FileName() { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; }
  public string RessGrp() { int o = __offset(8); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartSound(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddName(FlatBufferBuilder builder, int nameOffset) { builder.AddOffset(0, nameOffset, 0); }
  public static void AddFileName(FlatBufferBuilder builder, int fileNameOffset) { builder.AddOffset(1, fileNameOffset, 0); }
  public static void AddRessGrp(FlatBufferBuilder builder, int ressGrpOffset) { builder.AddOffset(2, ressGrpOffset, 0); }
  public static int EndSound(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
