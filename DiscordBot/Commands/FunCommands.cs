using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordBot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        
        [Command("ping")]
        [Description("Bot has a reply!")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("- You suck at this game, it's not ping's fault.ms").ConfigureAwait(false);
        }

        [Command("add")]
        [Description("Adding two numbers together")]
        public async Task Add(CommandContext ctx,
            [Description("First Number")] int numberOne,
            [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel.SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("minus")]
        [Description("Subtracting two numbers")]
        public async Task Minus(CommandContext ctx,
            [Description("First Number")] int numberOne,
            [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel.SendMessageAsync((numberOne - numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("multiply")]
        [Description("Multiplying two numbers")]
        public async Task Multiply(CommandContext ctx,
            [Description("First Number")] int numberOne,
            [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel.SendMessageAsync((numberOne * numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("divide")]
        [Description("Dividing two numbers")]
        public async Task Divide(CommandContext ctx,
            [Description("First Number")] double numberOne,
            [Description("Second Number")] double numberTwo)
        {
            await ctx.Channel.SendMessageAsync((numberOne / numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("roll")]
        [Description("Rolls a random number from 0 - 100")]
        public async Task Roll(CommandContext ctx)
        {
            Random random = new Random();
            int randomInt = random.Next(0, 100);

            List<String> funFacts = new List<string>() {
                " Fun fact, that's the number of eyeballs that a martian has.",
                " Fun fact, that's the number of brain cells I have left :)",
                " That's what I got on my SATs, try beating that :)",
                " Hey! That's how many toes I originally had when I came out the womb",
                " That's how many girls that've taken my heart and crushed it to little pieces and WAHHHHHHH",
                " I got my head dunked in the toilet that exact number of times in high school!",
                " I have an unusual amount of knees, that exact number actually.",
                " You know how many firemen needed to cross the road? That many. Wait that wasn't funny :(",
                " Problems but a ***** ain't one! Man i hope that number was a 99, I'm pre-programmed dude",
                " I can count the number of times I’ve been to Chernobyl on one hand. That many!"
            };

            Random random1 = new Random();
            int randomInt1 = random1.Next(0, 9);
            String randomPhrase = funFacts[randomInt1];

            if (randomInt == 100)
            {
                await ctx.Channel.SendMessageAsync("You got 100! Heres a cookie" + " :cookie:")
                    .ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("You got " + randomInt + "." + randomPhrase)
                    .ConfigureAwait(false);
            }
        }
    }
}
