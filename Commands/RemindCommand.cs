﻿using Boyfriend.Data;

namespace Boyfriend.Commands;

public sealed class RemindCommand : ICommand {
    public string[] Aliases { get; } = { "remind", "reminder", "remindme", "напомни", "напомнить", "напоминание" };

    public Task RunAsync(CommandProcessor cmd, string[] args, string[] cleanArgs) {
        // TODO: actually make this good
        var remindIn = CommandProcessor.GetTimeSpan(args, 0);
        var reminderText = cmd.GetRemaining(cleanArgs, 1, "ReminderText");
        if (reminderText is not null)
            GuildData.Get(cmd.Context.Guild).MemberData[cmd.Context.User.Id].Reminders.Add(new Reminder {
                RemindAt = DateTimeOffset.Now.Add(remindIn),
                ReminderText = reminderText,
                ReminderChannel = cmd.Context.Channel.Id
            });

        cmd.ConfigWriteScheduled = true;

        return Task.CompletedTask;
    }
}