// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Shoulder : Table {
  public static Shoulder GetRootAsShoulder(ByteBuffer _bb, int offset) { return (new Shoulder()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Shoulder __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Mesh() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public int MeshTypeIdx() { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }

  public static void StartShoulder(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddMesh(FlatBufferBuilder builder, int meshOffset) { builder.AddOffset(0, meshOffset, 0); }
  public static void AddMeshTypeIdx(FlatBufferBuilder builder, int meshTypeIdx) { builder.AddInt(1, meshTypeIdx, 0); }
  public static int EndShoulder(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
