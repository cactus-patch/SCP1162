using Exiled.API.Features;

namespace SCP1162;

public class Plugin : Plugin<Config> {
  public override string Name => "SCP1162";
  public override string Author => "xRoier, furry";
  public override Version Version => new(9, 1, 0);
  public override Version RequiredExiledVersion => new(9, 0, 0);
  private EventHandler? _eventHandler;
  
  public override void OnEnabled() {
    _eventHandler = new EventHandler(this);
    
    base.OnEnabled();
  }

  public override void OnDisabled() {
    _eventHandler = null;
    
    base.OnDisabled();
  }
}