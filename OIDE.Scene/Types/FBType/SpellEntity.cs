// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class SpellEntity : Table {
  public static SpellEntity GetRootAsSpellEntity(ByteBuffer _bb) { return GetRootAsSpellEntity(_bb, new SpellEntity()); }
  public static SpellEntity GetRootAsSpellEntity(ByteBuffer _bb, SpellEntity obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public SpellEntity __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public EntityBase Entitybase() { return Entitybase(new EntityBase()); }
  public EntityBase Entitybase(EntityBase obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static int CreateSpellEntity(FlatBufferBuilder builder,
      int entitybase = 0) {
    builder.StartObject(1);
    SpellEntity.AddEntitybase(builder, entitybase);
    return SpellEntity.EndSpellEntity(builder);
  }

  public static void StartSpellEntity(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddEntitybase(FlatBufferBuilder builder, int entitybaseOffset) { builder.AddOffset(0, entitybaseOffset, 0); }
  public static int EndSpellEntity(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
