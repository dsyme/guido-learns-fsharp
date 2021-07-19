module Index

open Elmish

open Feliz
open Feliz.Bulma
open Feliz.Recharts
open Feliz.PigeonMaps
open Elmish.SweetAlert

open Fable.Remoting.Client
open Shared
open Bulma
open type Html
open type Bulma

let country = "NL"

type ServerResponse =
    { 
      Location: LocationResponse
     (* Task 3.1a When we fetch data from the server, also get the weather *)
     (*          Add a 'Weather' field here of type 'WeatherResponse' *)
#if SOLVED     
#else
      Weather: WeatherResponse 
#endif
    }

type ServerState =
    | Idle
    | Loading
    | ServerError of string

type Destination =
    { Text: string
      ValidationError: string option
      ServerState: ServerState
      ServerResponse: ServerResponse option }

    static member Empty =
        { Text = ""
          ServerResponse = None
          ValidationError = None
          ServerState = Idle }


/// The overall data model driving the view.
type Model =
    { Destinations: Destination list }

    /// Get the given destination in the model. If it doesn't 
    /// exist return the empty destination
    member model.GetDestination idx =
        match List.tryItem idx model.Destinations with
        | None -> Destination.Empty
        | Some destination -> destination

    /// Adjust the given destination in the model. If its
    /// text is empty remove it.
    member model.SetDestination idx destination =
        let destinations =
            model.Destinations
            |> List.setItem idx destination 
            |> List.filter (fun v -> v.Text <> "")
        { model with Destinations = destinations }

    /// Remove the given destination from the model
    member model.RemoveDestination idx =
        let destinations =
            model.Destinations
            |> List.removeItem idx  
        { model with Destinations = destinations }

/// Which stop in the trip is being referred to?
type DestinationIndex = int

/// The different types of messages in the web user interface.
type Msg =
    | TextChanged of DestinationIndex * string
    | GetDestination of DestinationIndex
    | GotDestination of DestinationIndex * ServerResponse
    | ErrorMsg of DestinationIndex * exn
    (* Task 4.3a Add a new message RemoveDestination carrying a destination number *)
#if SOLVED
#else
    | RemoveDestination of DestinationIndex
#endif


/// The init function is called to start the message pump with an initial view.
let init () =
    let model = { Destinations = [] }
    model, Cmd.none

let dojoApi =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IDojoApi>

