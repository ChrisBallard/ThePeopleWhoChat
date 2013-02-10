namespace ThePeopleWhoChat.Service

    open System.ServiceModel
    open System.ServiceModel.Web

    [<ServiceContract>]
    type ChatService() =

        [<WebGet(UriTemplate = "token?username={username}&password={password}", ResponseFormat = WebMessageFormat.Json)>]
        [<OperationContract>]
        member this.Login(username:string, password:string) =
            "123456"

        [<WebInvoke(Method = "DELETE", UriTemplate = "token/{token}")>]
        [<OperationContract>]
        member this.Logout(token:string) =
            ()

