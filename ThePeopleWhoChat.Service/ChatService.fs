namespace ThePeopleWhoChat.Service

    open System.ServiceModel
    open System.ServiceModel.Web
    open System.Net
    open System.Collections.Generic

    [<ServiceContract>]
    [<ServiceBehaviorAttribute(ConcurrencyMode=ConcurrencyMode.Multiple,
        InstanceContextMode=InstanceContextMode.Single)>]
    type ChatService() =
        let setStatus(x:HttpStatusCode) =
            WebOperationContext.Current.OutgoingResponse.StatusCode <- x

        let authCache = new Dictionary<string,string>()

        [<WebGet(UriTemplate = "token?username={username}&password={password}", ResponseFormat = WebMessageFormat.Json)>]
        [<OperationContract>]
        member this.Login(username:string, password:string) =
            match username, password with
            | "dave", "password1" ->
                HttpStatusCode.OK |> setStatus
                let authToken = System.Guid.NewGuid().ToString()
                authCache.Add(authToken, username)
                authToken
            | _ ->
                HttpStatusCode.Unauthorized |> setStatus
                ""

        [<WebInvoke(Method = "DELETE", UriTemplate = "token/{token}")>]
        [<OperationContract>]
        member this.Logout(token:string) =
            if authCache.ContainsKey(token) then
                HttpStatusCode.OK |> setStatus
                authCache.Remove(token) |> ignore
            else
                HttpStatusCode.Unauthorized |> setStatus
           

