﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SlackHistoryRetriever
{
    [DataContract]
    public class Config
    {
        [IgnoreDataMember]
        public static Config Instance { get; set; }

        /// <summary>
        /// Slack APIのアクセストークン
        /// </summary>
        [DataMember]
        public string AccessToken { get; set; }

        /// <summary>
        /// パラメータに指定する日付のフォーマット
        /// </summary>
        [DataMember]
        public string DateTimeFormat { get; set; } = "yyyy/MM/dd-HH:mm:ss";

        /// <summary>
        /// 参加済みGroupを保存するか指定します。
        /// </summary>
        /// <remarks>いわゆるプライベートチャンネル</remarks>
        [DataMember]
        public bool IncludeGroup { get; set; } = false;

        /// <summary>
        /// Instant Messageを保存するか指定します (参加済みIMのみ)
        /// </summary>
        /// <remarks>いわゆるダイレクトメッセージ。複数人IMは別です</remarks>
        [DataMember]
        public bool IncludeIM { get; set; } = false;

        /// <summary>
        /// メッセージ取得間隔(最短60秒)
        /// </summary>
        [DataMember]
        public int GetHistoryInterval { get; set; } = 60;
    }
}
