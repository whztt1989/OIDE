// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class StaticEntity : Table {
  public static StaticEntity GetRootAsStaticEntity(ByteBuffer _bb) { return (new StaticEntity()).__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb); }
  public StaticEntity __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public EntityBase Entitybase() { return Entitybase(new EntityBase()); }
  public EntityBase Entitybase(EntityBase obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static int CreateStaticEntity(FlatBufferBuilder builder,
      int entitybase = 0) {
    builder.StartObject(1);
    StaticEntity.AddEntitybase(builder, entitybase);
    return StaticEntity.EndStaticEntity(builder);
  }

  public static void StartStaticEntity(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddEntitybase(FlatBufferBuilder builder, int entitybaseOffset) { builder.AddOffset(0, entitybaseOffset, 0); }
  public static int EndStaticEntity(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
