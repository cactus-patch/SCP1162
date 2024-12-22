namespace SCP1162.API;

using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

public class UsingScp1162EventArgs(Player player, ItemType itemAfter, ItemType itemBefore) : IExiledEvent {
  public Player Player => player;
  public ItemType ItemAfter => itemAfter;
  public ItemType ItemBefore => itemBefore;
}