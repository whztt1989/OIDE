// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class CharEntity : Table {
  public static CharEntity GetRootAsCharEntity(ByteBuffer _bb, int offset) { return (new CharEntity()).__init(_bb.GetInt(offset) + offset, _bb); }
  public CharEntity __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public GameEntity GameEntity() { return GameEntity(new GameEntity()); }
  public GameEntity GameEntity(GameEntity obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public int Face() { int o = __offset(6); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public int Skin() { int o = __offset(8); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public int HairColor() { int o = __offset(10); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public int HairStyle() { int o = __offset(12); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public int Facialfeature() { int o = __offset(14); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public int Facialcolor() { int o = __offset(16); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public Boots Boots() { return Boots(new Boots()); }
  public Boots Boots(Boots obj) { int o = __offset(18); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public Shoulder Shoulder() { return Shoulder(new Shoulder()); }
  public Shoulder Shoulder(Shoulder obj) { int o = __offset(20); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public OneHandWeapon OneHLWeapon() { return OneHLWeapon(new OneHandWeapon()); }
  public OneHandWeapon OneHLWeapon(OneHandWeapon obj) { int o = __offset(22); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public OneHandWeapon OneHRWeapon() { return OneHRWeapon(new OneHandWeapon()); }
  public OneHandWeapon OneHRWeapon(OneHandWeapon obj) { int o = __offset(24); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public TwoHandWeapon TwoHWeapon() { return TwoHWeapon(new TwoHandWeapon()); }
  public TwoHandWeapon TwoHWeapon(TwoHandWeapon obj) { int o = __offset(26); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public AI Ai() { return Ai(new AI()); }
  public AI Ai(AI obj) { int o = __offset(28); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static void StartCharEntity(FlatBufferBuilder builder) { builder.StartObject(13); }
  public static void AddGameEntity(FlatBufferBuilder builder, int gameEntityOffset) { builder.AddOffset(0, gameEntityOffset, 0); }
  public static void AddFace(FlatBufferBuilder builder, int face) { builder.AddInt(1, face, 0); }
  public static void AddSkin(FlatBufferBuilder builder, int skin) { builder.AddInt(2, skin, 0); }
  public static void AddHairColor(FlatBufferBuilder builder, int hairColor) { builder.AddInt(3, hairColor, 0); }
  public static void AddHairStyle(FlatBufferBuilder builder, int hairStyle) { builder.AddInt(4, hairStyle, 0); }
  public static void AddFacialfeature(FlatBufferBuilder builder, int facialfeature) { builder.AddInt(5, facialfeature, 0); }
  public static void AddFacialcolor(FlatBufferBuilder builder, int facialcolor) { builder.AddInt(6, facialcolor, 0); }
  public static void AddBoots(FlatBufferBuilder builder, int bootsOffset) { builder.AddOffset(7, bootsOffset, 0); }
  public static void AddShoulder(FlatBufferBuilder builder, int shoulderOffset) { builder.AddOffset(8, shoulderOffset, 0); }
  public static void AddOneHLWeapon(FlatBufferBuilder builder, int oneHLWeaponOffset) { builder.AddOffset(9, oneHLWeaponOffset, 0); }
  public static void AddOneHRWeapon(FlatBufferBuilder builder, int oneHRWeaponOffset) { builder.AddOffset(10, oneHRWeaponOffset, 0); }
  public static void AddTwoHWeapon(FlatBufferBuilder builder, int twoHWeaponOffset) { builder.AddOffset(11, twoHWeaponOffset, 0); }
  public static void AddAi(FlatBufferBuilder builder, int aiOffset) { builder.AddOffset(12, aiOffset, 0); }
  public static int EndCharEntity(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
