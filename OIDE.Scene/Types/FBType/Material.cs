// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Material : Table {
  public static Material GetRootAsMaterial(ByteBuffer _bb, int offset) { return (new Material()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Material __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Name() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public string RessGrp() { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartMaterial(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddName(FlatBufferBuilder builder, int NameOffset) { builder.AddOffset(0, NameOffset, 0); }
  public static void AddRessGrp(FlatBufferBuilder builder, int RessGrpOffset) { builder.AddOffset(1, RessGrpOffset, 0); }
  public static int EndMaterial(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
