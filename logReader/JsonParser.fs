module logReader.JonParser

open System.IO
open System.Text.Json
open System.Text.Json.Nodes

type Node = {
    Name: string
    Start: int
    End: int
    Duration: int
    SLA: int
    Children: Node List
}

let jsonString = File.ReadAllText "../../../data/sample-corr.json"

let deserializeJson (json: string): JsonObject = JsonSerializer.Deserialize<JsonObject> json

let getNode (jsonObj: JsonNode, key: string) = jsonObj.Item key

let getLogMessage (jsonStr: string) =
    let root = getNode(getNode(deserializeJson(jsonStr), "log"), "Message")
    root.AsObject()
    
//  Needs work, only need to find:
//      - SLA
//      - the nodes that exceed SLA

let rec recursiveDescent (node: JsonNode) =
        let nodeType = node.GetType()
        if nodeType = typedefof<JsonObject> then do
            let child = node.AsObject() 
            if child.ContainsKey "s" &&
               child.ContainsKey "e" &&
               child.ContainsKey "d" then
                   for v in child do
                       match v.Key with
                       | "s" as s -> printfn $"{s}"
                       | "e" as e -> printfn $"{e}"
                       | "d" as d -> printfn $"{d}"
                       |_  -> recursiveDescent v.Value 
                       
let rec topLevelDescent (json: JsonObject) =
    for child in json do
        child.Value |> recursiveDescent

let sla = (getNode (getNode(getNode(deserializeJson(jsonString), "log"), "Message"), "sla")).ToString() |> int

// pass in string |> deserialize into json object at level we desire |> recursive descent of nodes
jsonString |> getLogMessage |> topLevelDescent    