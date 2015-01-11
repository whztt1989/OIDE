// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Scene : Table {
  public static Scene GetRootAsScene(ByteBuffer _bb) { return (new Scene()).__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb); }
  public Scene __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Colour ColourAmbient() { return ColourAmbient(new Colour()); }
  public Colour ColourAmbient(Colour obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static int CreateScene(FlatBufferBuilder builder,
      int colourAmbient = 0) {
    builder.StartObject(1);
    Scene.AddColourAmbient(builder, colourAmbient);
    return Scene.EndScene(builder);
  }

  public static void StartScene(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddColourAmbient(FlatBufferBuilder builder, int colourAmbientOffset) { builder.AddOffset(0, colourAmbientOffset, 0); }
  public static int EndScene(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
