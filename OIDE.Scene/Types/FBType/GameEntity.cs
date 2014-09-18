// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class GameEntity : Table {
  public static GameEntity GetRootAsGameEntity(ByteBuffer _bb, int offset) { return (new GameEntity()).__init(_bb.GetInt(offset) + offset, _bb); }
  public GameEntity __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Mesh Meshes(int j) { return Meshes(new Mesh(), j); }
  public Mesh Meshes(Mesh obj, int j) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int MeshesLength() { int o = __offset(4); return o != 0 ? __vector_len(o) : 0; }
  public Sound Sounds(int j) { return Sounds(new Sound(), j); }
  public Sound Sounds(Sound obj, int j) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int SoundsLength() { int o = __offset(6); return o != 0 ? __vector_len(o) : 0; }
  public Material Materials(int j) { return Materials(new Material(), j); }
  public Material Materials(Material obj, int j) { int o = __offset(8); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int MaterialsLength() { int o = __offset(8); return o != 0 ? __vector_len(o) : 0; }
  public PhysicsObject Physics(int j) { return Physics(new PhysicsObject(), j); }
  public PhysicsObject Physics(PhysicsObject obj, int j) { int o = __offset(10); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int PhysicsLength() { int o = __offset(10); return o != 0 ? __vector_len(o) : 0; }
  public byte Type() { int o = __offset(12); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; }
  public string Boneparent() { int o = __offset(14); return o != 0 ? __string(o + bb_pos) : null; }
  public int Mode() { int o = __offset(16); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; }
  public byte CastShadows() { int o = __offset(18); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; }
  public Debug Debug() { return Debug(new Debug()); }
  public Debug Debug(Debug obj) { int o = __offset(20); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public string AnimationTree() { int o = __offset(22); return o != 0 ? __string(o + bb_pos) : null; }
  public string AnimationInfo() { int o = __offset(24); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartGameEntity(FlatBufferBuilder builder) { builder.StartObject(11); }
  public static void AddMeshes(FlatBufferBuilder builder, int meshesOffset) { builder.AddOffset(0, meshesOffset, 0); }
  public static void StartMeshesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddSounds(FlatBufferBuilder builder, int soundsOffset) { builder.AddOffset(1, soundsOffset, 0); }
  public static void StartSoundsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddMaterials(FlatBufferBuilder builder, int materialsOffset) { builder.AddOffset(2, materialsOffset, 0); }
  public static void StartMaterialsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddPhysics(FlatBufferBuilder builder, int physicsOffset) { builder.AddOffset(3, physicsOffset, 0); }
  public static void StartPhysicsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddType(FlatBufferBuilder builder, byte Type) { builder.AddByte(4, Type, 0); }
  public static void AddBoneparent(FlatBufferBuilder builder, int boneparentOffset) { builder.AddOffset(5, boneparentOffset, 0); }
  public static void AddMode(FlatBufferBuilder builder, int mode) { builder.AddInt(6, mode, 0); }
  public static void AddCastShadows(FlatBufferBuilder builder, byte castShadows) { builder.AddByte(7, castShadows, 0); }
  public static void AddDebug(FlatBufferBuilder builder, int debugOffset) { builder.AddOffset(8, debugOffset, 0); }
  public static void AddAnimationTree(FlatBufferBuilder builder, int animationTreeOffset) { builder.AddOffset(9, animationTreeOffset, 0); }
  public static void AddAnimationInfo(FlatBufferBuilder builder, int animationInfoOffset) { builder.AddOffset(10, animationInfoOffset, 0); }
  public static int EndGameEntity(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
