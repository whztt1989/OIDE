// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class SpawnPoint : Table {
  public static SpawnPoint GetRootAsSpawnPoint(ByteBuffer _bb) { return GetRootAsSpawnPoint(_bb, new SpawnPoint()); }
  public static SpawnPoint GetRootAsSpawnPoint(ByteBuffer _bb, SpawnPoint obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public SpawnPoint __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public uint SPGroup() { int o = __offset(4); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }

  public static int CreateSpawnPoint(FlatBufferBuilder builder,
      uint SPGroup = 0) {
    builder.StartObject(1);
    SpawnPoint.AddSPGroup(builder, SPGroup);
    return SpawnPoint.EndSpawnPoint(builder);
  }

  public static void StartSpawnPoint(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddSPGroup(FlatBufferBuilder builder, uint SPGroup) { builder.AddUint(0, SPGroup, 0); }
  public static int EndSpawnPoint(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
