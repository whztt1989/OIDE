// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class StaticEntity : Table {
  public static StaticEntity GetRootAsStaticEntity(ByteBuffer _bb, int offset) { return (new StaticEntity()).__init(_bb.GetInt(offset) + offset, _bb); }
  public StaticEntity __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public GameEntity GameEntity() { return GameEntity(new GameEntity()); }
  public GameEntity GameEntity(GameEntity obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public int Group() { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }

  public static void StartStaticEntity(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddGameEntity(FlatBufferBuilder builder, int gameEntityOffset) { builder.AddOffset(0, gameEntityOffset, 0); }
  public static void AddGroup(FlatBufferBuilder builder, int group) { builder.AddInt(1, group, 0); }
  public static int EndStaticEntity(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
