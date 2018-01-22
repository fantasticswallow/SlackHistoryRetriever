using System;
using System.Collections.Generic;
using System.Text;
using SlackHistoryRetriever.Slack;
using System.Threading.Tasks;
using artfulplace.Whirlwind.Reactive;
using artfulplace.Citrine;
using artfulplace.Whirlwind.OAuth;

namespace SlackHistoryRetriever
{
    internal class SlackInitializer
    {
        /// <summary>
        /// SlackClientを初期化します。
        /// </summary>
        internal static void InitializeClient()
        {
            var clientCredential = new OAuthCredential();
            var token = new SlackCredential(Config.Instance.AccessToken, "");

            SlackInfo.Client = new SlackObservableClient(clientCredential, token);

        }

        /// <summary>
        /// メッセージ以外の必要情報を取得します。
        /// </summary>
        /// <returns>awaitable Task</returns>
        internal static async Task InitializeData()
        {
            var client = SlackInfo.Client;
            var ures = await client.UserList();
            if (ures.IsSuccess)
            {
                foreach (var u in ures.Users.Members)
                {
                    SlackInfo.UserData.Add(u.Id, u);
                }
            }

            var res = await client.ChannelList();
            if (res.IsSuccess)
            {
                var ch = res.Channels.Channels;
                foreach (var c in ch)
                {
                    AddObserver(client, c.Id);

                    var channelObj = new SlackChannelInfo();
                    channelObj.ChannelId = c.Id;
                    channelObj.Name = c.Name;
                    channelObj.IsArchived = c.IsArchived;
                    SlackInfo.ChannelInfo.Add(c.Id, channelObj);
                }
            }
            var res2 = await client.GroupsList();
            if (res2.IsSuccess)
            {
                var ch = (res2).Groups.Groups;
                foreach (var c in ch)
                {
                    AddObserver(client, c.Id);
                    var channelObj = new SlackChannelInfo();
                    channelObj.ChannelId = c.Id;
                    channelObj.Name = c.Name;
                    channelObj.IsArchived = c.IsArchived;
                    // channelObj.IsNotMember = !c.IsMember;
                    SlackInfo.ChannelInfo.Add(c.Id, channelObj);
                }
            }

            var res3 = await client.ImList();
            if (res3.IsSuccess)
            {
                foreach (var c in ((artfulplace.Whirlwind.DataModel.ImList)res3.Response).Ims)
                {
                    var userData = SlackInfo.UserData[c.User];
                    AddObserver(client, c.Id);
                    var channelObj = new SlackChannelInfo();
                    channelObj.ChannelId = c.Id;
                    channelObj.Name = userData.Name;
                    SlackInfo.ChannelInfo.Add(c.Id, channelObj);
                }
            }
        }


        private static void AddObserver(SlackObservableClient client, string channelId)
        {
            client.Observable.Subscribe(channelId, x =>
            {
                //var s = await StatusCreater.Create(x, ownerId);
                //var channelInfo = Slack.SlackInfo.ChannelInfo[channelId];
                //if (!channelInfo.StatusReciever.Contains(s.TimeStamp))
                //{
                //    SlackInfo.MessageCollection.LockAdd(s.TimeStamp, s, _lockObj);
                //    channelInfo.StatusReciever.Add(s.TimeStamp);
                //    channelInfo.Statuses.AddItem(s);
                //    channelInfo.UpdateUnreadCount();
                //}
            }, (message, isCritical) =>
            {
                Console.WriteLine($"Get message was failed : {message}");
            });
        }

    }
}
