using System.Numerics;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class SizeComponent : ComponentTemplate, ICloneable<SizeComponent>
{
  public float Width { get; set; }
  public float Height { get; set; }
  public Vector2 Vec2 => new(Width, Height); 
  private bool InitWait;

  public SizeComponent() { InitWait = true; }
  public SizeComponent(Vector2 size) { Width = size.X / 2; Height = size.Y / 2; }
  public SizeComponent(float w, float h) { Width = w; Height = h; }
  public SizeComponent(float wh) { Width = wh; Height = wh; }
  
  public void Set(Vector2 size) { Width = size.X; Height = size.Y; }
  public void Set(float w, float h) { Width = w; Height = h; }
  public void Set(float wh) { Width = wh; Height = wh; }
  public void Add(Vector2 size) { Width += size.X; Height += size.Y; }
  public void Add(float w, float h) { Width += w; Height += h; }
  public void Add(float wh) { Width += wh; Height += wh; }

  public override void Init(Context context)
  {
    if (!InitWait) return;
    Item.Item item = context.Managers.Item.GetByComponent(this);

    ImageComponent? image = item.Component<ImageComponent>();
    if (image is not null)
    {
      Information("Found image component! Using image size.", true);
      Width = image.Resource.Size.X;
      Height = image.Resource.Size.Y;
    }
    else
    {
      Width = Defaults.Size.X;
      Height = Defaults.Size.Y;
    }
  }
  
  public SizeComponent Clone() => (SizeComponent)MemberwiseClone();
}