// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class CharEntity : Table {
  public static CharEntity GetRootAsCharEntity(ByteBuffer _bb) { return GetRootAsCharEntity(_bb, new CharEntity()); }
  public static CharEntity GetRootAsCharEntity(ByteBuffer _bb, CharEntity obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public CharEntity __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public EntityBase Entitybase() { return Entitybase(new EntityBase()); }
  public EntityBase Entitybase(EntityBase obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public AI Ai() { return Ai(new AI()); }
  public AI Ai(AI obj) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static int CreateCharEntity(FlatBufferBuilder builder,
      int entitybase = 0,
      int ai = 0) {
    builder.StartObject(2);
    CharEntity.AddAi(builder, ai);
    CharEntity.AddEntitybase(builder, entitybase);
    return CharEntity.EndCharEntity(builder);
  }

  public static void StartCharEntity(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddEntitybase(FlatBufferBuilder builder, int entitybaseOffset) { builder.AddOffset(0, entitybaseOffset, 0); }
  public static void AddAi(FlatBufferBuilder builder, int aiOffset) { builder.AddOffset(1, aiOffset, 0); }
  public static int EndCharEntity(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
