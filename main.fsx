#r "nuget: Fable.Core, 4.0.0-theta-007"
#r "nuget: Fable.Browser.Css"
#r "nuget: Fable.Browser.Dom"

open Browser
open Fable.Import
open Browser.Types
open Fable.Core.JsInterop

let queries =
    [ "form[role=\"search\"] input"
      ".global-navigation__search"
      "input[type=\"search\"]"
      "input[id=\"search\"]"
      "input[id=\"search2\"]"
      ".fxs-search-box > input"
      "span.ytmusic-search-box" 
      "input.js-header-search-field" 
      "input[role=\"search\"]" 
    ] 

let nodelist2seq (nl: NodeListOf<Element>) =
    seq {
        for i = 0 to nl.length - 1 do
            yield nl[i]
    }

let isVisible (elem:Element) =
    elem?offsetWidth 
    || elem?offsetHeight 
    || elem.getClientRects()?length

// let sendClickEvent (elem:Element) =
//     let ev = Event.Create("click",null)
//     elem.dispatchEvent(ev) |> ignore

let rec focusSearch (queries: string list) (elem: 't option) =
    match queries with
    | [] -> None
    | head :: tail ->
        document.querySelectorAll (head)
        |> nodelist2seq
        |> Seq.tryFind isVisible
        |> Option.orElseWith (fun _ -> focusSearch tail None)

document.onkeydown <-
    (fun key ->
        if key.altKey && key.key = "q" then
            let foundOpt = focusSearch queries None
            // console.log foundOpt
            foundOpt
            |> Option.map (fun f -> f :?> HTMLElement)
            |> Option.iter (fun f ->
                key.preventDefault ()
                match f.nodeName with
                | "INPUT" -> f.focus ()
                | "A" -> f.click ()
                // | n -> sendClickEvent f 
                | n -> f.click()
            )

        else
            ())
