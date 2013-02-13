namespace ThePeopleWhoChat.Service

    open System
    open System.ServiceModel
    open System.ServiceModel.Web
    open System.Net
    open System.Collections.Generic

    [<ServiceContract>]
    [<ServiceBehaviorAttribute(ConcurrencyMode=ConcurrencyMode.Multiple,
        InstanceContextMode=InstanceContextMode.Single)>]
    type ChatService() =

        let setToken x =
            WebOperationContext.Current.OutgoingResponse.Headers.Add(Consts.TokenHeaderName, x)
        let getToken() =
            WebOperationContext.Current.IncomingRequest.Headers.[Consts.TokenHeaderName]    
        let setFault(status,err) =
            WebOperationContext.Current.OutgoingResponse.StatusCode <- status
            WebOperationContext.Current.OutgoingResponse.Headers.Add(Consts.ErrorHeaderName,err)       
        let unauthorized err = setFault(HttpStatusCode.Unauthorized,err)         
        let badrequest err = setFault(HttpStatusCode.BadRequest,err)         

        let data = ChatDataConnection(@"C:\RavenDB") :> IChatServiceClient

        member private this.ImplementationWrapper<'U>(f:unit->'U,empty:'U) =
            try f()
            with
            | Failure(e) -> e |> badrequest
                            empty

        [<WebInvoke(Method = "PUT", UriTemplate="token", RequestFormat = WebMessageFormat.Json, 
                    ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)>]
        [<OperationContract>]
        member this.Login(details:LoginDetails) =
            this.ImplementationWrapper((fun () -> data.Login(details.username,details.password) |> setToken),())

//        [<WebInvoke(Method = "DELETE", UriTemplate="token/{token}")>]
//        [<OperationContract>]
//        member this.Logout(token:string) =
//            this.ImplementationWrapper((fun () -> data.Logout(token)),())
//
        [<WebGet(UriTemplate="sessions",ResponseFormat=WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)>]
        [<OperationContract>]
        member this.ListSessions() =
            this.ImplementationWrapper((fun () -> data.ListSessions(getToken())),Array.empty)
//
//        [<WebInvoke(Method = "PUT", UriTemplate="users", RequestFormat = WebMessageFormat.Json, 
//                    ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)>]
//        [<OperationContract>]
//        member this.AddUser(user:User) =
//            this.ImplementationWrapper((fun () -> data.AddUser(getToken(),user)),"")
//
//        [<WebInvoke(Method = "DELETE", UriTemplate="users/{userIdNumPart}")>]
//        [<OperationContract>]
//        member this.RemoveUser(userIdNumPart:string) =
//            this.ImplementationWrapper((fun () -> data.RemoveUser(getToken(),"users/"+userIdNumPart)),())

        [<WebGet(UriTemplate="users",ResponseFormat=WebMessageFormat.Json, BodyStyle=WebMessageBodyStyle.Bare)>]
        [<OperationContract>]
        member this.ListUsers() =
            this.ImplementationWrapper((fun () -> data.ListUsers(getToken())),Array.empty)
                                        
//        [<WebInvoke(Method = "PUT", UriTemplate="rooms", RequestFormat = WebMessageFormat.Json, 
//                    ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)>]
//        [<OperationContract>]
//        member this.AddRoom(room:Room) =
//            this.ImplementationWrapper((fun () -> data.AddRoom(getToken(),room)),"")
//
//        [<WebInvoke(Method = "DELETE", UriTemplate="rooms/{roomIdNumPart}")>]
//        [<OperationContract>]
//        member this.RemoveRoom(roomIdNumPart:string) =
//            this.ImplementationWrapper((fun () -> data.RemoveRoom(getToken(),"rooms/"+roomIdNumPart)),())
//
//        [<WebGet(UriTemplate="rooms",ResponseFormat=WebMessageFormat.Json)>]
//        [<OperationContract>]
//        member this.ListRooms() =
//            this.ImplementationWrapper((fun () -> data.ListRooms(getToken())),Array.empty)
//
//        [<WebInvoke(Method = "PUT", UriTemplate="currentroom", RequestFormat = WebMessageFormat.Json, 
//                    BodyStyle = WebMessageBodyStyle.Bare)>]
//        [<OperationContract>]
//        member this.EnterRoom(roomId:string) =
//            this.ImplementationWrapper((fun () -> data.EnterRoom(getToken(),roomId)),())
//
//        [<WebInvoke(Method = "DELETE", UriTemplate="currentroom")>]
//        [<OperationContract>]
//        member this.LeaveRoom() =
//            this.ImplementationWrapper((fun () -> data.LeaveRoom(getToken())),())
//
//        [<WebGet(UriTemplate="messages?after={after}",ResponseFormat=WebMessageFormat.Json)>]
//        [<OperationContract>]
//        member this.GetMessages(after:string) =
//            this.ImplementationWrapper((fun () -> 
//                let valid,date = DateTime.TryParse(after)
//                if valid then
//                    let results = data.GetMessages(getToken(),date)
//                    results
//                else failwith (sprintf "Invalid date: %s" after)
//                ), Array.empty)
//
//        [<WebInvoke(Method = "PUT", UriTemplate="messages", RequestFormat = WebMessageFormat.Json, 
//                    BodyStyle = WebMessageBodyStyle.Bare)>]
//        [<OperationContract>]
//        member this.PostMessage(message:string) =
//            this.ImplementationWrapper((fun () -> data.PostMessage(getToken(),message)),())