// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Skin : Table {
  public static Skin GetRootAsSkin(ByteBuffer _bb, int offset) { return (new Skin()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Skin __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string TextName() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public string Mesh() { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartSkin(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddTextName(FlatBufferBuilder builder, int textNameOffset) { builder.AddOffset(0, textNameOffset, 0); }
  public static void AddMesh(FlatBufferBuilder builder, int meshOffset) { builder.AddOffset(1, meshOffset, 0); }
  public static int EndSkin(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
