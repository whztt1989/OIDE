// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class SpawnPoint : Table {
  public static SpawnPoint GetRootAsSpawnPoint(ByteBuffer _bb, int offset) { return (new SpawnPoint()).__init(_bb.GetInt(offset) + offset, _bb); }
  public SpawnPoint __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int SPGroup() { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }

  public static void StartSpawnPoint(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddSPGroup(FlatBufferBuilder builder, int SPGroup) { builder.AddInt(0, SPGroup, 0); }
  public static int EndSpawnPoint(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
