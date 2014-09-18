// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class RaceGender : Table {
  public static RaceGender GetRootAsRaceGender(ByteBuffer _bb, int offset) { return (new RaceGender()).__init(_bb.GetInt(offset) + offset, _bb); }
  public RaceGender __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Race Race() { return Race(new Race()); }
  public Race Race(Race obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public PhysicsObject BodyPhys() { return BodyPhys(new PhysicsObject()); }
  public PhysicsObject BodyPhys(PhysicsObject obj) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public PhysicsObject FootLPhys() { return FootLPhys(new PhysicsObject()); }
  public PhysicsObject FootLPhys(PhysicsObject obj) { int o = __offset(8); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public PhysicsObject FootRPhys() { return FootRPhys(new PhysicsObject()); }
  public PhysicsObject FootRPhys(PhysicsObject obj) { int o = __offset(10); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public PhysicsObject HandLPhys() { return HandLPhys(new PhysicsObject()); }
  public PhysicsObject HandLPhys(PhysicsObject obj) { int o = __offset(12); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public PhysicsObject HandRPhys() { return HandRPhys(new PhysicsObject()); }
  public PhysicsObject HandRPhys(PhysicsObject obj) { int o = __offset(14); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public float TurnSpeed() { int o = __offset(16); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public byte Gender() { int o = __offset(18); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; }
  public Sound Sounds(int j) { return Sounds(new Sound(), j); }
  public Sound Sounds(Sound obj, int j) { int o = __offset(20); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int SoundsLength() { int o = __offset(20); return o != 0 ? __vector_len(o) : 0; }
  public Face Faces(int j) { return Faces(new Face(), j); }
  public Face Faces(Face obj, int j) { int o = __offset(22); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int FacesLength() { int o = __offset(22); return o != 0 ? __vector_len(o) : 0; }
  public Skin Skins(int j) { return Skins(new Skin(), j); }
  public Skin Skins(Skin obj, int j) { int o = __offset(24); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int SkinsLength() { int o = __offset(24); return o != 0 ? __vector_len(o) : 0; }
  public HairColor HairColors(int j) { return HairColors(new HairColor(), j); }
  public HairColor HairColors(HairColor obj, int j) { int o = __offset(26); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int HairColorsLength() { int o = __offset(26); return o != 0 ? __vector_len(o) : 0; }
  public HairStyle HairStyles(int j) { return HairStyles(new HairStyle(), j); }
  public HairStyle HairStyles(HairStyle obj, int j) { int o = __offset(28); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int HairStylesLength() { int o = __offset(28); return o != 0 ? __vector_len(o) : 0; }
  public FacialFeature Facials(int j) { return Facials(new FacialFeature(), j); }
  public FacialFeature Facials(FacialFeature obj, int j) { int o = __offset(30); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int FacialsLength() { int o = __offset(30); return o != 0 ? __vector_len(o) : 0; }
  public FacialColor Facialcolors(int j) { return Facialcolors(new FacialColor(), j); }
  public FacialColor Facialcolors(FacialColor obj, int j) { int o = __offset(32); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int FacialcolorsLength() { int o = __offset(32); return o != 0 ? __vector_len(o) : 0; }
  public Shoulder Shoulder(int j) { return Shoulder(new Shoulder(), j); }
  public Shoulder Shoulder(Shoulder obj, int j) { int o = __offset(34); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int ShoulderLength() { int o = __offset(34); return o != 0 ? __vector_len(o) : 0; }
  public Boots Boots(int j) { return Boots(new Boots(), j); }
  public Boots Boots(Boots obj, int j) { int o = __offset(36); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int BootsLength() { int o = __offset(36); return o != 0 ? __vector_len(o) : 0; }
  public SpellEntity Spells(int j) { return Spells(new SpellEntity(), j); }
  public SpellEntity Spells(SpellEntity obj, int j) { int o = __offset(38); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int SpellsLength() { int o = __offset(38); return o != 0 ? __vector_len(o) : 0; }
  public string AnimationTree() { int o = __offset(40); return o != 0 ? __string(o + bb_pos) : null; }
  public string AnimationInfo() { int o = __offset(42); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartRaceGender(FlatBufferBuilder builder) { builder.StartObject(20); }
  public static void AddRace(FlatBufferBuilder builder, int raceOffset) { builder.AddOffset(0, raceOffset, 0); }
  public static void AddBodyPhys(FlatBufferBuilder builder, int bodyPhysOffset) { builder.AddOffset(1, bodyPhysOffset, 0); }
  public static void AddFootLPhys(FlatBufferBuilder builder, int footLPhysOffset) { builder.AddOffset(2, footLPhysOffset, 0); }
  public static void AddFootRPhys(FlatBufferBuilder builder, int footRPhysOffset) { builder.AddOffset(3, footRPhysOffset, 0); }
  public static void AddHandLPhys(FlatBufferBuilder builder, int HandLPhysOffset) { builder.AddOffset(4, HandLPhysOffset, 0); }
  public static void AddHandRPhys(FlatBufferBuilder builder, int HandRPhysOffset) { builder.AddOffset(5, HandRPhysOffset, 0); }
  public static void AddTurnSpeed(FlatBufferBuilder builder, float turnSpeed) { builder.AddFloat(6, turnSpeed, 0); }
  public static void AddGender(FlatBufferBuilder builder, byte gender) { builder.AddByte(7, gender, 0); }
  public static void AddSounds(FlatBufferBuilder builder, int soundsOffset) { builder.AddOffset(8, soundsOffset, 0); }
  public static void StartSoundsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddFaces(FlatBufferBuilder builder, int facesOffset) { builder.AddOffset(9, facesOffset, 0); }
  public static void StartFacesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddSkins(FlatBufferBuilder builder, int skinsOffset) { builder.AddOffset(10, skinsOffset, 0); }
  public static void StartSkinsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddHairColors(FlatBufferBuilder builder, int hairColorsOffset) { builder.AddOffset(11, hairColorsOffset, 0); }
  public static void StartHairColorsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddHairStyles(FlatBufferBuilder builder, int hairStylesOffset) { builder.AddOffset(12, hairStylesOffset, 0); }
  public static void StartHairStylesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddFacials(FlatBufferBuilder builder, int facialsOffset) { builder.AddOffset(13, facialsOffset, 0); }
  public static void StartFacialsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddFacialcolors(FlatBufferBuilder builder, int facialcolorsOffset) { builder.AddOffset(14, facialcolorsOffset, 0); }
  public static void StartFacialcolorsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddShoulder(FlatBufferBuilder builder, int shoulderOffset) { builder.AddOffset(15, shoulderOffset, 0); }
  public static void StartShoulderVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddBoots(FlatBufferBuilder builder, int bootsOffset) { builder.AddOffset(16, bootsOffset, 0); }
  public static void StartBootsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddSpells(FlatBufferBuilder builder, int spellsOffset) { builder.AddOffset(17, spellsOffset, 0); }
  public static void StartSpellsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddAnimationTree(FlatBufferBuilder builder, int animationTreeOffset) { builder.AddOffset(18, animationTreeOffset, 0); }
  public static void AddAnimationInfo(FlatBufferBuilder builder, int animationInfoOffset) { builder.AddOffset(19, animationInfoOffset, 0); }
  public static int EndRaceGender(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
