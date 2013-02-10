﻿// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open ThePeopleWhoChat.Service
open System.ServiceModel.Activation
open System.ServiceModel.Web
open System

[<EntryPoint>]
let main argv = 

    let uri = new Uri("http://localhost:8080/chat")
    use host = new WebServiceHost(typeof<ChatService>, uri);
    host.Open()

    Console.WriteLine("Service running on '{0}' - press any key to exit", uri.AbsolutePath)
    Console.ReadKey() |> ignore

    0
