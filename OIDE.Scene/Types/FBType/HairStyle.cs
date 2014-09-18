// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class HairStyle : Table {
  public static HairStyle GetRootAsHairStyle(ByteBuffer _bb, int offset) { return (new HairStyle()).__init(_bb.GetInt(offset) + offset, _bb); }
  public HairStyle __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Mesh() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartHairStyle(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddMesh(FlatBufferBuilder builder, int meshOffset) { builder.AddOffset(0, meshOffset, 0); }
  public static int EndHairStyle(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
