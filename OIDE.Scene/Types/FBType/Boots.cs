// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Boots : Table {
  public static Boots GetRootAsBoots(ByteBuffer _bb, int offset) { return (new Boots()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Boots __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Mesh() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public int MeshTypeIdx() { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }

  public static void StartBoots(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddMesh(FlatBufferBuilder builder, int meshOffset) { builder.AddOffset(0, meshOffset, 0); }
  public static void AddMeshTypeIdx(FlatBufferBuilder builder, int meshTypeIdx) { builder.AddInt(1, meshTypeIdx, 0); }
  public static int EndBoots(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
