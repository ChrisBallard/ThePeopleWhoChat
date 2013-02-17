namespace ThePeopleWhoChat.Service

    open System
    open System.Runtime.Serialization

    type Consts = 
        static member DbUrlSettingKey = "ChatDbUrl"
        static member TokenHeaderName = "Session-Token"
        static member ErrorHeaderName = "Error-Message"
        static member CacheHeaderName = "Cache-Control"
        static member CacheNoCache = "no-cache"
        static member CacheOneMinute = "public, max-age=60"

    [<DataContract>]
    type Identifier = {
        [<DataMember>] mutable id: string
        }

    [<DataContract>]
    type User = {
        [<DataMember>] mutable name: string;
        [<DataMember>] mutable password: string;
        [<DataMember>] mutable fullName: string;
        [<DataMember>] mutable isAdmin: bool
        }

    [<DataContract>]
    type Room = {
        [<DataMember>] mutable name: string;
        [<DataMember>] mutable description: string
        }

    [<DataContract>]
    type Message = {
        [<DataMember>] mutable roomId: string;
        [<DataMember>] mutable timestamp: DateTime;
        [<DataMember>] mutable userId: string;
        [<DataMember>] mutable rawMessage: string;
        [<DataMember>] mutable html: string
        }

    [<DataContract>]
    type LoginDetails = {
        [<DataMember>] mutable username: string;
        [<DataMember>] mutable password: string
        }

    [<DataContract>]
    type LoginSession = {
        [<DataMember>] mutable token: string;
        [<DataMember>] mutable userId: string
        [<DataMember>] mutable user: User;
        [<DataMember>] mutable roomId: string option
        [<DataMember>] mutable lastTouch: DateTime
        }