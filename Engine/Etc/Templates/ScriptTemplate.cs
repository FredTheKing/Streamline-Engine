using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Etc.Templates;

public class ScriptTemplate : IScript
{
  public Item.Item Parent { get; set; }
  
  public virtual void Init(Context context) { }
  public virtual void Enter(Context context) { }
  public virtual void Leave(Context context) { }
  public virtual void Update(Context context) { }
  public virtual void Draw(Context context) { }
}