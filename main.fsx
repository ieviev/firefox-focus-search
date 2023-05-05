#r "nuget: Fable.Core, 4.0.0-theta-007"
#r "nuget: Fable.Browser.Css"
#r "nuget: Fable.Browser.Dom"

open Browser
open Fable.Import
open Browser.Types
open Fable.Core.JsInterop

let queries =
    [ 
      "input[placeholder=\"Filter by title\"]"
      "form[role=\"search\"] input.width-full"
      "form[role=\"search\"] input"
      "form[id=\"rtd-search-form\"] input"
      ".global-navigation__search"
      "#algoliaSearch"
      "input[type=\"search\"]"
      "input[id=\"search\"]"
      "input[id=\"search2\"]"
      "button.search-input-open"
      ".fxs-search-box > input"
      "span.ytmusic-search-box"
      "input.js-header-search-field"
      "input[role=\"search\"]" 
      "input[autocomplete=\"off\"]" 
      "textarea[data-id=\"root\"]" 
  ]

let nodelist2seq (nl: NodeListOf<Element>) =
    seq {
        for i = 0 to nl.length - 1 do
            yield nl[i]
    }

let isVisible (elem: Element) =
    elem?offsetWidth || elem?offsetHeight || elem.getClientRects()?length

// let sendClickEvent (elem:Element) =
//     let ev = Event.Create("click",null)
//     elem.dispatchEvent(ev) |> ignore

let rec focusSearch (queries: string list) =
    match queries with
    | [] -> None
    | head :: tail ->
        document.querySelectorAll (head)
        |> nodelist2seq
        |> Seq.tryFind isVisible
        |> Option.orElseWith (fun _ -> focusSearch tail)

document.onkeydown <-
    (fun key ->
        if key.altKey && key.code = "KeyQ" then
            // focus bing ai shadow dom properly
            if document.URL.StartsWith("https://www.bing.com/search?q=Bing+AI") then
                let elem = 
                    document.querySelector(".cib-serp-main")
                        .shadowRoot.querySelector("#cib-action-bar-main")
                        .shadowRoot.querySelector(".main-container") :?> HTMLElement
                elem.click()
            else
            
            let foundOpt = focusSearch queries
            console.log foundOpt
            foundOpt
            |> Option.map (fun f -> f :?> HTMLElement)
            |> Option.iter (fun f ->
                key.preventDefault ()
                match f.nodeName with
                | "INPUT" -> f.focus ()
                | "A" -> f.click ()
                | "TEXTAREA" -> f.focus()
                // | n -> sendClickEvent f
                | n -> f.focus ())

        else
            ())
