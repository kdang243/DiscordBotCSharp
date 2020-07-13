using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordBot.Commands
{
    public class ReminderCommands : BaseCommandModule
    {
        public List<String> Reminders = new List<string>();
        public Boolean Confirmation = false;



        [Command("ViewReminders")]
        [Description("View your outstanding reminders!")]
        public async Task ViewReminders(CommandContext ctx)
        {
            string organisedReminders = OrganiseReminders();
            await ctx.Channel.SendMessageAsync(organisedReminders)
                .ConfigureAwait(false);
        }



        [Command("AddReminder")]
        [Description("Add a new reminder to your list!")]
        public async Task AddReminder(CommandContext ctx,[RemainingText] string reminder)
        {
            Reminders.Add(reminder);
            string organisedReminders = OrganiseReminders();
            await ctx.Channel.SendMessageAsync(organisedReminders)
                .ConfigureAwait(false);
        }



        [Command("RemoveReminder")]
        [Description("Remove a reminder from your list!")]
        public async Task RemoveReminder(CommandContext ctx,[RemainingText] string reminder)
        {
            if (Reminders.Contains(reminder))
            {
                Reminders.Remove(reminder);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Sorry, I couldn't find that reminder, please try again")
                .ConfigureAwait(false);
            }

            string organisedReminders = OrganiseReminders();
            await ctx.Channel.SendMessageAsync(organisedReminders)
                .ConfigureAwait(false);
        }

        [Command("ClearReminder")]
        [Description("Clear your current list of reminders")]
        public async Task ClearReminder(CommandContext ctx)
        {
            if (!Confirmation)
            {
                await ctx.Channel.SendMessageAsync("Are you sure you want to clear your reminders? Do ?clearreminder again to confirm, if not, please do ?resetclear to reset safety switch.")
                .ConfigureAwait(false);
                Confirmation = true;
            }
            else
            {
                Reminders.Clear();
                await ctx.Channel.SendMessageAsync("Your reminders have been cleared.")
                .ConfigureAwait(false);
                string organisedReminders = OrganiseReminders();
                await ctx.Channel.SendMessageAsync(organisedReminders)
                    .ConfigureAwait(false);
            }
        }

        [Command("ResetClear")]
        [Description("Reset safety switch for ?clearreminder")]
        public async Task ResetClear(CommandContext ctx)
        {
            if (Confirmation)
            {
                Confirmation = false;
                await ctx.Channel.SendMessageAsync("Safety switch has been reset.")
                    .ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Safety switch was already on!")
                    .ConfigureAwait(false);
            }
        }
        



        public string OrganiseReminders()
        {
            int i = 1;
            string answer = "";

            if (Reminders.Count > 0)
            {
                foreach (var r in Reminders)
                {
                    if (answer == "" || answer == "Your reminders are empty, use ?addreminder to add a new reminder!")
                    {
                        answer = i + " : " + r;
                    }
                    else
                    {
                        answer = answer + "\n" + i + " : " + r;
                    }
                    i++;
                }
            }
            else
            {
                answer = "Your reminders are empty, use ?addreminder to add a new reminder!";
            }
            

            return answer;
        }
    }

}
