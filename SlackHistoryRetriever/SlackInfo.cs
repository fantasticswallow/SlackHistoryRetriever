using System;
using System.Collections.Generic;
using System.Text;
using artfulplace.Citrine;
using artfulplace.Whirlwind.DataModel;
using artfulplace.Whirlwind.Reactive;
using artfulplace.Whirlwind.OAuth;

namespace SlackHistoryRetriever.Slack
{
    internal class SlackInfo
    {
        internal static Dictionary<string, Member> UserData { get; private set; } = new Dictionary<string, Member>();

        internal static Dictionary<string, SlackChannelInfo> ChannelInfo { get; } = new Dictionary<string, SlackChannelInfo>();

        internal static SlackObservableClient Client { get; set; }
    }

    public class SlackChannelInfo
    {
        // internal Channel RawData { get; set; }
        internal string Name { get; set; }

        public bool IsArchived { get; set; }
        
        internal string ChannelId { get; set; }
    }
}
