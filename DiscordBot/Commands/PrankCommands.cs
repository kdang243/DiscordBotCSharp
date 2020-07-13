using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DiscordBot.Commands
{
    public class PrankCommands : BaseCommandModule
    {
        [Command("annoy")]
        [Description("Tag a user to annoy them!")]
        public async Task Annoy(CommandContext ctx,
            [Description("User to annoy")] String victimName)
        {
            var membersDictionary = ctx.Guild.Members;

            var discordMembers = membersDictionary.Values;

            String mentionString = "";

            foreach (var d in discordMembers)
            {
                var tempName = d.DisplayName;

                if (victimName == tempName)
                {
                    mentionString = d.Mention;
                }
            }

            if (mentionString != "")
            {
                for (int i = 0; i < 6; i++)
                {
                    await ctx.Channel.SendMessageAsync(mentionString + " Am I annoying?")
                    .ConfigureAwait(false);
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Failed to find a user with that name, remember to only write the name of the user.")
                    .ConfigureAwait(false);
            }

            
        }


        
    }
}
