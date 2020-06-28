using Microsoft.Xna.Framework;
using System;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Ghost
{
    [ApiVersion(2, 1)]
    public class Ghost : TerrariaPlugin
    {
        public override string Name => "Ghost";

        public override string Author => "SirApples (Mobile port by Vednix)";

        public override string Description => "A plugin that allows admins to become completely invisible to players.";

        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("ghost.ghost", OnGhost, "ghost", "vanish"));
        }
        public static int NewProjectile(Vector2 position, Vector2 velocity, int Type, int Damage, float KnockBack, int Owner = 16, float ai0 = 0.0f, float ai1 = 0.0f)
        {
            return Projectile.NewProjectile(position.X, position.Y, velocity.X, velocity.Y, Type, Damage, KnockBack, Owner, ai0, ai1);
        }
        void OnGhost(CommandArgs args)
        {
            //int i = NewProjectile(args.Player.LastNetPosition, Vector2.Zero, 170, 0, 0);
            //Main.projectile[i].timeLeft = 0;
            //NetMessage.SendData(27, -1, -1, "", i);
            args.TPlayer.active = !args.TPlayer.active;
            NetMessage.SendData(14, -1, args.Player.Index, "", args.Player.Index, args.TPlayer.active.GetHashCode());
            if (args.TPlayer.active)
            {
                NetMessage.SendData(4, -1, args.Player.Index, "", args.Player.Index);
                NetMessage.SendData(13, -1, args.Player.Index, "", args.Player.Index);
            }
            args.Player.SendSuccessMessage($"{(args.TPlayer.active ? "Dis" : "En")}abled Vanish.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }

        public Ghost(Main game)
            : base(game)
        {
            Order = 10;
        }
    }
}
