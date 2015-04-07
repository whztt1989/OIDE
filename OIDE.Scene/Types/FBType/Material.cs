// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Material : Table {
  public static Material GetRootAsMaterial(ByteBuffer _bb) { return GetRootAsMaterial(_bb, new Material()); }
  public static Material GetRootAsMaterial(ByteBuffer _bb, Material obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public Material __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Name() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public string RessGrp() { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; }

  public static int CreateMaterial(FlatBufferBuilder builder,
      int Name = 0,
      int RessGrp = 0) {
    builder.StartObject(2);
    Material.AddRessGrp(builder, RessGrp);
    Material.AddName(builder, Name);
    return Material.EndMaterial(builder);
  }

  public static void StartMaterial(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddName(FlatBufferBuilder builder, int NameOffset) { builder.AddOffset(0, NameOffset, 0); }
  public static void AddRessGrp(FlatBufferBuilder builder, int RessGrpOffset) { builder.AddOffset(1, RessGrpOffset, 0); }
  public static int EndMaterial(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
