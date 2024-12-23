using UnityEngine;
using Exiled.API.Extensions;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using SCP1162.API;
using Random = UnityEngine.Random;

namespace SCP1162;

public class EventHandlers(Plugin plugin) {
  public void OnItemDropped(DroppingItemEventArgs ev) {
    if (!ev.IsAllowed) return;
    if (!(Vector3.Distance(ev.Player.Position, RoleTypeId.Scp173.GetRandomSpawnLocation().Position) <= 8.2f)) return;
    if (plugin.Config.UseHints)
     ev.Player.ShowHint(plugin.Config.ItemDropMessage, plugin.Config.ItemDropMessageDuration);
    else
      ev.Player.Broadcast(plugin.Config.ItemDropMessageDuration, plugin.Config.ItemDropMessage, Broadcast.BroadcastFlags.Normal, true);
    ev.IsAllowed = false;
    var oldItem = ev.Item.Base.ItemTypeId;
    ev.Player.RemoveItem(ev.Item);
    var newItemType = plugin.Config.Chances[Random.Range(0, plugin.Config.Chances.Count)];
    var eventArgs = new UsingScp1162EventArgs(ev.Player, newItemType, oldItem);
    Scp1162Event.OnUsingScp1162(eventArgs);
    var newItem = Item.Create(eventArgs.ItemAfter);
    ev.Player.AddItem(newItem);
    ev.Player.DropItem(newItem);
  }
}