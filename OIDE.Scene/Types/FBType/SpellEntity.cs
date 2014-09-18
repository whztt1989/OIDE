// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class SpellEntity : Table {
  public static SpellEntity GetRootAsSpellEntity(ByteBuffer _bb, int offset) { return (new SpellEntity()).__init(_bb.GetInt(offset) + offset, _bb); }
  public SpellEntity __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public GameEntity GameEntity() { return GameEntity(new GameEntity()); }
  public GameEntity GameEntity(GameEntity obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static void StartSpellEntity(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddGameEntity(FlatBufferBuilder builder, int gameEntityOffset) { builder.AddOffset(0, gameEntityOffset, 0); }
  public static int EndSpellEntity(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