let getResponse stopText = async {
    let! location = dojoApi.GetLocation stopText
     (* Task 3.1c When we fetch data from the server, also get the weather  *)
     (*                                                                     *)
     (*           Use 'let! weather = ... GetWeather' here.                 *)
     (*           The call is asynchronous, so you'll need to use 'let!' to *)
     (*           await the result of the call.                             *)
#if SOLVED
#else
    let! weather = dojoApi.GetWeather stopText
#endif
    let response =
        { 
          Location = location
          (* Task 3.1b Return the weather as part of the overall response *)
          (*           Use 'Weather = weather' like 'Location = location' *)
#if SOLVED
#else
          Weather = weather 
#endif
        }
    return response }

/// The update function knows how to update the model given a message.
let update msg (model: Model) =
    match msg with
    | GetDestination idx->
        let destination = model.GetDestination idx
        match destination.ValidationError with
        | None -> 
            let model = model.SetDestination idx  { destination with ServerState = Loading }
            let text = destination.Text.Replace(" ","")
            model, Cmd.OfAsync.either getResponse text (fun r -> GotDestination(idx, r)) (fun msg -> ErrorMsg(idx, msg))
        | Some _ -> 
            model, Cmd.none

    | GotDestination (idx, response) ->
        let destination = model.GetDestination idx
        let destination = 
            { destination  with
                ValidationError = None
                ServerResponse = Some response
                ServerState = Idle }
        let model = model.SetDestination idx destination
        model, Cmd.none

    (* Task 4.3b Process the message RemoveDestination carrying a destination number *)
    (*           You can call model.RemoveDestination to generate a new model *)
    (*           with the element removed *)
#if SOLVED
#else
    | RemoveDestination idx ->
        let model = model.RemoveDestination idx
        model, Cmd.none
#endif

    | TextChanged (idx, p) ->
        let destination = model.GetDestination idx
        let destination = 
            { destination with
                Text = p
                ValidationError = 
                    if p = "" || Validation.isValidPostcode country p then
                        None
                    else
                        Some "invalid postcode" }
        let model = model.SetDestination idx destination
        model, Cmd.none

    | ErrorMsg (idx, e) ->
        let destination = model.GetDestination idx
        let destination = 
            { destination with
                ServerState = ServerError e.Message }
        let errorAlert =
            SimpleAlert(e.Message)
                .Title("Try another postcode")
                .Type(AlertType.Error)
        let model = model.SetDestination idx destination
        model, SweetAlert.Run errorAlert

let widget (title: string) (content: ReactElement list) =
    box [
        prop.children [
            subtitle title
            yield! content
        ]
    ]

let navbar =
    navbar [
        color.isInfo
        prop.children [
            navbarBrand.a [
                prop.href "https://safe-stack.github.io/docs/"
                prop.target "_"
                prop.children [
                    navbarItem.div [
                        title [
                            icon [
                                icon.isLarge
                                prop.style [ style.color.white; style.transform.scaleX -1 ]
                                prop.children [
                                    i [ prop.className "fas fa-unlock-alt" ]
                                ]
                            ]
                            span [
                                prop.style [ style.color.white ]
                                prop.text "Guido's Trip to Europe"
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]

let makeMarker (latitude, longitude)  =
    PigeonMaps.marker [
        marker.anchor (latitude, longitude)
        marker.render (fun marker -> [
            i [
                if marker.hovered
                then prop.style [ style.color.red; style.cursor.pointer ]
                prop.className [ "fa"; "fa-map-marker"; "fa-2x" ]
            ]
        ])
    ]

let mapDisplay (lr: LocationResponse) =
    let latLong = (lr.Location.LatLong.Latitude, lr.Location.LatLong.Longitude)
    PigeonMaps.map [
        (* Task 2.2 Set the center of the map using map.center *)
        (* supply the lat/long value as input. These come from the LocationResponse *)
#if SOLVED
#else
        map.center (lr.Location.LatLong.Latitude, lr.Location.LatLong.Longitude)
#endif

        (* Task 2.3 Update the Zoom to 15. *)
#if SOLVED
        map.zoom 12
#else
        map.zoom 15
#endif
        map.height 500
        map.markers [
            (* Task 2.4 Create a marker for the map. Use the makeMarker function above. *)
#if SOLVED
#else
            makeMarker (lr.Location.LatLong.Latitude, lr.Location.LatLong.Longitude)
#endif
        ]
    ]

let weatherDisplay (wr: WeatherResponse) =
    div [
        image [
            prop.children [
                img [
                    prop.style [ style.height 100]
                    prop.src (sprintf "https://www.metaweather.com/static/img/weather/%s.svg" wr.WeatherType.Abbreviation) ]
                ]
            ]
        table [
            table.isNarrow
            table.isFullWidth
            prop.style [ style.marginTop 37 ]
            prop.children [
                tbody [
                    tr [
                        th "Temp"
                        (* Task 3.3 Fill in the temperature, the right number is *)
                        (*          available in the WeatherResponse *)
#if SOLVED
                        td $"%.1f{3.00000}"
#else
                        td $"%.1f{wr.AverageTemperature}"
#endif
                    ]
                ]
            ]
        ]
    ]

let locationDisplay (lr: LocationResponse) =
    table [
        table.isNarrow
        table.isFullWidth
        prop.children [
            tbody [
                tr [
                    th "Region"
                    (* Task 1.2 The region shows "TODO".  *)
                    (*          Fill in the region, founf in the LocationResponse *)
                    (*          Replace the string "TODO" with lr.Location and  *)
                    (*          then hit "." to look for the Region *)
#if SOLVED
                    td "TODO"
#else
                    td lr.Location.Region
#endif
                ]
                tr [
                    (* Task 1.3a Heathrow is on plague island! Don't fly there! *)
                    (*          Change Heathrow to Schiphol! *)
                    (*          Then search for DistanceToAirport and find where it's calculated *)
#if SOLVED
                    th "Distance to Heathrow"
#else
                    th "Distance to Schiphol"
#endif
                    td $"%.2f{lr.DistanceToAirport}km" 
                ]
            ]
        ]
    ]

let adjective idx =
    match idx+1 with 
    | 1 -> "First"
#if SOLVED
    (* Task 1.1 The text is wrong - 2th, 3th etc. *)
    (*          Add entries for Second, Third, Fourth, Fifth, 6th etc. *)
    | n -> string n + "th"
#else
    | 2 -> "Second"
    | 3 -> "Third"
    | 4 -> "Fourth"
    | 5 -> "Fifth"
    | n -> string n + "th"
#endif

let destinationEntrySection idx (destination: Destination) dispatch =
    box [
        
        label $"{adjective idx} Stop"
        
        field.div  [
            field.hasAddons
            prop.children [
                control.div [
                    control.hasIconsLeft
                    prop.style [ style.width (length.percent 100)]
                    control.hasIconsRight
                    prop.children [
                        // Show the input box
                        input.text [
                            if destination.ValidationError.IsSome then color.isDanger else color.isInfo
                            prop.placeholder "Ex: 1011 or 2012 ES"
                            prop.style [ style.textTransform.uppercase ]
                            prop.value destination.Text
                            prop.onChange (fun text -> TextChanged(idx, text) |> dispatch)
                        ]

                        // Show the home logo in the input box
                        icon [
                            icon.isLeft
                            prop.children [
                                i [ prop.className "fas fa-home"]
                            ]
                        ]

                        // Show the validation tick/cross
                        match destination.ValidationError with
                        | Some _ ->
                            icon [
                                icon.isRight
                                prop.children [
                                    i [ prop.className "fas fa-times"]
                                ]
                            ]
                        | None when destination.Text = "" -> ()
                        | None -> 
                            icon [
                                icon.isRight
                                prop.children [
                                    i [ prop.className "fas fa-check"]
                                ]
                            ]

                        // Show the validation error (if any)
                        help [
                            match destination.ValidationError with
                            | None -> 
                                 color.isPrimary
                            | Some error -> 
                                 (* Task 2.5 *) 
#if !SOLVED
                                 color.isPrimary
#else
                                 color.isDanger
#endif
                                 prop.text error
                        ]
                    ]
                ]

                // Show the submit button for this entry
                control.div [
                    button.a [
                        color.isInfo
                        prop.onClick (fun _ -> dispatch (GetDestination idx))
                        prop.disabled (destination.Text = "" || destination.ValidationError.IsSome)
                        if (destination.ServerState = Loading) then button.isLoading
                        prop.text "Fetch"
                    ]
                ]
                (* Task 4.1 Add a trash icon by uncommenting the code below *)

#if SOLVED
#else
                control.div [
                    button.a [
                        prop.children [
                            icon [
                                icon.isRight
                                prop.children [
                                    i [ prop.className "fas fa-trash"]
                                ]
                            ]
                        ]
                        if destination.Text = "" then
                            prop.disabled true
                        (* Task 4.2 Add an onClick property that dispatches the *)
                        (* RemoveDestination message *)
#if SOLVED
                        prop.onClick (fun _ -> GetDestination idx |> dispatch) 
#else
                        prop.onClick (fun _ -> RemoveDestination idx |> dispatch) 
#endif

                    ]
                ]
#endif
            ]
        ]
    ]

let destinationInfoSection idx (model: Destination) =
    [
        // Don't show the report if there is a server error
        match model.ServerState with 
        | ServerError _ -> ()
        | _ -> 
        // Don't show the report if it's not available
        match model.ServerResponse with
        | None -> ()
        | Some response ->
         
        section [
            prop.children [
                let title = $"{adjective idx} Stop - {response.Location.Location.Town}"
                widget title  [
                    columns [
                        column [
                            column.isThreeFifths
                            prop.children [
                                locationDisplay response.Location
                            ]
                        ]
                        (* Task 3.2 Add a second column containing the weather information  *)
                        (*          Create this using weatherDisplay, which takes a WeatherResponce *)
                        (*          This can be found in the overall server response *)
#if SOLVED
#else
                        column [
                            weatherDisplay response.Weather
                        ]
#endif
                    ]
                    (* Task 2.1 Add the map widget *) 
                    (*          This is created using mapDisplay which takes a LocationResponse *)
                    (*          This can be found in the overall server response *)
#if !SOLVED
                    mapDisplay response.Location
#else
                    mapDisplay response.Location
#endif
                ]
            ]
        ]
    ]

/// The view function knows how to render the UI given a model, as well as to dispatch new messages based on user actions.
let view (model:Model) dispatch =
    let destinations = model.Destinations
    div [
        prop.style [ style.backgroundColor "#eeeeee57"; style.minHeight (length.vh 100) ]
        prop.children [
            navbar

            section [
                for (i, destination) in List.indexed destinations do
                    destinationEntrySection i destination dispatch

                // Always show at least one empty section so we can add a stop
                destinationEntrySection destinations.Length Destination.Empty dispatch
            ]

            for (i, destination) in List.indexed destinations do
                yield! destinationInfoSection i destination
        ]
    ]


