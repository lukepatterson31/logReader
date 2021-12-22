module logReader.JonParser

open System.IO
open System.Text.Json
open System.Text.Json.Nodes

let jsonString = File.ReadAllText "../../../data/sample-corr.json"
let deserializeJson (json: string): JsonObject = JsonSerializer.Deserialize<JsonObject> json


let getLogMessage (jsonStr: string) =
    let getNode (jsonObj: JsonNode, key: string) = jsonObj.Item key
    let root = getNode(getNode(deserializeJson(jsonStr), "log"), "Message")
    root
    
let logRoot = getLogMessage jsonString

let rootObj = logRoot.AsObject()

let rec recursiveDescent (json: JsonObject) =
    for v in json do
        let key = v.Key
        let value = v.Value
        let vtype = value.GetType()
        if vtype = typedefof<JsonObject> then do
            let child = value.AsObject() 
            if child.ContainsKey "s" ||
               child.ContainsKey "e" ||
               child.ContainsKey "d" then printf $"%s{key}: {value}"
    
// recursiveDescent rootObj